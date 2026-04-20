using RentalDBModels.Models.Interface;

namespace RentalDBModels.Models
{
    public class Materials : ILookupModel
    {
        public int Id { get; set; }
        public bool Flag { get; set; }
        public string Name { get; set; }
    }
}
