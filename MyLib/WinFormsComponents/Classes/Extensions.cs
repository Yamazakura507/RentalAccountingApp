using Newtonsoft.Json;
using Npgsql;
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

        public static Form GetForm(this Control control)
        {
            if (control is not Form)
            {
                control = control.Parent.GetForm();
            }

            return (Form)control;
        }
    }
}
