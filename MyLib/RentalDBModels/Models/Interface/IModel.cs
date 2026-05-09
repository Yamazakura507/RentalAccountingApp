using DataBaseProvaider.Attributes;
using RentalDBModels.Models.Abstract;
using RentalDBModels.Views.Interface;

namespace RentalDBModels.Models.Interface
{
    public interface IModel
    {
        public int Id { get; init; }
        public abstract Type ViewType { get; }

        Task<IModel> Insert();
        Task<IModel> Update(IModel oldModel = null);
        Task Delete();

        Task<TModel> Insert<TModel>() where TModel : IModel, new();
        Task<TModel> Update<TModel>(TModel oldModel = null) where TModel : class, IModel, new();
        Task Delete<TModel>() where TModel : IModel;

        Task<bool> InsertDependency<TDependencyModel>(IModel model, IEnumerable<int> idDependecies) where TDependencyModel : BaseDependeceModel, new();

        IModel Clone();

        IEnumerable<DependencyAttribute> GetDependencies<TModel>() where TModel : IModel;

        Task<IView> GetView();
    }
}
