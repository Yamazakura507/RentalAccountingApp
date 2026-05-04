using DataBaseProvaider.Enums;

namespace DataBaseProvaider.Attributes
{
    /// <summary>
    /// Атрибут привязки
    /// </summary>
    public class DependencyAttribute : Attribute
    {
        /// <summary>
        /// Подпись зависимости
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Иконка зависимости
        /// </summary>
        public string ImageKey { get; set; }

        /// <summary>
        /// Тип модели зависимости
        /// </summary>
        public Type DependencyModelType { get; set; }

        /// <summary>
        /// Тип модели коллекции зависимости
        /// </summary>
        public Type DependencyViewType { get; set; }

        /// <summary>
        /// Тип зависимости
        /// </summary>
        public DependencyType DependencyType { get; set; }

        public DependencyAttribute(string title, Type dependencyViewType, DependencyType dependencyType)
        {
            Title = title;
            DependencyViewType = dependencyViewType;
            DependencyType = dependencyType;
        }
    }
}
