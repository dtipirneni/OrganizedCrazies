using System;
using System.Collections.Generic;

namespace oc.models
{
    public class MyRecipe
    {
        public string Author { get; set; }
        public string Name { get; set; }
        public string Desciption { get; set; }
        public int Servings { get; set; }
        public string Source { get; set; }
        public string Category { get; set; }
        public string Cuisine { get; set; }
        public string CookingMethod { get; set; }
        public List<string> Ingredients { get; set; }
        public RecipeInstructions Instructions { get; set; }
        public NutritionList Nutrition { get; set; }
        public List<string> Diets { get; set; }
        public TimeSpan PrepTime { get; set; }
        public TimeSpan CookTime { get; set; }
        public byte[] SourceImage { get; set; }
        public byte[] MyImage { get; set; }
        public byte[] Video { get; set; }
        public List<string> Notes { get; set; }
    }
}
