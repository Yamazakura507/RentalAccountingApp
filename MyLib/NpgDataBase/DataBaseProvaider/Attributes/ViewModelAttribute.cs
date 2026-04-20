namespace DataBaseProvaider.Attributes
{
    /// <summary>
    /// Автрибут меток модели для взаимодействия с DB элементами
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class ViewModelAttribute : Attribute
    {
        /// <summary>
        /// Метка свойства, которое будет указываться в заголовке отображения
        /// </summary>
        public bool Headline { get; set; } = false;
        /// <summary>
        /// Метка свойства хранящего ImageKey значение
        /// </summary>
        public bool Image { get; set; } = false;
        /// <summary>
        /// Метка сокрытия свойства от взаимодествия с пользователем
        /// </summary>
        public bool ViewHide { get; set; } = false;
        /// <summary>
        /// Метка свойства отвечающего за статус актуальности данных(удаленности)
        /// </summary>
        public bool RemovingFlag { get; set; } = false;
        /// <summary>
        /// Метка свойства включающая его в компонент фильтрации
        /// </summary>
        public bool FilterOn { get; set; } = false;
    }
}
