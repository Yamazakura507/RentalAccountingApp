using System.Text.RegularExpressions;

namespace WinFormsComponents.Classes.Model
{
    /// <summary>
    /// Объект настройки фильтра
    /// </summary>
    public class SettingFilter
    {
        /// <summary>
        /// Верхний ограничитель фильтра
        /// </summary>
        public object Maximum { get; set; }
        /// <summary>
        /// Нижний ограничитель фильтра
        /// </summary>
        public object Minimum { get; set; }
        /// <summary>
        /// Регулярное ограничение
        /// </summary>
        public Regex Regex {  get; set; }

        /// <summary>
        /// Конструктор настройки фильтра
        /// </summary>
        /// <param name="maximum">Верхний ограничитель фильтра</param>
        /// <param name="minimum">Нижний ограничитель фильтра</param>
        public SettingFilter(object maximum, object minimum)
        {
            Maximum = maximum;
            Minimum = minimum;
            Regex = null;
        }

        /// <summary>
        /// Конструктор настройки фильтра
        /// </summary>
        /// <param name="regex">Регулярный ограничитель</param>
        public SettingFilter(Regex regex)
        {
            Maximum = null;
            Minimum = null;
            Regex = regex;
        }
    }
}
