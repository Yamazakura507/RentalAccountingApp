using DataBaseProvaider.Attributes;
using RentalDBModels.Models.Abstract;
using RentalDBModels.Models.DependenceModel;
using RentalDBModels.Models.Interface;


namespace RentalDBModels.Models
{
    public class Inventory : BaseLookupModel, IForigenParent
    {
        public double Price { get; set; }

        [SkipProperty]
        public override Type ViewType => typeof(Views.Inventory);

        [SkipProperty]
        public Dictionary<Type, IEnumerable<int?>> InsertDependencies { get; set; }
        [SkipProperty]
        public Dictionary<Type, IEnumerable<int>> RemoveDependencies { get; set; }

        public void ClearDependencies()
        {
            InsertDependencies.Clear();
            RemoveDependencies.Clear();
        }

        public override async Task<IModel> Insert()
        {
            IModel model = await base.Insert();
            bool isInsertDependency = await InsertDependency(model);

            return model;
        }

        public override async Task<TModel> Insert<TModel>()
        {
            TModel model = await base.Insert<TModel>();
            bool isInsertDependency = await InsertDependency(model);

            return model;
        }

        public override async Task<IModel> Update(IModel oldModel = null)
        {
            IModel model = await base.Update(oldModel);
            bool isUpdate = await UpdateDependency(model);

            return model;
        }

        public override async Task<TModel> Update<TModel>(TModel oldModel = null)
        {
            TModel model = await base.Update<TModel>(oldModel);
            bool isUpdate = await UpdateDependency(model);

            return model;
        }

        private async Task<bool> InsertDependency(IModel model)
        {
            bool isDependencyCategory = await InsertDependency<InventoryCategories>(model, InsertDependencies.GetValueOrDefault(typeof(InventoryCategories)).OfType<int>());
            bool isDependencyMaterial = await InsertDependency<InventoryMaterials>(model, InsertDependencies.GetValueOrDefault(typeof(InventoryMaterials)).OfType<int>());
            InsertDependencies.Clear();

            return isDependencyCategory && isDependencyMaterial;
        }

        private async Task<bool> RemoveDependency(IModel model)
        {
            bool isDependencyCategory = await RemoveDependency<InventoryCategories>(model, RemoveDependencies.GetValueOrDefault(typeof(InventoryCategories)));
            bool isDependencyMaterial = await RemoveDependency<InventoryMaterials>(model, RemoveDependencies.GetValueOrDefault(typeof(InventoryMaterials)));
            RemoveDependencies.Clear();

            return isDependencyCategory && isDependencyMaterial;
        }

        private async Task<bool> UpdateDependency(IModel model)
        { 
            bool isInsertDependency = await InsertDependency(model);
            bool isRemoveDependency = await RemoveDependency(model);

            return isInsertDependency && isRemoveDependency;
        }
    }
}
