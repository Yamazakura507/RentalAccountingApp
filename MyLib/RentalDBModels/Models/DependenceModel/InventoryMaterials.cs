using DataBaseProvaider.Attributes;
using RentalDBModels.Models.Abstract;

namespace RentalDBModels.Models.DependenceModel
{
    public class InventoryMaterials : BaseDependeceModel
    {
        [DependencyModel(IsDependency = true)]
        public int IdMaterial { get; set; }
        [DependencyModel(IsForigen = true)]
        public int IdInventory { get; set; }

        public override TDependencyModel Initialize<TDependencyModel>(int dependencyId, int forignId)
        {
            IdInventory = forignId;
            IdMaterial = dependencyId;

            return this as TDependencyModel;
        }
    }
}
