using DataBaseProvaider.Attributes;
using RentalDBModels.Models.Abstract;

namespace RentalDBModels.Models
{
    public class Materials : BaseLookupModel
    {
        [SkipProperty]
        public override Type ViewType => typeof(Views.Materials);
    }
}
