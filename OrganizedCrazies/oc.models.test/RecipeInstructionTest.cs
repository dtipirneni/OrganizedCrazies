using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace oc.models.test
{   
    public class RecipeInstructionTest
    {
        [Fact]
        public void Test_RecipeInstruction_ToString()
        {
            RecipeInstruction instruction = new RecipeInstruction
            {
                Sequence = 1,
                Instruction = "Boil Rice"
            };
            string actual = instruction.ToString();
            Assert.Equal("1.  Boil Rice", actual);
        }
    }
}
