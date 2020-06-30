using Conexia.InfraStructure.AntiCorruption.OpenWeatherMap.Entities;
using Conexia.Domain.Shared.Facades;
using Newtonsoft.Json;
using RestSharp;
using System;

namespace Conexia.InfraStructure.AntiCorruption.Services.OpenWeatherMap
{
    public class TemperatureFacade : ITemperatureFacade
    {
        private const string apiKey = "af3623524b0e7075820a6ca46d04682f";

        public object GetTemperatureCity(string City)
        {
            object calcTemperature;
            object returnTemperature = null;

            var urlTemp = "https://api.openweathermap.org/data/2.5/weather?q=" + City + "&appid=" + apiKey;

            var client = new RestClient("https://openweathermap.org/api");
            var request = new RestRequest(urlTemp, Method.POST);
            request.AddHeader("content-type", "application/json");
            
            var response = client.Execute(request);
            var resultTemperature = JsonConvert.DeserializeObject<Temperature>(response.Content); 

            if(resultTemperature.main != null)
            {
                // return value temperature
                var roundTemperature = Math.Round(resultTemperature.main.temp, MidpointRounding.AwayFromZero);

                // Calculate temperature
                calcTemperature = ((273.15 - roundTemperature) * (-1));
                returnTemperature = Math.Round((double)calcTemperature, MidpointRounding.AwayFromZero);
            }
            
            return returnTemperature;
        }
    }
}
