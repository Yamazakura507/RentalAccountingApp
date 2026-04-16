using System.Reflection;

namespace DataBaseProvaider.Objects
{
    /// <summary>
    /// Представляет маппинг между свойством модели и столбцом в таблице данных
    /// </summary>
    /// <remarks>
    /// Класс используется для кэширования информации о свойствах модели,
    /// чтобы избежать повторного рефлексивного анализа типов при каждом преобразовании.
    /// </remarks>
    public class PropertyMap
    {
        public PropertyInfo Property { get; }

        public string ColumnName { get; }

        public Type PropertyType { get; }

        /// <summary>
        /// Конструктор класса <see cref="PropertyMap"/>
        /// </summary>
        /// <param name="property">Информация о свойстве модели</param>
        /// <param name="columnName">Имя столбца в таблице данных</param>
        public PropertyMap(PropertyInfo property, string columnName)
        {
            Property = property;
            ColumnName = columnName;
            PropertyType = property.PropertyType;
        }
    }
}
