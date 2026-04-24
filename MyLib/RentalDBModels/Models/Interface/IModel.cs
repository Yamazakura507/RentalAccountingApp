using System.Data;

namespace RentalDBModels.Models.Interface
{
    public interface IModel
    {
        public int Id { get; init; }

        public bool Flag { get; set; }

        Task<IModel> Insert();
        Task<IModel> Update();
        Task Delete();
    }
}
