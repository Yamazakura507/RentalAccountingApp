using DataBaseProvaider.Attributes;
using RentalDBModels.Views.Abstract;
using System.ComponentModel;

namespace RentalDBModels.Views
{
    public class Clients : BaseView
    {
        [ViewModel(Headline = true)]
        [Description("Клиент")]
        public string OwnerName { get; set; }

        [ViewModel]
        [Description("Телефон")]
        public string Phone { get; set; }

        [ViewModel(ViewHide = true, Image = true)]
        public string ImageKey { get; set; } = "clients.png";

        [ViewModel(ViewHide = true)]
        public override Type ModelType { get => typeof(Models.Clients); }
    }
}
