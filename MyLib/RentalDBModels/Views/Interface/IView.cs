using RentalDBModels.Models.Interface;

namespace RentalDBModels.Views.Interface
{
    public interface IView
    {
        public int Id { get; set; }

        public bool Flag { get; set; }

        public Type ModelType { get; set; }

        Task<IModel> GetModel();
    }
}
