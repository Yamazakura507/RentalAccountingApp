using System.Reflection;
using System.Text.RegularExpressions;

namespace DataBaseProvaider.Attributes
{
    /// <summary>
    /// Атрибут проверки интерфейса
    /// </summary>
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
        /// Наименование функции кастомной проверки
        /// </summary>
        /// <returns></returns>
        public string NameCustomCheckFunc { get; set; } = null;

        /// <summary>
        /// Сообщение при отрицательном результате проверки
        /// </summary>
        public string NotChecibleMessage = "Значение поля не соответствует условию проверки";

        /// <summary>
        /// Выполнение кастомной функции проверки
        /// </summary>
        /// <param name="instance">Объект функции</param>
        /// <returns>Результат выполнения функции, если функция не возвращает bool, вернётся false</returns>
        public async Task<bool> GetNameCustomCheckFunc(object instance) => await GetNameCustomCheckFunc(instance, NameCustomCheckFunc);

        /// <summary>
        /// Выполнение кастомной функции проверки
        /// </summary>
        /// <param name="instance">Объект функции</param>
        /// <param name="nameCustomCheckFunc">Имя исполняемой функции</param>
        /// <returns>Результат выполнения функции, если функция не возвращает bool, вернётся false</returns>
        public static async Task<bool> GetNameCustomCheckFunc(object instance, string nameCustomCheckFunc)
        {
            if (nameCustomCheckFunc != null)
            {
                MethodInfo method = instance.GetType().GetMethod(nameCustomCheckFunc,
                    BindingFlags.Public | BindingFlags.NonPublic |
                    BindingFlags.Instance | BindingFlags.Static);

                if (method != null)
                {
                    object result = method.Invoke(method.IsStatic ? null : instance, null);

                    if (result is Task<bool> taskBool) return await taskBool;
                    if (result is bool bolVal) return bolVal;
                }
            }

            return false;
        }
    }
}
