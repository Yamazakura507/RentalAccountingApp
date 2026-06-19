namespace DataBaseProvaider.Attributes
{
    /// <summary>
    /// Атрибут для вынесения какого либо текста
    /// </summary>
    public class CommentAttribute : Attribute
    {
        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Атрибут для вынесения какого либо текста
        /// </summary>
        /// <param name="description">Описание</param>
        public CommentAttribute(string description) 
        {
            Description = description;
        }
    }
}
