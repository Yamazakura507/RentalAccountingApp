using DataBaseProvaider.Attributes;
using RentalDBModels.Models.Abstract;

namespace RentalDBModels.Models
{
    public class Payments : BaseModel
    {
        [SkipProperty]
        public override Type ViewType => typeof(Views.Payments);

        public double Sum { get; set; }

        public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        public bool SumCheck() => Sum > 0;
    }
}
