using System.Text.RegularExpressions;

namespace DataBaseProvaider.Attributes
{
    public class CheckAttribute : Attribute
    {
        /// <summary>
        /// Строка регулярного выражения
        /// </summary>
        public string RegexPattern { get; set; } = String.Empty;

        /// <summary>
        /// Дополнительные опции регулярного выражения
        /// </summary>
        public RegexOptions RegexOptions { get; set; } = RegexOptions.None;

        /// <summary>
        /// Проверка на соответствие регулярному выражению
        /// </summary>
        public Regex RegexCheck => new (RegexPattern, RegexOptions);

        /// <summary>
        /// Параметр указывающий, что значение может принимать пустой параметр
        /// </summary>
        public bool IsNull { get; set; } = false;

        /// <summary>
        /// Сообщение при отрицательном результате проверки
        /// </summary>
        public string NotChecibleMessage = "Значение поля не соответствует условию проверки";
    }
}
