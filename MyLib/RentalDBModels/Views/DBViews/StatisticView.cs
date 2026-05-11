using DataBaseProvaider.Attributes;
using System.ComponentModel;

namespace RentalDBModels.Views.DBViews
{
    public class StatisticView
    {
        [ViewModel(Headline = true)]
        [Description("Квартальный период")]
        public string HeaderTitle => Quarter > 4 
            ? String.Format("За период с {0} года по {1} год", Quarter, Year)
            : String.Format("{0} квартал {1} года", Quarter.ToOrdinal(), Year);

        public int Year { get; set; }

        public int Quarter { get; set; }

        [Description("Доход")]
        public double Income { get; set; }

        [Description("Сумма аренды")]
        public double RentalSum { get; set; }

        [Description("Задолжность")]
        public double DebetSum { get; set; }

        [Description("Маржа")]
        public double Profit { get; set; }
    }
}
