using DataBaseProvaider.Attributes;
using RentalDBModels.Models.Abstract;

namespace RentalDBModels.Models.DependenceModel
{
    public class InventoryCategories : BaseDependeceModel
    {
        [DependencyModel(IsDependency = true)]
        public int IdCategory { get; set; }
        [DependencyModel(IsForigen = true)]
        public int IdInventory { get; set; }

        public override TDependencyModel Initialize<TDependencyModel>(int dependencyId, int forignId)
        {
            IdInventory = forignId;
            IdCategory = dependencyId;

            return this as TDependencyModel;
        }
    }
}
