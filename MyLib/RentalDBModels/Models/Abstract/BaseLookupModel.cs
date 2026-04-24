using RentalDBModels.Models.Interface;

namespace RentalDBModels.Models.Abstract
{
    public abstract class BaseLookupModel : BaseModel, ILookupModel
    {
        public string Name { get; set; }
    }
}
