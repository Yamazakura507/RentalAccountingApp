using DataBaseProvaider.Attributes;
using RentalDBModels.Models.Abstract;
using RentalDBModels.Models.DependenceModel;
using RentalDBModels.Models.Interface;

namespace RentalDBModels.Models
{
    public class Rental : BaseRemovingModel, IForigenParent
    {
        [SkipProperty]
        public override Type ViewType => typeof(Views.Rental);

        public DateOnly IssueDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        public DateOnly? ReturnDate { get; set; }

        public int? IdPayment { get; set; }

        public int IdClient { get; set; }

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
            IdClient = InsertDependencies.GetValueOrDefault(typeof(Clients)).OfType<int>().First();
            IdPayment = InsertDependencies.GetValueOrDefault(typeof(Payments)).DefaultIfEmpty(null).FirstOrDefault();

            IModel model = await base.Insert();
            bool isInsertDependency = await InsertDependency(model);

            return model;
        }

        public override async Task<TModel> Insert<TModel>()
        {
            IdClient = InsertDependencies.GetValueOrDefault(typeof(Clients)).OfType<int>().First();
            IdPayment = InsertDependencies.GetValueOrDefault(typeof(Payments)).DefaultIfEmpty(null).FirstOrDefault();

            TModel model = await base.Insert<TModel>();
            bool isInsertDependency = await InsertDependency(model);

            return model;
        }

        public override async Task<IModel> Update(IModel oldModel = null)
        {
            IdClient = InsertDependencies.GetValueOrDefault(typeof(Clients)).OfType<int>().FirstOrDefault(IdClient);
            IdPayment = InsertDependencies.GetValueOrDefault(typeof(Payments)).DefaultIfEmpty(IdPayment).FirstOrDefault();

            IModel model = await base.Update(oldModel);
            bool isUpdate = await UpdateDependency(model);

            return model;
        }

        public override async Task<TModel> Update<TModel>(TModel oldModel = null)
        {
            IdClient = InsertDependencies.GetValueOrDefault(typeof(Clients)).OfType<int>().FirstOrDefault(IdClient);
            IdPayment = InsertDependencies.GetValueOrDefault(typeof(Payments)).DefaultIfEmpty(IdPayment).FirstOrDefault();

            TModel model = await base.Update<TModel>(oldModel);
            bool isUpdate = await UpdateDependency(model);

            return model;
        }

        private async Task<bool> InsertDependency(IModel model)
        {
            bool isDependencyInventory = await InsertDependency<InventoryRental>(model, InsertDependencies.GetValueOrDefault(typeof(InventoryRental)).OfType<int>());
            InsertDependencies.Clear();

            return isDependencyInventory;
        }

        private async Task<bool> RemoveDependency(IModel model)
        {
            bool isDependencyInventory = await RemoveDependency<InventoryRental>(model, RemoveDependencies.GetValueOrDefault(typeof(InventoryRental)));
            RemoveDependencies.Clear();

            return isDependencyInventory;
        }

        private async Task<bool> UpdateDependency(IModel model)
        {
            bool isInsertDependency = await InsertDependency(model);
            bool isRemoveDependency = await RemoveDependency(model);

            return isInsertDependency && isRemoveDependency;
        }

        public bool DateIssueCheck() => ReturnDate is null || IssueDate <= ReturnDate;
    }
}
