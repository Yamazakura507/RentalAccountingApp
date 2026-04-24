using DataBaseProvaider;
using DataBaseProvaider.Attributes;
using RentalDBModels.Models.Interface;
using RentalDBModels.Views.Interface;

namespace RentalDBModels.Views.Abstract
{
    public abstract class BaseView : IView
    {
        [ViewModel(ViewHide = true)]
        public int Id { get; set; }

        [ViewModel(ViewHide = true, RemovingFlag = true)]
        public bool Flag { get; set; }

        [ViewModel(ViewHide = true)]
        public virtual Type ModelType { get; set; }

        public async Task<IModel> GetModel() => (IModel)Convert.ChangeType(await this.ModelType.GetResultByType<object>([this.Id], nameof(DBProvider.GetModel)), this.ModelType);
    }
}
