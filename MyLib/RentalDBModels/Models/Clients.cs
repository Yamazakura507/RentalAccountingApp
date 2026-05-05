using RentalDBModels.Models.Abstract;

namespace RentalDBModels.Models
{
    public class Clients : BaseModel
    {
        public string OwnerName { get; set; }
        public string Phone { get; set; }
    }
}
