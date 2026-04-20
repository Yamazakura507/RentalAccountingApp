using System.Text;
using System.Text.RegularExpressions;

namespace WinFormsComponents.Classes
{
    /// <summary>
    /// Ограничитель - позволяет задать ограничение для полей ввода, а так же проверять текстовые поля
    /// </summary>
    public static class TextBoxRestriction
    {
        /// <summary>
        /// Цвет текстового поля при отрицательном результате проверки
        /// </summary>
        public static Color AlertBursh = Color.MistyRose;

        /// <summary>
        /// Цвет текстового поля при положительном результате проверки
        /// </summary>
        public static Color GoodBursh = Color.White;

        /// <summary>
        /// Ограничени на ввод чисел(разрешенно вводить только целые числа)
        /// </summary>
        /// <param name="e">Событие <see cref="KeyPressEventArgs"/>Событие ввода</param>
        public static void NumRestrictionTextBox(this KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) &&
                e.KeyChar != (char)Keys.Back &&
                e.KeyChar != (char)Keys.Delete &&
                e.KeyChar != (char)Keys.Left &&
                e.KeyChar != (char)Keys.Right &&
                e.KeyChar != (char)Keys.Up &&
                e.KeyChar != (char)Keys.Down &&
                e.KeyChar != (char)Keys.LShiftKey &&
                e.KeyChar != (char)Keys.RShiftKey)
            {
                e.Handled = true;
                return;
            }
        }

        /// <summary>
        /// Ограничени на ввод дробных чисел(разрешенно вводить дробные и целые числа)
        /// </summary>
        /// <param name="e">Событие <see cref="KeyPressEventArgs"/>Событие ввода</param>
        public static void NumDecimalRestrictionTextBox(this KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Decimal) return;

            NumRestrictionTextBox(e);
        }

        /// <summary>
        /// Ограничение на ввод букв(разрешенно вводить только буквенные символы)
        /// </summary>
        /// <param name="e">Событие <see cref="KeyPressEventArgs"/>Событие ввода</param>
        public static void TextRestrictionTextBox(this KeyPressEventArgs e)
        {
            if (!Char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Задержка для визуального эффекта при отрицательном результате проверки
        /// </summary>
        /// <param name="textBox">Поле ввода</param>
        /// <param name="timeout">Время задержки(в милисекундах)</param>
        /// <returns>Процес</returns>
        async private static Task Sleep(this TextBox textBox, int timeout)
        {
            await Task.Delay(timeout);
            textBox.BackColor = GoodBursh;
        }

        /// <summary>
        /// Проверка поля ввода на пустоту
        /// </summary>
        /// <param name="textBox">Поле ввода</param>
        /// <param name="timeout">Время задержки(в милисекундах, по умолчанию 3 секунды)</param>
        /// <returns>Результат проверки</returns>
        async public static Task<bool> TextEmptyTextBox(this TextBox textBox, int timeout = 3000)
        {
            if (String.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.BackColor = AlertBursh;
                textBox.Clear();
                await Sleep(textBox, timeout);
            }
            else
            {
                textBox.BackColor = GoodBursh;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Проверка поля ввода на соответствие регулярному выражению
        /// </summary>
        /// <param name="textBox">Поле ввода</param>
        /// <param name="regex">Регулярное выражение для проверки</param>
        /// <param name="timeout">Время задержки(в милисекундах, по умолчанию 3 секунды)</param>
        /// <returns>Результат проверки</returns>
        async public static Task<bool> RegexTextBoxCheck(this TextBox textBox, Regex regex, int timeout = 3000)
        {
            if (!regex.IsMatch(textBox.Text.Trim()))
            {
                textBox.BackColor = AlertBursh;
                textBox.Clear();
                await Sleep(textBox, timeout);

                return false;
            }

            textBox.BackColor = GoodBursh;
            return true;
        }

        /// <summary>
        /// Генерация случайной строки(кода)
        /// </summary>
        /// <param name="lenght">Длинна строки</param>
        /// <returns>Случайная строка</returns>
        public static string GeneratorChufleString(int lenght)
        {
            string symbols = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            Random random = new Random();

            string rand = null;


            rand = Enumerable.Range(0, lenght)
                    .Aggregate(
                        new StringBuilder(),
                        (sb, n) => sb.Append(symbols[random.Next(0, symbols.Length)]),
                        sb => sb.ToString());


            return rand;
        }
    }
}
