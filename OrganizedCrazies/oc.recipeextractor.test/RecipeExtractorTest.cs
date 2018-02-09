using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Schema.NET;
using System;
using System.Text;
using Xunit;

namespace oc.recipeextractor.test
{
    public class RecipeExtractorTest
    {
        [Fact]
        public void ExtractRecipeTest()
        {
            string testUrl = "https://headbangerskitchen.com/recipe/low-carb-dessert/";
            var recipe = RecipeExtractor.ExtractRecipe(testUrl);
        }

        
    }
}
