namespace RentalDBModels
{
    public static class WordDeclension
    {
        /// <summary>
        /// Универсальный метод склонения для трех форм слова
        /// </summary>
        /// <param name="number">Число</param>
        /// <param name="formFirst">Форма для 1 склонения</param>
        /// <param name="formSecond">Форма для 2 склонения</param>
        /// <param name="formThird">Форма для 3 склонения</param>
        public static string GetDeclension(this int number, string formFirst, string formSecond, string formThird)
        {
            number = Math.Abs(number) % 100;
            if (number >= 11 && number <= 19) return formThird;

            number %= 10;
            if (number == 1) return formFirst;
            if (number >= 2 && number <= 4) return formSecond;
            return formThird;
        }

        /// <summary>
        /// Получение окончания порядкового номера
        /// </summary>
        /// <param name="number">номер</param>
        /// <returns>Строка с номером и окончанием</returns>
        public static string ToOrdinal(this int number)
        {
            if (number <= 0)
                return number.ToString();

            int lastDigit = number % 10;
            int lastTwoDigits = number % 100;

            if (lastTwoDigits >= 11 && lastTwoDigits <= 19)
                return $"{number}-ый";

            return lastDigit > 5 && lastDigit < 9 || lastDigit == 2 ? $"{number}-ой" : $"{number}-ый";
        }
    }
}
