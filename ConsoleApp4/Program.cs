﻿using System;
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
        public const string Endpoint = "https://textanalysisforazure5thdayofserverless.cognitiveservices.azure.com/";
        private const string SubscriptionKey = "614aa3d48e0e4f549ccaf78c604f6780";
        
        static void Main(string[] args)
        {
            //getMessage();
            var client = authenticateClient();

            sentimentAnalysisExample(client);
            languageDetectionExample(client);
            entityRecognitionExample(client);
            keyPhraseExtractionExample(client);
            Console.Write("Press any key to exit.");
            Console.ReadKey();
            
        }

        static TextAnalyticsClient authenticateClient()
        {
            ApiKeyServiceClientCredentials credentials = new ApiKeyServiceClientCredentials(SubscriptionKey);
            TextAnalyticsClient client = new TextAnalyticsClient(credentials)
            {
                Endpoint = Endpoint
            };
            return client;
        }


        static void languageDetectionExample(ITextAnalyticsClient client)
        {
            var result = client.DetectLanguage("This is something.");
            Console.WriteLine($"Language: {result.DetectedLanguages[0].Name}");
        }

        static void sentimentAnalysisExample(ITextAnalyticsClient client)
        {
            var result = client.Sentiment("I had the worse day of my life.", "en");
            Console.WriteLine($"Sentiment Score: {result.Score:0.00}");
        }
        static void entityRecognitionExample(ITextAnalyticsClient client)
        {

            var result = client.Entities("Microsoft was founded by Bill Gates and Paul Allen on April 4, 1975, to develop and sell BASIC interpreters for the Altair 8800.");
            Console.WriteLine("Entities:");
            foreach (var entity in result.Entities)
            {
                Console.WriteLine($"\tName: {entity.Name},\tType: {entity.Type ?? "N/A"},\tSub-Type: {entity.SubType ?? "N/A"}");
                foreach (var match in entity.Matches)
                {
                    Console.WriteLine($"\t\tOffset: {match.Offset},\tLength: {match.Length},\tScore: {match.EntityTypeScore:F3}");
                }
            }
        }

        static void keyPhraseExtractionExample(TextAnalyticsClient client)
        {
            var result = client.KeyPhrases("My cat might need to see a veterinarian.");

            // Printing key phrases
            Console.WriteLine("Key phrases:");

            foreach (string keyphrase in result.KeyPhrases)
            {
                Console.WriteLine($"\t{keyphrase}");
            }
        }

        static async void getMessage()
        {
            //store the GET request in a URL which can be referenced later
            string GetRequestURL = "https://christmaswishes.azurewebsites.net/api/Wishes";

            //create an instance of HttpClient
            HttpClient httpclient = new HttpClient();

            //get the reponse as JSON format
            string response = await httpclient.GetStringAsync(GetRequestURL);
            string newResponse = @"{ ""children"":" + response + "}";

            //parse the data
            var parseddata = JsonConvert.DeserializeObject<Rootobject>(newResponse);

            //assign the textbox in the UWP application from the deserialsed data
            Console.WriteLine(parseddata.children[0].who.ToString());

          
        }
    }
}
