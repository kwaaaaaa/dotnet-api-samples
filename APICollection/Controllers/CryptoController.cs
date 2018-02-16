using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace APICollection.Controllers
{
    public class CryptoController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Models.Crypto> cryptos = new List<Models.Crypto>();

            // url is static for now
            string baseCoin = "BTC";
            string base_url = "https://rest.coinapi.io";
            string ck = Env._cryptoSecret;
            string path_url = $"/v1/exchangerate/{baseCoin}?apikey={ck}&output_format=json";

            // call url
            using (var client = new HttpClient())
            {
                client.BaseAddress = new System.Uri(base_url);
                var response = await client.GetAsync(path_url);
                response.EnsureSuccessStatusCode();

                var stringResult = await response.Content.ReadAsStringAsync();
                JObject json = JObject.Parse(stringResult);

                foreach (var obj in json["rates"])
                {
                    Models.Crypto cr = new Models.Crypto
                    {
                        Currency = obj["asset_id_quote"].ToString(),
                        Rate = System.Convert.ToInt32(System.Math.Round(System.Decimal.Parse(obj["rate"].ToString())))
                    };
                    if (cr.Rate > 0) cryptos.Add(cr);
                }
            };

            return View(cryptos.OrderBy(i => i.Rate).ToList());
        }
    }
}
