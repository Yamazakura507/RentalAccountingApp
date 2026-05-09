using DataBaseProvaider.Attributes;
using RentalDBModels.Models.Abstract;

namespace RentalDBModels.Models
{
    public class Clients : BaseRemovingModel
    {
        public string OwnerName { get; set; }
        public string Phone { get; set; }

        [SkipProperty]
        public override Type ViewType => typeof(Views.Clients);
    }
}
