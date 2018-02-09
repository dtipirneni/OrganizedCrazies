using System;
using System.Text;
using Xunit;

namespace oc.models.test
{
    public class NutritionListTest
    {

        [Fact]
        public void Test_NutritionList_ToString()
        {
            NutritionList nl = new NutritionList
            {
                Calories = 200,
                Fat = 10,
                Protien = 10,
                Carbohydrates = 15,
                Fiber = 4
            };            
            string expectedString = new StringBuilder("Nutrition Per Serving:").AppendLine()
                .Append($"Calories: 200 kCal;").AppendLine()
                .Append($"Fat: 10 gms;").AppendLine()
                .Append($"Protien: 10 gms;").AppendLine()
                .Append($"Carbs: 15 gms;").AppendLine()
                .Append($"Fiber: 4 gms;").AppendLine()
                .Append($"NetCarbs: 11 gms;").ToString();
            Assert.Equal(expectedString, nl.ToString());

        }

        [Fact]
        public void Test_NutritionList_NetCarbs()
        {
            NutritionList nl = new NutritionList
            {
                Calories = 200,
                Fat = 10,
                Protien = 10,
                Carbohydrates = 15,
                Fiber = 4
            };
            Assert.Equal(11, nl.NetCarbs);   
        }
    }
}
