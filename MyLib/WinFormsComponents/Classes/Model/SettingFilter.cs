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
        /// Конструктор настройки фильтра
        /// </summary>
        /// <param name="maximum">Верхний ограничитель фильтра</param>
        /// <param name="minimum">Нижний ограничитель фильтра</param>
        public SettingFilter(object maximum, object minimum)
        {
            Maximum = maximum;
            Minimum = minimum;
        }
    }
}
