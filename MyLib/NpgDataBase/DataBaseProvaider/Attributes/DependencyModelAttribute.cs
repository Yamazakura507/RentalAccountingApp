namespace DataBaseProvaider.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class DependencyModelAttribute : Attribute
    {
        public bool IsForigen = false;

        public bool IsDependency = false;
    }
}
