using DataBaseProvaider;
using DataBaseProvaider.Attributes;
using DataBaseProvaider.Enums;
using DataBaseProvaider.Objects;
using RentalDBModels.Models.DependenceModel;
using RentalDBModels.Views.Abstract;
using RentalDBModels.Views.DBFunction;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace RentalDBModels.Views
{
    [Print(Title = "Чек | Счёт", NameResourceReport = "billReport", ReportParameters = ["idRental=Id"])]
    public class Rental : BaseRemovingView
    {
        private double sumInventory = 0;
        private int countInventory = 0;
        Task<GetRentalInfo> getRentalInfo = null;
        private bool isColorLoaded = false;

        [ViewModel(Headline = true, IsEdit = false)]
        [Description("Заявка от клиента")]
        public Task<string> ClientName => GetCilent();

        [ViewModel(FilterOn = true)]
        [Description("Дата аренды")]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}")]
        [Check(NameCustomCheckFunc = nameof(Models.Rental.DateIssueCheck),
            NotChecibleMessage = "Некоректная дата аренды!\nДата аренды должна быть меньше или равна дате возврата.")]
        public DateOnly IssueDate { get; set; }

        [ViewModel(FilterOn = true)]
        [Check(IsNull = true)]
        [Description("Дата возврата")]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}")]
        public DateOnly? ReturnDate { get; set; }

        [ViewModel(IsEdit = false)]
        [Description("Информация")]
        public Task<string> InfoStr => GetBaseInfo();

        [ViewModel(IsEdit = false)]
        [Description("Статус оплаты")]
        public Task<string> PayStatus => GetPayInfo();

        [ViewModel(ViewHide = true, Image = true)]
        public string ImageKey { get; set; } = "rent.png";

        public override Color BackColor 
        { 
            get
            {
                if (!isColorLoaded) _ = WithColor();
                
                return base.BackColor;
            }
            set 
            {
                isColorLoaded = true;
                base.BackColor = value;
            }
        }

        [ViewModel(ViewHide = true)]
        public override Type ModelType { get => typeof(Models.Rental); }
        [ViewModel(ViewHide = true)]
        public Task<int> CountInventory => DBProvider.Count<InventoryRental>([new ConditionsParametr(nameof(InventoryRental.IdRental), ConditionalOperators.Equal, this.Id)]);

        [Dependency("Платеж", typeof(Payments), DependencyType.OneToOneSelectionNewObject, ImageKey = "pay.png", DependencyModelType = typeof(Models.Payments))]
        public int? IdPayment { get; set; }

        [Dependency("Клиент", typeof(Clients), DependencyType.OneToOneSelectionList, ImageKey = "clients.png", DependencyModelType = typeof(Models.Clients))]
        public int IdClient { get; set; }

        [Dependency("Инвентарь в аренде", typeof(Inventory), DependencyType.OneToMany, ImageKey = "inventory.png", DependencyModelType = typeof(InventoryRental))]
        Task<IEnumerable<int>> InventoryId => GetDependenciesId<InventoryRental>();

        public Task<GetRentalInfo> GetRentalInfo
        {
            get
            {
                if (getRentalInfo is null)
                {
                    getRentalInfo = DBProvider.GetCallFunctionModel<GetRentalInfo>([this.Id]);
                }

                return getRentalInfo;
            }
        }

        private async Task<string> GetCilent() => (await GetRentalInfo).Client;
        private async Task<string> GetPayInfo() => (await GetRentalInfo).PayInfo;
        private async Task<string> GetBaseInfo() => (await GetRentalInfo).GetBaseInfo();
        private async Task WithColor() 
        {
            BackColor = (await GetRentalInfo).StatusColor;
        }

        public void RefrashRentalInfo()
        {
            getRentalInfo = null;
            isColorLoaded = false;
        }
    }
}
