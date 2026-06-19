namespace DataBaseProvaider.Attributes
{
    /// <summary>
    /// Указывает на атрибуты пропускаемые при сборке БД контейнеров
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class SkipPropertyAttribute : System.Attribute
    {
    }
}
