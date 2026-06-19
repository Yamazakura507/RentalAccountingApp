using System.Drawing;
using System.Text;

namespace RentalDBModels.Views.DBFunction
{
    public class GetRentalInfo
    {
        /// <summary>
        /// Цвета состояния заявки
        /// </summary>
        /// <remarks>
        /// Частичная оплата | Долг - LightBlue (Status - 0)
        /// Частичная оплата - LightYellow (Status - 1)
        /// Переплата - FromArgb(157, 212, 160) (Status - 2)
        /// Оплачено - FromArgb(219, 255, 221) (Status - 3)
        /// Не оплачено | Долг - LightGray (Status - 4)
        /// Не оплачено - White (Status - 5)
        /// </remarks>
        private static readonly Color[] statusColor = [ Color.LightBlue, Color.LightYellow, Color.FromArgb(157, 212, 160), Color.FromArgb(219, 255, 221), Color.LightGray, Color.White ];

        public string Client { get; set; }

        public int RentalDays { get; set; }

        public int CountInventory { get; set; }

        public double RentalSum { get; set; }

        public string PayInfo { get; set; }

        public int Status { get; set; }

        public Color StatusColor => statusColor[Status];

        /// <summary>
        /// Строковое представление основной информации по заявке аренды
        /// </summary>
        /// <returns></returns>
        public string GetBaseInfo()
        {
            return String.Format("Арендовано {0}, в течении {1}, на сумму: {2:N2} ₽", FormatPositions(CountInventory), FormatDays(RentalDays), RentalSum);
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
            if (countDays <= 0) return "0 дней";

            DateTime startDate = DateTime.Today;
            DateTime endDate = startDate.AddDays(countDays);

            int years = endDate.Year - startDate.Year;
            int months = endDate.Month - startDate.Month;
            int days = endDate.Day - startDate.Day;

            if (days < 0)
            {
                months--;
                days += DateTime.DaysInMonth(startDate.Year, startDate.Month);
            }

            if (months < 0)
            {
                years--;
                months += 12;
            }

            StringBuilder builder = new();
            string format = "{0}{1} ";

            if (years > 0)
            {
                string yearWord = years.GetDeclension("-го года", "-х лет", "-и лет");
                builder.AppendFormat(format, years, yearWord);
            }

            if (months > 0)
            {
                string monthWord = months.GetDeclension("-го месяца", "-х месяцов", "-и месяцев");
                builder.AppendFormat(format, months, monthWord);
            }

            if (days > 0 || builder.Length == 0)
            {
                string dayWord = days.GetDeclension("-го дня", "-х дней", "-и дней");
                builder.AppendFormat(format.TrimEnd(), days, dayWord);
            }

            return builder.ToString();
        }
    }
}
