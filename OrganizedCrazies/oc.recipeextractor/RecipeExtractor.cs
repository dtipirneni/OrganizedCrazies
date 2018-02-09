using Newtonsoft.Json;
using oc.models;
using OpenScraping;
using OpenScraping.Config;
using Schema.NET;
using System;
using System.Net;

namespace oc.recipeextractor
{
    public static class RecipeExtractor
    {
        public static MyRecipe ExtractRecipe(string url)
        {
            string urlResponse;
            MyRecipe myRecipe = null; 

            // 1. Get Response from url
            using (WebClient w = new WebClient())
            {
                urlResponse = w.DownloadString(url);
            }

            //2: Check and scrape if any structured JSON is present (application/ld+json)
            var configJson = @"{                
                'data': '//script[contains(@type, \'application\/ld+json\')]'
            }";
            var config = StructuredDataConfig.ParseJsonString(configJson);
            var openScraping = new StructuredDataExtractor(config);
            var scrapingResults = openScraping.Extract(urlResponse);
            if(scrapingResults != null && scrapingResults["data"] != null)
            {
                var content = scrapingResults["data"].ToString();
                if(content.Contains("\"@type\":\"Recipe\""))
                {
                    try
                    {
                        var serializerSettings = new JsonSerializerSettings()
                        {
                            DateParseHandling = DateParseHandling.DateTimeOffset
                        };
                        Recipe rec = JsonConvert.DeserializeObject<Recipe>(content, serializerSettings);
                        RecipeBuilder builder = new RecipeBuilder();
                        myRecipe = builder.Build(rec);
                    }
                    catch(Exception e)
                    {

                    }
                }
            }
            return myRecipe;
        }
    }
}
