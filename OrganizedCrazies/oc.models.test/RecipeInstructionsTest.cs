using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace oc.models.test
{   
    public class RecipeInstructionsTest
    {
        [Fact]
        public void Test_RecipeInstructions_ToString()
        {
            RecipeInstructions instructions = new RecipeInstructions
            {
                new RecipeInstruction
                {
                    Sequence = 1,
                    Instruction = "Boil Rice"
                },
                new RecipeInstruction
                {
                    Sequence = 2,
                    Instruction = "Drain water from rice"
                }
            };
            string actual = instructions.ToString();
            string expected = new StringBuilder()
                .Append("1.  Boil Rice").AppendLine()
                .Append("2.  Drain water from rice").AppendLine().ToString();

            Assert.Equal(expected, actual);
        }
    }
}
