using System.Linq;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace ExpenseTracker.WebClient.Helpers
{
    public class HeaderParser
    {
        public static PagingInfo FindAndParsePagingInfo(HttpResponseHeaders responseHeaders)
        {
            if (responseHeaders.Contains("X-Pagination"))
            {
                var xPag = responseHeaders.First(p => p.Key == "X-Pagination").Value;

                return JsonConvert.DeserializeObject<PagingInfo>(xPag.First());
            }

            return null;
        } 
    }
}