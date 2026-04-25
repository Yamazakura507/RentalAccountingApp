using DataBaseProvaider.Attributes;
using RentalDBModels.Views.Abstract;
using System.ComponentModel;

namespace RentalDBModels.Views
{
    public class Categories : BaseView
    {
        [ViewModel(Headline = true)]
        [Description("Категория")]
        public string Name { get; set; }

        [ViewModel(ViewHide = true, Image = true)]
        public string ImageKey { get; set; } = "category.png";

        public override Type ModelType { get => typeof(Models.Categories); }
    }
}
