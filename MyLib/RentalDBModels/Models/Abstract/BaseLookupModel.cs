using RentalDBModels.Models.Interface;

namespace RentalDBModels.Models.Abstract
{
    public abstract class BaseLookupModel : BaseRemovingModel, ILookupModel
    {
        public string Name { get; set; }
    }
}
