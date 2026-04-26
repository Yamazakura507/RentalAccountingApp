using DataBaseProvaider.Attributes;
using RentalDBModels.Views.Abstract;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RentalDBModels.Views
{
    public class Inventory : BaseView
    {
        [ViewModel(Headline = true)]
        [Description("Инвентарь")]
        public string Name { get; set; }

        [Description("Цена")]
        [DisplayFormat(DataFormatString = "N2")]
        public double Price { get; set; }

        [ViewModel(ViewHide = true, Image = true)]
        public string ImageKey { get; set; } = "inventory.png";

        public override Type ModelType { get => typeof(Models.Categories); }
    }
}
