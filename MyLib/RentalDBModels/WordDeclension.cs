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
        public static string GetDeclension(int number, string form1, string form2, string form5)
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
    }
}
