using DataBaseProvaider.Attributes;
using RentalDBModels.Views.Abstract;
using System.ComponentModel;

namespace RentalDBModels.Views
{
    public class Materials : BaseView
    {
        [ViewModel(Headline = true)]
        [Description("Материал")]
        public string Name { get; set; }

        [ViewModel(ViewHide = true, Image = true)]
        public string ImageKey { get; set; } = "materials.png";

        public override Type ModelType { get => typeof(Models.Materials); }
    }
}
