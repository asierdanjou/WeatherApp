namespace WeatherApp
{
    using System;
    using System.Threading.Tasks;

    public class Core
    {
        public static async Task<Weather> GetWeather(string zipCode)
        {
            string key = "c4af70b3395e852a0b0db76915f1813a";
            string queryString = "http://api.openweathermap.org/data/2.5/weather?zip=" + zipCode + ",es&appid=" + key + "&units=metric";

            dynamic results = await DataService.getDataFromService(queryString).ConfigureAwait(false);

            if (results["weather"] != null)
            {
                Weather weather = new Weather();
                weather.Title = (string)results["name"];
                weather.Temperature = (string)results["main"]["temp"] + " ºC";
                weather.Wind = (string)results["wind"]["speed"] + " Km/h";
                weather.Humidity = (string)results["main"]["humidity"] + " %";
                weather.Visibility = (string)results["weather"][0]["main"];

                DateTime time = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
                DateTime sunrise = time.AddSeconds((double)results["sys"]["sunrise"]);
                DateTime sunset = time.AddSeconds((double)results["sys"]["sunset"]);
                weather.Sunrise = sunrise.ToString() + " UTC";
                weather.Sunset = sunset.ToString() + " UTC";
                return weather;
            }
            else
            {
                return null;
            }
        }
    }
}
