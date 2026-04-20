using DataBaseProvaider;
using DataBaseProvaider.Attributes;
using DataBaseProvaider.Enums;
using DataBaseProvaider.Objects;
using Newtonsoft.Json;
using Npgsql;
using System.ComponentModel;
using System.Reflection;
using WinFormsComponents.Classes.Model;

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
        /// <param name="loaderForm">Форма</param>
        /// <param name="progress">Исключить прогрес бар</param>
        public static void InterfaceLock(this Form loaderForm, ToolStripProgressBar progress = null)
        {
            loaderForm.Enabled = false;
            progress?.Visible = true;
            progress?.Enabled = true;
        }

        /// <summary>
        /// Разблокировка интерфейса формы
        /// </summary>
        /// <param name="loaderForm">Форма</param>
        /// <param name="progress">Исключить прогрес бар</param>
        public static void InterfaceUnlock(this Form loaderForm, ToolStripProgressBar progress = null)
        {
            loaderForm.Enabled = true;
            progress?.Visible = false;
            progress?.Enabled = false;
        }

        /// <summary>
        /// Блокировка интерфейса формы
        /// </summary>
        /// <param name="loaderForm">Форма</param>
        /// <param name="progress">Исключить прогрес бар</param>
        public static void InterfaceLock(this Form loaderForm, ProgressBar progress = null)
        {
            loaderForm.Enabled = false;
            progress?.Visible = true;
            progress?.Enabled = true;
        }

        /// <summary>
        /// Разблокировка интерфейса формы
        /// </summary>
        /// <param name="loaderForm">Форма</param>
        /// <param name="progress">Исключить прогрес бар</param>
        public static void InterfaceUnlock(this Form loaderForm, ProgressBar progress = null)
        {
            loaderForm.Enabled = true;
            progress?.Visible = false;
            progress?.Enabled = false;
        }

        public static void Save(this ConnectionElement[] connections)
        {
            string updatedJson = JsonConvert.SerializeObject(connections);
            File.WriteAllText("connection_list.json", updatedJson);

            DBProvider.NpgsqlProvider = new(ConnectionInfo.ActiveConnection?.ConnectionBuilder ?? ConnectionInfo.DefaultConnection);
            DBProvider.NpgsqlProvider.HandlerErrror.ErrorReporter = new Progress<string>(async message => InfoViewer.ErrrorMessege(message));
        }

        /// <summary>
        /// Проверка соединения
        /// </summary>
        /// <param name="connectionBuilder">Строка соединения</param>
        /// <returns>Результат проверкаи</returns>
        public static bool IsCheckConection(this NpgsqlConnectionStringBuilder connectionBuilder)
        {
            using (NpgsqlConnection connection = new(connectionBuilder.ToString()))
            {
                try
                {
                    connection.Open();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
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
    }
}
