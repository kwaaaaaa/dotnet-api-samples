using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace APICollection.Controllers
{
    public class WeatherController : Controller
    {
        public Models.BeaufortScale WindDescription(float windspeed)
        {
            if (windspeed <= 3f) {
                return Models.BeaufortScale.Calm;
            } else if (windspeed <= 11f) {
                return Models.BeaufortScale.Light;
            } else if (windspeed <= 19f) {
                return Models.BeaufortScale.Gentle;
            } else if (windspeed <= 38f) {
                return Models.BeaufortScale.Moderate;
            } else if (windspeed <= 61f) {
                return Models.BeaufortScale.Strong;
            } else if (windspeed <= 88f) {
                return Models.BeaufortScale.Severe;
            } else if (windspeed <= 102f) {
                return Models.BeaufortScale.Stormy;
            } else if (windspeed <= 117f) {
                return Models.BeaufortScale.Violent;
            } else {
                return Models.BeaufortScale.Devastating;
            }
        }

        public async Task<Models.Weather> ReturnCity(string city)
        {
            // textInfo class to change strings to proper case
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;

            // create url
            string base_url = "http://api.openweathermap.org";
            string path_url = string.Format("/data/2.5/weather?q={0}&appid={1}&units=metric",
                city,
                Env._weatherSecret
                );

            // call url
            using (var client = new HttpClient())
            {
                client.BaseAddress = new System.Uri(base_url);
                var response = await client.GetAsync(path_url);
                response.EnsureSuccessStatusCode();

                var stringResult = await response.Content.ReadAsStringAsync();
                JObject json = JObject.Parse(stringResult);

                var wind = WindDescription(System.Convert.ToSingle(json["wind"]["speed"].ToString()));

                return new Models.Weather
                {
                    City = textInfo.ToTitleCase(city),
                    Description = textInfo.ToTitleCase(json["weather"][0]["description"].ToString()),
                    Wind = wind,
                    Humidity = json["main"]["humidity"].ToString(),
                    MinTemp = json["main"]["temp_min"].ToString(),
                    MaxTemp = json["main"]["temp_max"].ToString(),
                    Scale = Models.TemperatureScales.Celsius
                };
            };
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View("IndexEmpty");
        }

        [HttpPost]
        public async Task<IActionResult> Index(string city_one, string city_two)
        {
            if (city_one == null || city_two == null)
            {
                return View("IndexEmpty");
            };

            List<Models.Weather> weathers = new List<Models.Weather>();

            // first city
            try {
                Models.Weather w = await ReturnCity(city_one);
                weathers.Add(w);
            } catch
            {
                return Content($"Sorry but {city_one} cannot be found. Try Another!");
            }

            // second city
            try
            {
                Models.Weather w2 = await ReturnCity(city_two);
                weathers.Add(w2);
            }
            catch
            {
                return Content($"Sorry but {city_two} cannot be found. Try Another!");
            }

            return View(weathers);
        }
    }
}
