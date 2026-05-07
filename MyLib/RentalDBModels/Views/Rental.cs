using DataBaseProvaider;
using DataBaseProvaider.Attributes;
using DataBaseProvaider.Enums;
using DataBaseProvaider.Objects;
using RentalDBModels.Models.DependenceModel;
using RentalDBModels.Views.Abstract;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RentalDBModels.Views
{
    public class Rental : BaseView
    {
        [ViewModel(Headline = true)]
        [Description("Клиент")]
        public Task<string> ClientName => ClientStr();

        [ViewModel]
        [Description("Дата аренды")]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}")]
        public DateOnly IssueDate { get; set; }

        [ViewModel]
        [Description("Дата возврата")]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}")]
        public DateOnly? ReturnDate { get; set; }

        [ViewModel]
        [Description("Количество ивентаря в аренде")]
        public Task<int> CountInventory => DBProvider.Count<InventoryRental>([new ConditionsParametr(nameof(InventoryRental.IdRental), ConditionalOperators.Equal, this.Id)]);

        [ViewModel]
        [Description("Статус оплаты")]
        public string PayStatus => true ? "Оплачено" : ReturnDate is not null && DateOnly.FromDateTime(DateTime.Now) > ReturnDate ? "Не оплачено | Долг" : "Не оплачено";

        [ViewModel(ViewHide = true, Image = true)]
        public string ImageKey { get; set; } = "rent.png";

        [ViewModel(ViewHide = true)]
        public override Type ModelType { get => typeof(Models.Rental); }

        //[Dependency("Платеж", typeof(Categories), DependencyType.OneToOne, ImageKey = "pay.png", DependencyModelType = typeof(InventoryCategories))]
        //public int? IdPayment { get; set; }

        [Dependency("Клиент", typeof(Clients), DependencyType.OneToOnePicker, ImageKey = "clients.png", DependencyModelType = typeof(Models.Clients))]
        public int IdClient { get; set; }

        [Dependency("Инвентарь в аренде", typeof(Inventory), DependencyType.OneToMany, ImageKey = "inventory.png", DependencyModelType = typeof(InventoryRental))]
        Task<IEnumerable<int>> InventoryId => GetDependenciesId<InventoryRental>();


        private async Task<string> ClientStr() => (await DBProvider.GetModel<Clients>(this.IdClient)).OwnerName;
    }
}
