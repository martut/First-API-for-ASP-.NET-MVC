using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ExpenseTracker.WebClient.Helpers
{
    public static class ExpenseTrackerHttpClient
    {
        public static HttpClient GetClient(string requestedVersion = null)
        {

            HttpClient client = new HttpClient();



            client.BaseAddress = new Uri(ExpenseTrackerConstants.ExpenseTrackerAPI);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            if (requestedVersion != null)
            {
                client.DefaultRequestHeaders.Add("api-version", requestedVersion);


                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/vnd.expensetrackerapi.v"
                                                        + requestedVersion + "+json"));
            }



            return client;

        }

    }
}