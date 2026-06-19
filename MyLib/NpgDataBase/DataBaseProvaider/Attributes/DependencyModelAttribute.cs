namespace DataBaseProvaider.Attributes
{
    /// <summary>
    /// Атрибут зависимости модели
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class DependencyModelAttribute : Attribute
    {
        /// <summary>
        /// Указывает на то что поле является главным внешним ключом
        /// </summary>
        public bool IsForigen = false;

        /// <summary>
        /// Указывает на подчиненную зависимость внешнему ключу
        /// </summary>
        public bool IsDependency = false;
    }
}
