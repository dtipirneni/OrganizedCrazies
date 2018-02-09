using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using oc.models;
using Schema.NET;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FluentAssertions.Common;
using FluentAssertions;

namespace oc.recipeextractor.test
{
    public class RecipeBuilderTest
    {
        [Fact]
        public void ExtractRecipeTest()
        {
            Recipe rec = PrepRecipe();
            RecipeInstructions instructions = PrepRecipeInstructions();
            NutritionList nlist = new NutritionList
            {
                Calories = 240,
                Fat = 9
            };

            RecipeBuilder builder = new RecipeBuilder();
            MyRecipe recipe = builder.Build(rec);

            Assert.Equal("Sahil Makhija", recipe.Author);
            Assert.Equal("Dessert", recipe.Category);
            Assert.Equal(string.Empty, recipe.CookingMethod);
            Assert.Equal(TimeSpan.FromMinutes(20), recipe.CookTime);
            Assert.Equal("General", recipe.Cuisine);
            Assert.Equal("A delicious layered low carb dessert with the flavours of chocolate and coffee", recipe.Desciption);
            Assert.Equal(new List<string> { "LowFatDiet" }, recipe.Diets);
            Assert.Equal(new List<string>
                {
                "45 grams Almond Flour ( I use this one )",
                "30 grams Salted Butter",
                "1 Tbsp Unsweetened Coco Powder ( I recommend this one )",
                "150 grams Mascarpone cheese",
                "1 Tsp Vanilla Extract",
                "2 Tbsp Water",
                "1 Tsp Instant espresso powder",
                "100 ml Heavy Cream",
                "30 grams Dark Chocolate (85% or Higher) (I use Lindt 85%)",
                "Stevia to taste" }
            , recipe.Ingredients);
            
            recipe.Instructions.Should().BeEquivalentTo(instructions);
            Assert.Equal("Keto Coffee & Chocolate Tart", recipe.Name);
            recipe.Nutrition.Should().BeEquivalentTo(nlist);
            Assert.Equal(TimeSpan.FromMinutes(10), recipe.PrepTime);
            Assert.Equal(3, recipe.Servings);
            Assert.Equal("http://www.janedoe.com/", recipe.Source);
        }

        private static RecipeInstructions PrepRecipeInstructions()
        {
            RecipeInstructions instructions = new RecipeInstructions
            {
                new RecipeInstruction
                {
                    Sequence = 1,
                    Instruction = "Microwave the butter for 30 seconds till melted"
                },
                new RecipeInstruction
                {
                    Sequence = 2,
                    Instruction = "Add in your stevia/sweetner to taste, vanilla essence and the coco powder and mix well together"
                },
                new RecipeInstruction
                {
                    Sequence = 3,
                    Instruction = "Add in the almond flour and combine till well incorporated"
                },
                new RecipeInstruction
                {
                    Sequence = 4,
                    Instruction = "Divide the mixture in 3 tart tins or ramekins and shape the base"
                },
                new RecipeInstruction
                {
                    Sequence = 5,
                    Instruction = "Bake at 175 C/ 350 F for 10 minutes and then allow them to cool"
                },
                new RecipeInstruction
                {
                    Sequence = 6,
                    Instruction = "Heat 2 tablespoons of water and mix 1 tsp of instant espresso powder into that"
                },
                new RecipeInstruction
                {
                    Sequence = 7,
                    Instruction = "Whip the mascarpone cheese, stevia, vanilla extract and coffee mixture together till nice and fluffy"
                },
                new RecipeInstruction
                {
                    Sequence = 8,
                    Instruction = "Pour the mascarpone mixture over the base and chill in the fridge for 15 minutes"
                },
                new RecipeInstruction
                {
                    Sequence = 9,
                    Instruction = "Meanwhile warm up the cream for 30 seconds in the microwave and add the chocolate and sweetner to that and mix till fully melted and you have a creamy ganache"
                },
                new RecipeInstruction
                {
                    Sequence = 10,
                    Instruction = "Pour the ganache over the mascarpone mousse in the tart molds and chill in the fridge for an hour"
                },
                new RecipeInstruction
                {
                    Sequence = 11,
                    Instruction = "Finish with some sea salt on top of each tart."
                }
            };
            return instructions;
        }

        private static Recipe PrepRecipe()
        {
            StringBuilder sb = new StringBuilder("{");
            sb.Append("\"@context\":\"http://schema.org/\",");
            sb.Append("\"@type\":\"Recipe\",");
            sb.Append("\"name\":\"Keto Coffee & Chocolate Tart\",\"author\":{ \"@type\":\"Person\",\"name\":\"Sahil Makhija\"},\"datePublished\":\"2009-11-05T00:00:00+00:00\",");
            sb.Append("\"description\":\"A delicious layered low carb dessert with the flavours of chocolate and coffee\",\"recipeYield\":\"3 servings\",\"aggregateRating\":{ \"@type\":\"AggregateRating\",\"ratingValue\":\"5\",\"ratingCount\":\"6\"},");
            sb.Append("\"prepTime\":\"PT10M\",\"cookTime\":\"PT20M\",");
            sb.Append("\"recipeIngredient\":[\"45 grams Almond Flour ( I use this one )\",\"30 grams Salted Butter\",\"1 Tbsp Unsweetened Coco Powder ( I recommend this one )\",\"150 grams Mascarpone cheese\",\"1 Tsp Vanilla Extract\",\"2 Tbsp Water\",\"1 Tsp Instant espresso powder\",\"100 ml Heavy Cream\",\"30 grams Dark Chocolate (85% or Higher) (I use Lindt 85%)\",\"Stevia to taste\"],");
            sb.Append("\"nutrition\": {");
            sb.Append("\"@type\": \"NutritionInformation\",");
            sb.Append("\"calories\": \"240 calories\",");
            sb.Append("\"fatContent\": \"9 grams fat\"},");
            sb.Append("\"recipeInstructions\":[");
            sb.Append("\"Microwave the butter for 30 seconds till melted\",\"Add in your stevia/sweetner to taste, vanilla essence and the coco powder and mix well together\",\"Add in the almond flour and combine till well incorporated\",");
            sb.Append("\"Divide the mixture in 3 tart tins or ramekins and shape the base\",\"Bake at 175 C/ 350 F for 10 minutes and then allow them to cool\",\"Heat 2 tablespoons of water and mix 1 tsp of instant espresso powder into that\",");
            sb.Append("\"Whip the mascarpone cheese, stevia, vanilla extract and coffee mixture together till nice and fluffy\",\"Pour the mascarpone mixture over the base and chill in the fridge for 15 minutes\",");
            sb.Append("\"Meanwhile warm up the cream for 30 seconds in the microwave and add the chocolate and sweetner to that and mix till fully melted and you have a creamy ganache\",\"Pour the ganache over the mascarpone mousse in the tart molds and chill in the fridge for an hour\",\"Finish with some sea salt on top of each tart.\"],");
            sb.Append("\"recipeCategory\":\"Dessert\",\"recipeCuisine\":\"General\",\"suitableForDiet\": \"http://schema.org/LowFatDiet\",");
            sb.Append("\"url\": \"http://www.janedoe.com\"}");

            var json = sb.ToString();
            var serializerSettings = new JsonSerializerSettings()
            {
                DateParseHandling = DateParseHandling.DateTimeOffset
            };
            serializerSettings.Converters.Add(new IsoDateTimeConverter());
            serializerSettings.Converters.Add(new TimeSpanToISO8601DurationValuesConverter());
            Recipe rec = JsonConvert.DeserializeObject<Recipe>(json, serializerSettings);
            return rec;
        }
    }
}
