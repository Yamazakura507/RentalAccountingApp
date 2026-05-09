using RentalDBModels.Models.Abstract;

namespace RentalDBModels.Models.Interface
{
    public interface IForigenParent
    {
        Dictionary<Type,IEnumerable<int?>> InsertDependencies { get; set; }
        Dictionary<Type,IEnumerable<int>> RemoveDependencies { get; set; }

        void ClearDependencies();
    }
}
