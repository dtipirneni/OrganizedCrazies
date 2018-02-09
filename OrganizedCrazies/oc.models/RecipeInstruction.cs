using System.Text;

namespace oc.models
{
    public class RecipeInstruction
    {
        public int Sequence { get; set; }
        public string Instruction { get; set; }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder($"{Sequence}.  {Instruction}");
            return sb.ToString();
        }
    }
}