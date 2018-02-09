using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using OpenScraping;
using OpenScraping.Config;
using Schema.NET;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;


namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //1: LD+JSON
            ExtractJsonLdRecipe();
        }

        static void ExtractJsonLdRecipe()
        {
            //// 1.
            //// URL: http://en.wikipedia.org/wiki/Main_Page
            //WebClient w = new WebClient();
            //string s = w.DownloadString("https://headbangerskitchen.com/recipe/low-carb-dessert/");

            var configJson = @"
            {
                'title': '//h1',
                'body': '//script[contains(@type, \'application\/ld+json\')]'
            }";

            var config = StructuredDataConfig.ParseJsonString(configJson);
            StringBuilder sb = new StringBuilder("{");
            sb.Append("\"@context\":\"http://schema.org/\",");
            sb.Append("\"@type\":\"Recipe\",");
            sb.Append("\"name\":\"Keto Coffee & Chocolate Tart\",\"author\":{ \"@type\":\"Person\",\"name\":\"Sahil Makhija\"},\"datePublished\":\"2009-11-05T00:00:00+00:00\",");
            sb.Append("\"description\":\"A delicious layered low carb dessert with the flavours of chocolate and coffee\",\"recipeYield\":\"3 servings\",\"aggregateRating\":{ \"@type\":\"AggregateRating\",\"ratingValue\":\"5\",\"ratingCount\":\"6\"},");
            sb.Append("\"prepTime\":\"PT10M\",\"cookTime\":\"PT20M\",");
            sb.Append("\"recipeIngredient\":[\"45 grams Almond Flour ( I use this one )\",\"30 grams Salted Butter\",\"1 Tbsp Unsweetened Coco Powder ( I recommend this one )\",\"150 grams Mascarpone cheese\",\"1 Tsp Vanilla Extract\",\"2 Tbsp Water\",\"1 Tsp Instant espresso powder\",\"100 ml Heavy Cream\",\"30 grams Dark Chocolate (85% or Higher) (I use Lindt 85%)\",\"Stevia to taste\"],");
            sb.Append("\"recipeInstructions\":[");
            sb.Append("\"Microwave the butter for 30 seconds till melted\",\"Add in your stevia/sweetner to taste, vanilla essence and the coco powder and mix well together\",\"Add in the almond flour and combine till well incorporated\",");
            sb.Append("\"Divide the mixture in 3 tart tins or ramekins and shape the base\",\"Bake at 175 C/ 350 F for 10 minutes and then allow them to cool\",\"Heat 2 tablespoons of water and mix 1 tsp of instant espresso powder into that\",");
            sb.Append("\"Whip the mascarpone cheese, stevia, vanilla extract and coffee mixture together till nice and fluffy\",\"Pour the mascarpone mixture over the base and chill in the fridge for 15 minutes\",");
            sb.Append("\"Meanwhile warm up the cream for 30 seconds in the microwave and add the chocolate and sweetner to that and mix till fully melted and you have a creamy ganache\",\"Pour the ganache over the mascarpone mousse in the tart molds and chill in the fridge for an hour\",\"Finish with some sea salt on top of each tart.\"],");
            sb.Append("\"recipeCategory\":\"Dessert\",\"recipeCuisine\":\"General\",\"suitableForDiet\": \"http://schema.org/LowFatDiet\"}");

            var json = sb.ToString();            
            var serializerSettings = new JsonSerializerSettings()
            {
                DateParseHandling = DateParseHandling.DateTimeOffset
            };
            serializerSettings.Converters.Add(new IsoDateTimeConverter());
            serializerSettings.Converters.Add(new TimeSpanToISO8601DurationValuesConverter());
           

            Recipe rec = JsonConvert.DeserializeObject<Recipe>(json, serializerSettings);            
            Console.WriteLine("Extracting LD+JSON Recipe.....");
            Console.Write(rec);           
            Console.ReadKey();
        }
    }    
}
