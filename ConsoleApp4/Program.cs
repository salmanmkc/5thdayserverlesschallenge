using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics.Models;
using Microsoft.Rest;
using Newtonsoft.Json;
using static ConsoleApp4.messages;

namespace ConsoleApp4
{
    class Program
    {
        private const string key_var = "614aa3d48e0e4f549ccaf78c604f6780";
        private static readonly string key = Environment.GetEnvironmentVariable(key_var);

        private const string endpoint_var = "https://textanalysisforazure5thdayofserverless.cognitiveservices.azure.com/";
        private static readonly string endpoint = Environment.GetEnvironmentVariable(endpoint_var);
        static void Main(string[] args)
        {
            getMessage();
            Console.ReadLine();
        }

        static async void getMessage()
        {
            //store the GET request in a URL which can be referenced later
            string GetRequestURL = "https://christmaswishes.azurewebsites.net/api/Wishes";

            //create an instance of HttpClient
            HttpClient jsonclient = new HttpClient();

            //get the reponse as JSON format
            string response = await jsonclient.GetStringAsync(GetRequestURL);
            string newResponse = @"{ ""children"":" + response + "}";

            //parse the data
            var parseddata = JsonConvert.DeserializeObject<Rootobject>(newResponse);

            //assign the textbox in the UWP application from the deserialsed data
            Console.WriteLine(parseddata.children[0].who.ToString());

          
        }
    }
}
