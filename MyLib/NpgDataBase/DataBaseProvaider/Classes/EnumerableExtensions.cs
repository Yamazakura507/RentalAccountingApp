namespace DataBaseProvaider.Classes
{
    /// <summary>
    /// Класс <see cref="IEnumerable{T}"/> расширений
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Добавление элемента в по указаной позиции
        /// </summary>
        /// <typeparam name="T">Тип элементов последовательности</typeparam>
        /// <param name="source">Последовательность</param>
        /// <param name="item">Новый элемент</param>
        /// <param name="index">Позиция вставки</param>
        /// <returns>Последовательность с новым элементом по позиции</returns>
        /// <remarks>
        /// <para>Если длинна последовательности короче указаной позиции, элемент будет вставлен в конец последовательности</para>
        /// <para>Если позиция меньше минимальной позиции элемента, элемент будет вставлен в начало последовательности</para>
        /// </remarks>
        public static IEnumerable<T> InsertAt<T>(this IEnumerable<T> source, T item, int index)
        {
            if (index < 0)
            {
                index = 0;
            }

            int currentIndex = 0;
            bool inserted = false;

            foreach (T element in source)
            {
                if (currentIndex == index)
                {
                    yield return item;

                    inserted = true;
                }

                yield return element;

                currentIndex++;
            }

            if (!inserted)
            {
                yield return item;
            }
        }
    }
}
