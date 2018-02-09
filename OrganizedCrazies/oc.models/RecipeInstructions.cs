using System.Collections.Generic;
using System.Text;

namespace oc.models
{
    public class RecipeInstructions : List<RecipeInstruction>
    {
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach(RecipeInstruction instruction in this)
            {
                sb.Append(instruction.ToString()).AppendLine();
            }
            return sb.ToString();
        }
    }
}