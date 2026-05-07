using DataBaseProvaider.Attributes;
using RentalDBModels.Models.Abstract;

namespace RentalDBModels.Models.DependenceModel
{
    public class InventoryRental : BaseDependeceModel
    {
        [DependencyModel(IsDependency = true)]
        public int IdInventory { get; set; }

        [DependencyModel(IsForigen = true)]
        public int IdRental { get; set; }

        public override TDependencyModel Initialize<TDependencyModel>(int dependencyId, int forignId)
        {
            IdRental = forignId;
            IdInventory = dependencyId;

            return this as TDependencyModel;
        }
    }
}
