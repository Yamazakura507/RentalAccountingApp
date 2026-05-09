using DataBaseProvaider.Attributes;
using RentalDBModels.Models.Abstract;

namespace RentalDBModels.Models
{
    public class Categories : BaseLookupModel
    {
        [SkipProperty]
        public override Type ViewType => typeof(Views.Categories);
    }
}
