using DataBaseProvaider.Attributes;
using RentalDBModels.Views.Interface;

namespace RentalDBModels.Views.Abstract
{
    public class BaseRemovingView : BaseView, IRemovingView
    {
        [ViewModel(ViewHide = true, RemovingFlag = true)]
        public bool Flag { get; set; } = true;
    }
}
