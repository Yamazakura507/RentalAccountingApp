using RentalDBModels.Models.Abstract;

namespace RentalDBModels.Models
{
    public class Payments : BaseModel
    {
        public double Sum { get; set; }

        public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    }
}
