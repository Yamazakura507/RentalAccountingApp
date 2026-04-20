using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DataBaseProvaider.Objects
{
    public class CollectionParametrs : IDisposable, INotifyPropertyChanged
    {
        private IEnumerable<ConditionsParametr> conditions = new List<ConditionsParametr>();
        private IEnumerable<OrderParametr> orders = new List<OrderParametr>();
        private int serhingParametrsCount = 0;
        private int limit = 0;
        private int offset = 0;

        /// <summary>
        /// Набор параметров фильтрации
        /// </summary>
        public IEnumerable<ConditionsParametr> Conditions
        {
            get => conditions;
            set
            {
                if (conditions != value)
                {
                    conditions = value ?? new List<ConditionsParametr>();
                    serhingParametrsCount = conditions.Count(i => i?.IsSerhing ?? false);
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Набор параметров сортировки
        /// </summary>
        public IEnumerable<OrderParametr> Orders 
        { 
            get => orders;
            set
            {
                if (orders != value)
                {
                    orders = value ?? new OrderParametr[0];
                    OnPropertyChanged();
                }
            } 
        }

        /// <summary>
        /// Ограничение вывода коллекции с конца
        /// </summary>
        public int Limit 
        { 
            get => limit;
            set
            {
                if (limit != value)
                {
                    limit = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Ограничение вывода коллекции с начала
        /// </summary>
        public int Offset 
        {
            get => offset;
            set
            {
                if (offset != value)
                {
                    offset = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Количество поисковых параметров
        /// </summary>
        public int SerhingParametrsCount 
        {
            get => serhingParametrsCount;
            set
            {
                if (serhingParametrsCount != value)
                {
                    serhingParametrsCount = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Деструктор/Очистка памяти от объекта
        /// </summary>
        public void Dispose() => GC.SuppressFinalize(this);

        /// <summary>
        /// Реализация сообщений об обновления свойств
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
