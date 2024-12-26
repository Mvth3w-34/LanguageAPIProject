using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace LanguageProjectBackend.Services
{
    //This models a simple text translator.
    public class Translator
    {
        private readonly string key = Environment.GetEnvironmentVariable("TRANS_API_KEY"); //Translator API key.
        private readonly string endpoint = "https://api.cognitive.microsofttranslator.com"; //Translator API endpoint.
        private string location = "canadacentral"; //Resource location for the header of the request.

        // This method will take an english word/phrase and translates it into the target languange using the Azure translator API.
        public string TranslateWord(string word, string languagePreference)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }

            string targetLanguage = "";
            string translation = "";

            //Set the target language for the translator API
            switch (languagePreference)
            {
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

            string route = $"/translate?api-version=3.0&from=en&to={targetLanguage}"; //url

            object[] body = new object[] { new { text = word } };
            var requestBody = JsonConvert.SerializeObject(body);



            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                // Build the request.
                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(endpoint + route);
                request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                request.Headers.Add("Ocp-Apim-Subscription-Key", key);
                // location required if you're using a multi-service or regional (not global) resource.
                request.Headers.Add("Ocp-Apim-Subscription-Region", location);

                // Send the request and get response.
                HttpResponseMessage response = client.Send(request);
                // Read response as a string.
                string jsonResponse = response.Content.ReadAsStringAsync().Result;

                //Parse the text key to get the translation
                JArray jArray = JArray.Parse(jsonResponse);
                translation = jArray[0]["translations"][0]["text"].ToString();


                return translation;
            }

        }
    }
}
