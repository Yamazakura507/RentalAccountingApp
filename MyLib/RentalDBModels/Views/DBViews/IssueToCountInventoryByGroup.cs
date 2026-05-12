using DataBaseProvaider.Attributes;
using System.ComponentModel;

namespace RentalDBModels.Views.DBViews
{
    public class IssueToCountInventoryByGroup
    {
        [ViewModel(Headline = true)]
        [Description("Категория")]
        public string IssueDate => String.Format("{0} - {1}", IssueYear, IssueMonth);

        [ViewModel(ViewHide = true)]
        public int IssueYear { get; set; }

        [ViewModel(ViewHide = true)]
        public int IssueMonth { get; set; }

        [Description("Количество выданого инвентаря")]
        [ViewModel]
        public int CountInventroy { get; set; }

        [ViewModel(ViewHide = true, Image = true)]
        public string ImageKey => "date.png";
    }
}
