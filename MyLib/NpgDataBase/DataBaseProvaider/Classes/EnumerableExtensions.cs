using System.Runtime.CompilerServices;

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

        /// <summary>
        /// Преобразование масива кортежей в словарь
        /// </summary>
        /// <typeparam name="TKey">Тип ключа</typeparam>
        /// <typeparam name="TValue">Тип значения</typeparam>
        /// <param name="items">Масив кортежей</param>
        /// <returns>Словарь на основе масива кортежей</returns>
        public static Dictionary<TKey, TValue> ToDictionary<TKey,TValue>(this (TKey Key, TValue Value)[]? items) => items?.ToDictionary(x => x.Key, x => x.Value) ?? new();

        /// <summary>
        /// Преобразование масива строк в словарь по разделительному символу
        /// </summary>
        /// <param name="items">Масив строк вида [ Ключ1 Символ разделитель Значение1,... ]</param>
        /// <param name="splitSymbol">Символ разделитель</param>
        /// <returns>Словарь на основе масива строк</returns>
        public static Dictionary<string, string> ToDictionary(this string[]? items, char splitSymbol = '=')
        {
            return items?.Select(p => p.Split(splitSymbol))
                .Where(parts => parts.Length == 2)
                .ToDictionary(parts => parts[0], parts => parts[1]) ?? new();
        }
    }
}
