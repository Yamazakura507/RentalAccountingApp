using DataBaseProvaider.Enums;

namespace DataBaseProvaider.Objects
{
    /// <summary>
    /// Клас надстройка над классом базовой фильтрации, дает возможность указать подробные параметры для поиска по левинштейну
    /// </summary>
    public class LevenshteinSupplement : ConditionsParametr
    {
        private ConditionalOperators poperator = ConditionalOperators.Levenshtein;
        private bool isSerhing = true;

        /// <summary>
        /// Условный оператор
        /// </summary>
        public override ConditionalOperators Operator 
        { 
            get => poperator; 
            set => poperator = ConditionalOperators.Levenshtein;
        }

        /// <summary>
        /// Указвает, что параметр следует считать поисковым и назначать в Value значение из поля поиск
        /// </summary>
        public override bool IsSerhing 
        { 
            get => isSerhing; 
            set => isSerhing = true; 
        }

        /// <summary>
        /// Максимальная дистанция ливенштейна
        /// </summary>
        public int? MaxDistance { get; set; } = null;

        /// <summary>
        /// Приоритет сортировки по ростаянию ливенштейна
        /// </summary>
        public OrderLevenshteineSupplement OrderPriority { get; set; } = OrderLevenshteineSupplement.None;

        /// <summary>
        /// Тип сортировки
        /// </summary>
        public OrderType OrderType { get; set; } = OrderType.Asc;

        /// <summary>
        /// Точное указание приоритета сортировки, работает только при <see cref="OrderLevenshteineSupplement.Position"/>
        /// </summary>
        public int OrderPriorityPosition { get; set; } = 0;
    }
}
