using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APICollection.Controllers
{
    public class CurrencyController : Controller
    {
        string _api_end_point = "https://api.fixer.io/latest?base=USD";

        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            List<Models.Currency> fxRates = new List<Models.Currency>();

            // build url
            var uriBuilder = new UriBuilder(_api_end_point);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["base"] = "USD";
            uriBuilder.Query = query.ToString();

            // call url
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(uriBuilder.ToString());
                var response = await client.GetAsync("");
                response.EnsureSuccessStatusCode();

                var stringResult = await response.Content.ReadAsStringAsync();
                JObject json = JObject.Parse(stringResult);
                JObject jsonRates = JObject.Parse(json["rates"].ToString());

                foreach (var r in jsonRates)
                {
                    fxRates.Add(new Models.Currency
                    {
                        Name = r.Key,
                        Rate = Math.Round(Decimal.Parse(r.Value.ToString()), 2)
                    });
                };
            };

            return View(fxRates);
        }
    }
}
