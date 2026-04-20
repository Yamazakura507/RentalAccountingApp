namespace DataBaseProvaider.Attributes
{
    public class CommentAttribute : Attribute
    {
        public string Description { get; set; }

        public CommentAttribute(string description) 
        {
            Description = description;
        }
    }
}
