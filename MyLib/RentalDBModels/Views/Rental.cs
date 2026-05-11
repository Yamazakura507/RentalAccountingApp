using DataBaseProvaider;
using DataBaseProvaider.Attributes;
using DataBaseProvaider.Enums;
using DataBaseProvaider.Objects;
using RentalDBModels.Models;
using RentalDBModels.Models.DependenceModel;
using RentalDBModels.Views.Abstract;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace RentalDBModels.Views
{
    public class Rental : BaseRemovingView
    {
        private double sumInventory = 0;

        [ViewModel(Headline = true, IsEdit = false)]
        [Description("Заявка от клиента")]
        public Task<string> ClientName => GetClientInfo();

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


        private async Task<string> GetClientInfo() => (await DBProvider.GetModel<Clients>(this.IdClient)).OwnerName;

        private async Task<double> GetSumInventory()
        {
            ConditionsParametr[] parametrsInventoryRental = [new ConditionsParametr(nameof(InventoryRental.IdRental), ConditionalOperators.Equal, this.Id)];
            List<int> idsInventory = (await DBProvider.GetColumnModel<int, InventoryRental>(nameof(InventoryRental.IdInventory), parametrsInventoryRental)).ToList();

            if (idsInventory.Count == 0) return 0;

            ConditionsParametr[] parametrInventory = [new ConditionsParametr(nameof(Inventory.Id), ConditionalOperators.In, idsInventory)];

            return Convert.ToDouble(await DBProvider.Sum<Inventory>(nameof(Inventory.Price), parametrInventory));
        }

        private async Task<string> GetBaseInfo()
        {
            int rentalCountDays = ((ReturnDate?.DayNumber ?? DateOnly.FromDateTime(DateTime.Now).DayNumber) - IssueDate.DayNumber) + 1;
            sumInventory = await GetSumInventory() * rentalCountDays;

            return String.Format("Арендовано {0}, в течении {1}, на сумму: {2:N2} ₽", FormatPositions(await CountInventory), FormatDays(rentalCountDays), sumInventory);
        }

        private async Task<string> GetPayInfo()
        {
            string status = "Не оплачено";

            if (IdPayment is not null)
            {
                Payments payments = (await DBProvider.GetModel<Payments>(IdPayment.Value));

                if (payments.Sum < sumInventory)
                {
                    if (ReturnDate is not null && DateOnly.FromDateTime(DateTime.Now.Date) > ReturnDate)
                    {
                        status = "Частичная оплата | Долг";
                        BackColor = Color.LightBlue;
                    }
                    else
                    {
                        status = "Частичная оплата";
                        BackColor = Color.LightYellow;
                    }

                    return String.Format("{0} - [{1:N2} ₽/{2:N2} ₽ - {3:dd MMMM yyyy}]", status, payments.Sum, sumInventory, payments.Date);
                }
                else
                {
                    status = "Оплачено";
                    BackColor = Color.FromArgb(219, 255, 221);
                }

                return String.Format("{0} - [{1:N2} ₽ - {2:dd MMMM yyyy}]", status, payments.Sum, payments.Date);
            }
            else if (ReturnDate is not null && DateOnly.FromDateTime(DateTime.Now) > ReturnDate)
            {
                status = "Не оплачено | Долг";
                BackColor = Color.LightGray;
            }

            return status;
        }

        /// <summary>
        /// Склонение слова "позиция" в зависимости от числа
        /// </summary>
        public static string GetPositionDeclension(int number)
        {
            int lastDigit = number % 10;
            int lastTwoDigits = number % 100;

            if (lastTwoDigits >= 11 && lastTwoDigits <= 19)
                return "позиций";

            if (lastDigit == 1)
                return "позиция";

            if (lastDigit >= 2 && lastDigit <= 4)
                return "позиции";

            return "позиций";
        }

        /// <summary>
        /// Возвращает склонированую фразу количество позиций
        /// </summary>
        /// <param name="countPosition">Количество позиций</param>
        /// <returns></returns>
        public static string FormatPositions(int countPosition)
        {
            string word = countPosition.GetDeclension("позиция", "позиции", "позиций");
            return $"{countPosition} {word}";
        }

        /// <summary>
        /// Возвращает склонированую фразу количество дней
        /// </summary>
        /// <param name="countDays">Количество дней</param>
        /// <returns></returns>
        public static string FormatDays(int countDays)
        {
            string word = countDays.GetDeclension("-го дня", "-х дней", "-ти дней");
            return $"{countDays}{word}";
        }
    }
}
