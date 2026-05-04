using RentalDBModels.Models.Abstract;

namespace RentalDBModels.Models.Interface
{
    public interface IDependency
    {
        public int Id { get; init; }

        Task<IDependency> Insert<TModel>() where TModel : IDependency, new();

        Task Delete<TModel>() where TModel : IDependency, new();
    }
}
