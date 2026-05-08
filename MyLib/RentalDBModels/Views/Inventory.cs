using DataBaseProvaider.Attributes;
using DataBaseProvaider.Enums;
using RentalDBModels.Models.DependenceModel;
using RentalDBModels.Views.Abstract;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RentalDBModels.Views
{
    public class Inventory : BaseRemovingView
    {
        [ViewModel(Headline = true)]
        [Description("Инвентарь")]
        public string Name { get; set; }

        [ViewModel(FilterOn = true)]
        [Description("Цена(₽/сут.)")]
        [DisplayFormat(DataFormatString = "{0:N2} ₽")]
        public double Price { get; set; }

        [ViewModel(ViewHide = true, Image = true)]
        public string ImageKey { get; set; } = "inventory.png";

        [ViewModel(ViewHide = true)]
        public override Type ModelType { get => typeof(Models.Inventory); }

        [Dependency("Категории", typeof(Categories), DependencyType.OneToMany, ImageKey = "category.png", DependencyModelType = typeof(InventoryCategories))]
        Task<IEnumerable<int>> CategoriesId => GetDependenciesId<InventoryCategories>();

        [Dependency("Материалы", typeof(Materials), DependencyType.OneToMany, ImageKey = "materials.png", DependencyModelType = typeof(InventoryMaterials))]
        Task<IEnumerable<int>> MaterialsId => GetDependenciesId<InventoryMaterials>();
    }
}
