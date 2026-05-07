using DataBaseProvaider.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using WinFormsComponents.Classes.Interface;

namespace WinFormsComponents.Classes.Services
{
    /// <summary>
    /// Сервис заполнения ListView
    /// </summary>
    public class ListViewLoader : IListViewLoader
    {
        /// <summary>
        /// Цвет удаленных строк
        /// </summary>
        private Color removingRowColor = Color.MistyRose;

        /// <summary>
        /// Конструктор сервиса заполнения ListView
        /// </summary>
        /// <param name="removingRowColor">Цвет удаленных строк</param>
        public ListViewLoader(Color removingRowColor) 
        {
            this.removingRowColor = removingRowColor;
        }
        /// <summary>
        /// Конструктор сервиса заполнения ListView
        /// </summary>
        public ListViewLoader()
        {
        }

        /// <summary>
        /// Заполнение ListView данными
        /// </summary>
        public async Task PopulateListView(ListView listView, BindingList<object> items)
        {
            listView.BeginUpdate();
            listView.Items.Clear();

            bool isNum = listView.Columns[0].Name == "numColumn";
            int num = isNum ? (int)listView.Columns[0].Tag : 0;

            foreach (object item in items)
            {
                ListViewItem lvItem = await CreateListViewItem(item, num);
                listView.Items.Add(lvItem);
                if(isNum) num++;
            }

            listView.EndUpdate();
            AutoSizeColumns(listView);
        }

        /// <summary>
        /// Создание строки ListView
        /// </summary>
        /// <param name="item">Компонент примезки</param>
        /// <param name="properties">Список свойств модели</param>
        /// <returns>Элемент <see cref="ListView"/></returns>
        private async Task<ListViewItem> CreateListViewItem(object item, int num)
        {
            ListViewItem lvItem = new ();
            bool isNum = num != 0;
            PropertyInfo[] properties = item.GetType().GetProperties();

            if (isNum) lvItem.Text = num.ToString(); 

            foreach (PropertyInfo property in properties)
            {
                ViewModelAttribute vmAttribute = property.GetCustomAttribute<ViewModelAttribute>();
                DisplayFormatAttribute dfAttribute = property.GetCustomAttribute<DisplayFormatAttribute>();

                object rawValue = property.GetValue(item);

                if (rawValue is Task task)
                {
                    await task;

                    PropertyInfo resultProperty = task.GetType().GetProperty("Result");
                    rawValue = resultProperty?.GetValue(task);
                }

                if (vmAttribute != null)
                {
                    if (vmAttribute.Headline)
                    {
                        string value = rawValue.StringOutDBFormated(dfAttribute?.DataFormatString);

                        if (isNum) lvItem.SubItems.Add(value);
                        else lvItem.Text = value;
                    }
                    else if (vmAttribute.Image)
                    {
                        lvItem.ImageKey = rawValue?.ToString();
                    }
                    else if (vmAttribute.RemovingFlag && !Convert.ToBoolean(property.GetValue(item)))
                    {
                        lvItem.BackColor = removingRowColor;
                    }
                    else if (vmAttribute.BackColor && lvItem.BackColor != removingRowColor)
                    {
                        lvItem.BackColor = (Color)rawValue;
                    }
                    else
                    {
                        lvItem.SubItems.Add(rawValue.StringOutDBFormated(dfAttribute?.DataFormatString));
                    }
                }
                else
                {
                    lvItem.SubItems.Add(rawValue.StringOutDBFormated(dfAttribute?.DataFormatString));
                }

                lvItem.Tag = item;
            }

            return lvItem;
        }

        /// <summary>
        /// Определение ширины коклонки для списка относительно заголовка и контента
        /// </summary>
        /// <param name="listView">Список, объект <see cref="ListView"/></param>
        public static void AutoSizeColumns(ListView listView)
        {
            if (listView.Items.Count == 0) 
            {
                listView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                return;
            };

            foreach (ColumnHeader column in listView.Columns)
            {
                column.Width = -2; // AutoSize по заголовку
                int headerWidth = column.Width;

                column.Width = -1; // AutoSize по контенту
                int contentWidth = column.Width;

                column.Width = Math.Max(headerWidth, contentWidth);
            }
        }
    }
}
