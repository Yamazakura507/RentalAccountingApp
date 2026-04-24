using DataBaseProvaider;
using DataBaseProvaider.Attributes;
using DataBaseProvaider.Objects;
using System.Reflection;
using WinFormsComponents.Controls;

namespace WinFormsComponents.Classes
{
    /// <summary>
    /// Класс выполнения процеса
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Блокировка интерфейса формы
        /// </summary>
        /// <param name="loaderControl">Форма</param>
        /// <param name="progress">Исключить прогрес бар</param>
        public static void InterfaceLock(this Control loaderControl, Loader loader)
        {
            loaderControl.Enabled = false;
            loader.Visible = true;
        }

        /// <summary>
        /// Разблокировка интерфейса формы
        /// </summary>
        /// <param name="loaderControl">Форма</param>
        /// <param name="progress">Исключить прогрес бар</param>
        public static void InterfaceUnlock(this Control loaderControl, Loader loader)
        {
            loaderControl.Enabled = true;
            loader.Visible = false;
        }

        /// <summary>
        /// Получение формы контрола
        /// </summary>
        /// <param name="control">Контрол</param>
        /// <returns>Форма</returns>
        public static Form GetForm(this Control control)
        {
            if (control is not Form)
            {
                control = control.Parent.GetForm();
            }

            return (Form)control;
        }

        /// <summary>
        /// Получение информации о свойствах типа
        /// </summary>
        /// <param name="tags">Набор наименований свойств</param>
        /// <returns>Набор информации о свойствах типа</returns>
        public static IEnumerable<PropertyInfo> GetInfoParametrs<TModel>(this string[] tags)
        {
            Type type = typeof(TModel);

            foreach (string tag in tags)
            {
                yield return type.GetProperty(tag);
            }
        }

        /// <summary>
        /// Получение словаря состоящего из объектов перечисления и их коментариев
        /// </summary>
        /// <typeparam name="TEnum">Тип перечисления</typeparam>
        /// <returns>Словарь где Key - это элемент перечесления, а Value - его коментарий</returns>
        public static Dictionary<TEnum, string> GetCommitEnumDictionary<TEnum>() where TEnum : Enum
        {
            Dictionary<TEnum, string> result = new ();

            Type enumType = typeof(TEnum);

            foreach (string name in Enum.GetNames(enumType))
            {
                FieldInfo field = enumType.GetField(name);
                TEnum value = (TEnum)Enum.Parse(enumType, name);
                CommentAttribute commentAttr = field.GetCustomAttribute<CommentAttribute>();
                string description = commentAttr?.Description ?? name;

                result.Add(value, description);
            }

            return result;
        }

        /// <summary>
        /// Удаление из коллекции все элементы с <paramref name="limitUp"/> по <paramref name="limitEnd"/>
        /// </summary>
        /// <param name="collection">Коллекция элементов</param>
        /// <param name="limitUp">Верхняя граница(количество элементов не затрагиваемых сверху)</param>
        /// <param name="limitEnd">Нижняя граница(количество элементов не затрагиваемых снизу)</param>
        public static void CutToolStripCollection (this ToolStripItemCollection collection, int limitUp = 0, int limitEnd = 0)
        {
            if (collection.Count <= limitUp + limitEnd)
                return;

            for (int i = limitUp; i < collection.Count - limitEnd;)
            {
                collection.RemoveAt(i);
            }
        }

        /// <summary>
        /// Получение полного количества старниц записей модели
        /// </summary>
        /// <param name="conditions">Список ограничений</param>
        /// <returns>Количество страниц записей</returns>
        public async static Task<int> GetCountPage(this Type model, IEnumerable<ConditionsParametr> conditions, int limit)
        {
            double countPage = await model.GetResultByType<int>([conditions], nameof(DBProvider.Count));
            countPage = countPage / limit;

            return Convert.ToInt32(Math.Ceiling(countPage));
        }

        /// <summary>
        /// Получение случайного цвета
        /// </summary>
        /// <returns>Цвет</returns>
        public static Color RandomColor()
        {
            KnownColor[] names = (KnownColor[])Enum.GetValues(typeof(KnownColor));
            KnownColor randomName = names[Random.Shared.Next(names.Length)];
            Color randomColor = Color.FromKnownColor(randomName);

            return randomColor;
        }
    }
}
