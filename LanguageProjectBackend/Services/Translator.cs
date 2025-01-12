using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace LanguageProjectBackend.Services
{
    //This models a simple text translator.
    public class Translator
    {
        private readonly string _key = Environment.GetEnvironmentVariable("TRANS_API_KEY"); //Translator API _key.
        private readonly string _endpoint = "https://api.cognitive.microsofttranslator.com"; //Translator API endpoint.
        private string _location = "canadacentral"; //Resource location for the header of the request.

        // This method will take an english word/phrase and translates it into the target languange using the Azure translator API.
        public string TranslateWord(string word, string languagePreference)
        {
            if (string.IsNullOrEmpty(_key))
            {
                throw new ArgumentNullException("_key not found");
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
                request.RequestUri = new Uri(_endpoint + route);
                request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                request.Headers.Add("Ocp-Apim-Subscription-Key", _key);
                // _location required if you're using a multi-service or regional (not global) resource.
                request.Headers.Add("Ocp-Apim-Subscription-Region", _location);

                // Send the request and get response.
                HttpResponseMessage response = client.Send(request);
                // Read response as a string.
                string jsonResponse = response.Content.ReadAsStringAsync().Result;

                //Parse the text _key to get the translation
                JArray jArray = JArray.Parse(jsonResponse);
                translation = jArray[0]["translations"][0]["text"].ToString();


                return translation;
            }

        }
    }
}
