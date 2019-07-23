using ExpenseTracker.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ExpenseTracker.Repository.Helpers;

namespace ExpenseTracker.Repository.Factories
{
    public class ExpenseGroupFactory
    {
        ExpenseFactory expenseFactory = new ExpenseFactory();

        public ExpenseGroupFactory()
        {

        }

        public ExpenseGroup CreateExpenseGroup(DTO.ExpenseGroup expenseGroup)
        {
            return new ExpenseGroup()
            {
                Description = expenseGroup.Description,
                ExpenseGroupStatusId = expenseGroup.ExpenseGroupStatusId,
                Id = expenseGroup.Id,
                Title = expenseGroup.Title,
                UserId = expenseGroup.UserId,
                Expenses = expenseGroup.Expenses == null ? new List<Expense>() : expenseGroup.Expenses.Select(e => expenseFactory.CreateExpense(e)).ToList()
            };
        }


        public DTO.ExpenseGroup CreateExpenseGroup(ExpenseGroup expenseGroup)
        {
            return new DTO.ExpenseGroup()
            {
                Description = expenseGroup.Description,
                ExpenseGroupStatusId = expenseGroup.ExpenseGroupStatusId,
                Id = expenseGroup.Id,
                Title = expenseGroup.Title,
                UserId = expenseGroup.UserId,
                Expenses = expenseGroup.Expenses.Select(e => expenseFactory.CreateExpense(e)).ToList()
            };
        }


        public object CreateDataShapedObject(DTO.ExpenseGroup expenseGroup, List<string> lstOfFields)
        {
            List<string> fieldsToWorkWithList = new List<string>(lstOfFields);




            if (!fieldsToWorkWithList.Any())
            {
                return expenseGroup;
            }
            else
            {
                var lstOfExpenseFields = fieldsToWorkWithList.Where(f => f.Contains("expenses")).ToList();

                bool returnPartialExpense = lstOfExpenseFields.Any() && !lstOfExpenseFields.Contains("expenses");

                if (returnPartialExpense)
                {
                    fieldsToWorkWithList.RemoveRange(lstOfExpenseFields);
                    lstOfExpenseFields = lstOfExpenseFields.Select(f => f.Substring(f.IndexOf(".") + 1)).ToList();
                }
                else
                {
                    lstOfExpenseFields.Remove("expenses");
                    fieldsToWorkWithList.RemoveRange(lstOfExpenseFields);
                }

                ExpandoObject objectToReturn = new ExpandoObject();
                foreach (var field in fieldsToWorkWithList)
                {
                    var fieldValue = expenseGroup.GetType()
                        .GetProperty(field, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)
                        .GetValue(expenseGroup, null);

                    ((IDictionary<string, object>)objectToReturn).Add(field, fieldValue);

                }

                if (returnPartialExpense)
                {
                    List<object> expenses = new List<object>();
                    foreach (var expense in expenseGroup.Expenses)
                    {
                        expenses.Add(expenseFactory.CreateDataShapedObject(expense, lstOfExpenseFields));
                    }
                    ((IDictionary<string, object>)objectToReturn).Add("expenses", expenses);

                }

                return objectToReturn;
            }
        }

        public object CreateDataShapedObject(ExpenseGroup expenseGroup, List<string> lstOfFields)
        {
            return CreateDataShapedObject(CreateExpenseGroup(expenseGroup), lstOfFields);

        }
    }
}
