using DataBaseProvaider.Attributes;
using System.ComponentModel;

namespace RentalDBModels.Views.DBViews
{
    public class AllCategoryView
    {
        [ViewModel(ViewHide = true)]
        public int Id { get; set; }
        [ViewModel(ViewHide = true)]
        public bool IsTypeCategory { get; set; }

        [ViewModel(Headline = true)]
        [Description("Категория")]
        public string Name { get; set; }

        [ViewModel(ViewHide = true, Image = true)]
        public string ImageKey => IsTypeCategory ? "category.png" : "materials.png";
    }
}
