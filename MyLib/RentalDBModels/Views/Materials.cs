using DataBaseProvaider.Attributes;
using RentalDBModels.Models.Interface;
using System.ComponentModel;

namespace RentalDBModels.Views
{
    public class Materials : IModel
    {
        [ViewModel(ViewHide = true)]
        public int Id { get; set; }

        [ViewModel(ViewHide = true, RemovingFlag = true)]
        public bool Flag { get; set; }

        [ViewModel(Headline = true)]
        [Description("Материал")]
        public string Name { get; set; }

        [ViewModel(ViewHide = true, Image = true)]
        public string ImageKey { get; set; } = "materials.png";
    }
}
