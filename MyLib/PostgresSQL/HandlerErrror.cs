namespace PostgresSQL
{
    /// <summary>
    /// Класс Обработчик ошибок
    /// </summary>
    public class HandlerErrror : IDisposable
    {
        private string innerErrorFormat = "{0}\n\nВнутреняя ошибка:\n{1}";

        /// <summary>
        /// Обработчик ошибок, назначте делегат прогресс
        /// </summary>
        public IProgress<string> ErrorReporter { get; set; }

        /// <summary>
        /// Если true обработчик ошибок будет выводить сообщение вида [Ошибка] [Новая строка] [Новая строка] [Внутряняя ошибка] или по формату если такой задан
        /// </summary>
        public bool HandlerInnerError { get; set; } = false;

        /// <summary>
        /// Формат вывода сообщения об ошибке при HandlerInnerError True по умолчанию "{0}\n\nВнутреняя ошибка:\n{1}"
        /// </summary>
        public string InnerErrorFormat
        {
            get => innerErrorFormat;
            set => innerErrorFormat = String.IsNullOrEmpty(value) ? "{0}\n\nВнутреняя ошибка:\n{1}" : value;
        }

        /// <summary>
        /// Сборщик сообщения об ошибке
        /// </summary>
        /// <param name="ex">Объект ошибки</param>
        /// <returns>Сообщение ошибки</returns>
        private string BildErrorMessage(Exception ex) => HandlerInnerError ? String.Format(innerErrorFormat, ex.Message, ex.InnerException?.Message) : ex.Message;

        /// <summary>
        /// Вывод ошибки в привязаный обработчик
        /// </summary>
        /// <param name="ex">Ошибка</param>
        public void ErrorReport(Exception ex)
        {
            if (this.ErrorReporter != null)
            {
                this.ErrorReporter?.Report(BildErrorMessage(ex));
            }
        }

        /// <summary>
        /// Сборщик мусора/Деструктор
        /// </summary>
        public void Dispose() => GC.SuppressFinalize(this);
    }
}
