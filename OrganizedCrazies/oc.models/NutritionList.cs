using System.Text;

namespace oc.models
{
    public class NutritionList
    {
        public double Calories { get; set; }
        public double Fat { get; set; }
        public double Protien { get; set; }
        public double Carbohydrates { get; set; }
        public double Fiber { get; set; }
        public double NetCarbs => (Carbohydrates > 0) ? (Carbohydrates - Fiber) : 0;

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Nutrition Per Serving:").AppendLine();
            sb.Append($"Calories: {Calories} kCal;").AppendLine();
            sb.Append($"Fat: {Fat} gms;").AppendLine(); 
            sb.Append($"Protien: {Protien} gms;").AppendLine(); 
            sb.Append($"Carbs: {Carbohydrates} gms;").AppendLine();
            sb.Append($"Fiber: {Fiber} gms;").AppendLine();
            sb.Append($"NetCarbs: {NetCarbs} gms;");
            return sb.ToString();
        }
    }
}