using DataBaseProvaider.Attributes;
using RentalDBModels.Views.Abstract;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RentalDBModels.Views
{
    public class Payments : BaseView
    {
        [ViewModel(Headline = true, IsEdit = false)]
        [Description("Оплата")]
        public string Header => $"{Sum.ToString("N2")} ₽ | {Date.ToDateTime(TimeOnly.MinValue).ToString("dd MMMM yyyy")}";

        [ViewModel(FilterOn = true)]
        [Description("Сумма оплаты")]
        [DisplayFormat(DataFormatString = "{0:N2} ₽")]
        [Check(NameCustomCheckFunc = nameof(Models.Payments.SumCheck),
            NotChecibleMessage = "Некоректная сумма оплаты!\nСумма оплаты должна быть больше 0.")]
        public double Sum { get; set; }

        [ViewModel(FilterOn = true)]
        [Description("Дата оплаты")]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}")]
        public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        [ViewModel(ViewHide = true, Image = true)]
        public string ImageKey { get; set; } = "pay.png";

        public override Type ModelType { get => typeof(Models.Payments); }
    }
}
