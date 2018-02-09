using System;
using oc.models;
using Schema.NET;
using System.Collections.Generic;

namespace oc.recipeextractor
{
    public class RecipeBuilder
    {
        public MyRecipe Build(Recipe rec)
        {
            MyRecipe recipe = new MyRecipe();

            var person = (Person)rec.Author.Value.Value;
            recipe.Author = person.Name.HasValue && person.Name.Value.HasValue ? (string)person.Name.Value.Value : string.Empty;
            recipe.Category = rec.RecipeCategory.HasValue && rec.RecipeCategory.Value.HasValue ? (string)rec.RecipeCategory.Value.Value : string.Empty;
            recipe.CookingMethod = rec.CookingMethod.HasValue && rec.CookingMethod.Value.HasValue ? (string)rec.CookingMethod.Value.Value: string.Empty;
            recipe.CookTime = rec.CookTime.HasValue && rec.CookTime.Value.HasValue ? (TimeSpan)rec.CookTime.Value.Value : TimeSpan.FromTicks(0);
            recipe.Cuisine = rec.RecipeCuisine.HasValue && rec.RecipeCuisine.Value.HasValue ? (string)rec.RecipeCuisine.Value.Value : string.Empty;
            recipe.Desciption = rec.Description.HasValue && rec.Description.Value.HasValue ? (string)rec.Description.Value.Value : string.Empty;
            recipe.Diets = rec.SuitableForDiet.HasValue && rec.SuitableForDiet.Value.HasValue ? new List<string> { (rec.SuitableForDiet.Value.Value).ToString() } : null;
            recipe.Ingredients = rec.RecipeIngredient.HasValue ? rec.RecipeIngredient.Value.List : null;            
            recipe.Instructions = BuildInstructions(rec.RecipeInstructions);
            recipe.Name = rec.Name.HasValue ? rec.Name.Value.Value.ToString() : string.Empty;
            recipe.Nutrition = BuildNutrition(rec.Nutrition);
            recipe.PrepTime = rec.PrepTime.HasValue && rec.PrepTime.Value.HasValue? (TimeSpan)rec.PrepTime.Value.Value : TimeSpan.FromTicks(0);
            recipe.Servings = ExtractServings(rec.RecipeYield);
            recipe.Source = rec.Url.HasValue && rec.Url.Value.HasValue ? rec.Url.Value.Value.ToString() : string.Empty;
            
            return recipe;
        }

        private int ExtractServings(Values<QuantitativeValue, string>? recipeYield)
        {
            int servings = 0;
            if(!recipeYield.HasValue)
            {
                return servings;
            }
            if(recipeYield.Value.Values1.HasValue)
            {
                servings = (int)recipeYield.Value.Values1.Value;
            }
            else if (recipeYield.Value.Values2.HasValue)
            {
                string[] temp = recipeYield.Value.Values2.Value.ToString().Split(" ");
                servings = Convert.ToInt32(temp[0]);
            }
            return servings;
        }

        private NutritionList BuildNutrition(Values<NutritionInformation>? nutrition)
        {
            if(!nutrition.HasValue || !nutrition.Value.HasValue)
            {
                return null;
            }
            NutritionList nList = new NutritionList();
            NutritionInformation nInfo = (NutritionInformation)nutrition.Value.Value;
            nList.Calories = ExtractCalories(nInfo.Calories);
            nList.Carbohydrates = ExtractCarbs(nInfo.CarbohydrateContent);
            nList.Fat = ExtractFat(nInfo.FatContent);
            nList.Fiber = ExtractFiber(nInfo.FiberContent);
            nList.Protien = ExtractProtien(nInfo.ProteinContent);
            
            return nList;
        }

        private double ExtractProtien(Values<string>? proteinContent)
        {
            if (!proteinContent.HasValue || !(proteinContent.Value.HasValue))
            {
                return 0;
            }
            string protien = (string)proteinContent.Value.Value;
            double p = 0;
            var pArray = protien.Split(" ");
            p = Convert.ToDouble(pArray[0]);
            return p;
        }

        private double ExtractFiber(Values<string>? fiberContent)
        {
            if (!fiberContent.HasValue || !(fiberContent.Value.HasValue))
            {
                return 0;
            }
            string fiber = (string)fiberContent.Value.Value;
            double f = 0;
            var fArray = fiber.Split(" ");
            f = Convert.ToDouble(fArray[0]);
            return f;
        }

        private double ExtractFat(Values<string>? fatContent)
        {
            if (!fatContent.HasValue || !(fatContent.Value.HasValue))
            {
                return 0;
            }
            string fat = (string)fatContent.Value.Value;
            double f = 0;
            var fArray = fat.Split(" ");
            f = Convert.ToDouble(fArray[0]);
            return f;
        }

        private double ExtractCarbs(Values<string>? carbohydrateContent)
        {
            if (!carbohydrateContent.HasValue || !(carbohydrateContent.Value.HasValue))
            {
                return 0;
            }
            string carbs = (string)carbohydrateContent.Value.Value;
            double c = 0;
            var cArray = carbs.Split(" ");
            c = Convert.ToDouble(cArray[0]);
            return c;
        }

        private double ExtractCalories(Values<string>? calories)
        {
            if(!calories.HasValue || !(calories.Value.HasValue))
            {
                return 0;
            }
            string cals = (string)calories.Value.Value;
            double c = 0;
            var cArray = cals.Split(" ");
            c = Convert.ToDouble(cArray[0]);
            return c;
        }

        private RecipeInstructions BuildInstructions(Values<CreativeWork, ItemList, string>? recipeInstructions)
        {
            if(!recipeInstructions.HasValue)
            {
                return null;
            }
            RecipeInstructions instructions = new RecipeInstructions();
            if (recipeInstructions.Value.Values3.HasValue)
            {
                int counter = 0;
                foreach (var i in recipeInstructions.Value.Values3.List)
                {
                    RecipeInstruction instr = new RecipeInstruction
                    {
                        Sequence = ++counter,
                        Instruction = i
                    };
                    instructions.Add(instr);
                }
            }
            return (instructions.Count > 0 ? instructions : null);
        }       
    }
}