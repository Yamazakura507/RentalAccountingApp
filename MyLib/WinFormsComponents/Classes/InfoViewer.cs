using Microsoft.VisualBasic;

namespace WinFormsComponents.Classes
{
    /// <summary>
    /// Класс информирования
    /// </summary>
    public static class InfoViewer
    {
        /// <summary>
        /// Вызвать диалог ошибки
        /// </summary>
        /// <param name="messege">Сообщение</param>
        public static void ErrrorMessege(this string messege)
        {
            MessageBox.Show(messege, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Вызвать диалог информирования
        /// </summary>
        /// <param name="messege">Сообщение</param>
        public static void InfoMessege(this string messege)
        {
            MessageBox.Show(messege, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Вызвать диалог предупреждения
        /// </summary>
        /// <param name="messege">Сообщение</param>
        public static void AlertMessege(this string messege)
        {
            MessageBox.Show(messege, "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Вызвать диалог ввода
        /// </summary>
        /// <param name="messege">Сообщение</param>
        /// <param name="defaultInput">Значение введенное по умолчанию</param>
        public static string InputMessege(this string messege, string defaultInput = null)
        {
            string input = Interaction.InputBox(messege, "Ввод", defaultInput);

            return input;
        }

        /// <summary>
        /// Вызвать диалог вопроса
        /// </summary>
        /// <param name="messege">Сообщение</param>
        /// <param name="buttons">Кнопки ответа</param>
        public static DialogResult QuestionMessege(this string messege, MessageBoxButtons buttons)
        {
            return MessageBox.Show(messege, "Вопрос", buttons, MessageBoxIcon.Question);
        }
    }
}
