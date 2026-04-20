namespace DataBaseProvaider.Classes.Abstract
{
    public abstract class BaseParametrCollection
    {
        private static readonly Dictionary<Type, int> typeCounters = new();
        private static readonly object lockObject = new();

        public int Id { get; set; }

        protected BaseParametrCollection()
        {
            Id = GenerateId();
        }

        /// <summary>
        /// Генератор Id
        /// </summary>
        /// <returns>Уникальный Id</returns>
        private int GenerateId()
        {
            lock (lockObject)
            {
                Type type = GetType();

                if (!typeCounters.ContainsKey(type))
                {
                    typeCounters[type] = 0;
                }

                return ++typeCounters[type];
            }
        }

        /// <summary>
        /// Сбросить счетчик для конкретного типа
        /// </summary>
        public static void ResetCounter<T>() where T : BaseParametrCollection
        {
            lock (lockObject)
            {
                typeCounters[typeof(T)] = 0;
            }
        }
    }
}
