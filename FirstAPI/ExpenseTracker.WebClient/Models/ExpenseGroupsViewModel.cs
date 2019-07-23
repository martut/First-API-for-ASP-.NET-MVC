using System.Collections;
using System.Collections.Generic;
using ExpenseTracker.DTO;
using ExpenseTracker.WebClient.Helpers;
using PagedList;

namespace ExpenseTracker.WebClient.Models
{
    public class ExpenseGroupsViewModel
    {
        public IPagedList<ExpenseGroup> ExpenseGroups { get; set; }
        public IEnumerable<ExpenseGroupStatus> ExpenseGroupStatuses { get; set; }

        public PagingInfo PagingInfo { get; set; }
    }
}