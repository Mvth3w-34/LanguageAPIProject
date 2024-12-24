using LanguageProjectBackend.Models;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Text;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace LanguageProjectBackend.Services
{
    //This models a simple text translator.
    public class Translator
    {

        private readonly string key = Environment.GetEnvironmentVariable("TRANS_API_KEY"); //Translator API key.
        private readonly string endpoint = "https://api.cognitive.microsofttranslator.com"; //Translator API endpoint.
        private string location = "canadacentral"; //Resource location for the header of the request.

        /*
         * This method will take an english word/phrase and translates it into the target languange using the Azure translator API.
         */
        public string TranslateWord(NewWord word, string languagePreference) 
        {
            string targetLanguage = "";
            string translation = "";

            //Set the target language for the translator API
            switch (languagePreference) {
            case "Swahili":
                targetLanguage = "sw";
                break;
            case "Arabic":
                targetLanguage = "ar";
                break;
            default: 
                targetLanguage = "fr"; //Default target language will be french.
                break;
            }

            string route = "/translate?api-version=3.0&from=en&to=" + targetLanguage; //url

            object[] body = new object[] { new { Text= word } };
            var requestBody =JsonConvert.SerializeObject(body);



            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {

                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(endpoint + route);
                request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                request.Headers.Add("Ocp-Apim-Subscription-Key", key); //API key 
                request.Headers.Add("Ocp-Apim-Subscription-Region", location); //Resource Location

                HttpResponseMessage response = client.Send(request); //Makes the post request

                string jsonResponse = response.Content.ToString(); // Converts result into a string
                var result = JsonConvert.DeserializeObject<JsonElement>(jsonResponse);

                translation = result[0].GetProperty("translation")[0].GetProperty("text").GetString();
                 
            }

            return translation;
        } 
    }
}
