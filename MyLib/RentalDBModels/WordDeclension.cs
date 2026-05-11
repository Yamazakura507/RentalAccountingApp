namespace RentalDBModels
{
    public static class WordDeclension
    {
        /// <summary>
        /// Универсальный метод склонения для трех форм слова
        /// </summary>
        /// <param name="number">Число</param>
        /// <param name="form1">Форма для 1 склонения</param>
        /// <param name="form2">Форма для 2 склонения</param>
        /// <param name="form5">Форма для 3 склонения</param>
        public static string GetDeclension(this int number, string form1, string form2, string form5)
        {
            int lastDigit = number % 10;
            int lastTwoDigits = number % 100;

            if (lastTwoDigits >= 11 && lastTwoDigits <= 19)
                return form5;

            if (lastDigit == 1)
                return form1;

            if (lastDigit >= 2 && lastDigit <= 4)
                return form2;

            return form5;
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
