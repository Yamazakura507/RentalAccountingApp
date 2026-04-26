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
        public void PopulateListView(ListView listView, Type modelType, BindingList<object> items)
        {
            listView.BeginUpdate();
            listView.Items.Clear();

            PropertyInfo[] propertyCache = modelType.GetProperties();
            bool isNum = listView.Columns[0].Name == "numColumn";
            int num = isNum ? (int)listView.Columns[0].Tag : 0;

            foreach (object item in items)
            {
                ListViewItem lvItem = CreateListViewItem(item, propertyCache, num);
                listView.Items.Add(lvItem);
                if(isNum) num++;
            }

            listView.EndUpdate();
            listView.AutoResizeColumns(items.Count == 0 ? ColumnHeaderAutoResizeStyle.HeaderSize : ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        /// <summary>
        /// Создание строки ListView
        /// </summary>
        /// <param name="item">Компонент примезки</param>
        /// <param name="properties">Список свойств модели</param>
        /// <returns>Элемент <see cref="ListView"/></returns>
        private ListViewItem CreateListViewItem(object item, PropertyInfo[] properties, int num)
        {
            ListViewItem lvItem = new ();
            bool isNum = num != 0;

            if (isNum) lvItem.Text = num.ToString(); 

            foreach (PropertyInfo property in properties)
            {
                ViewModelAttribute vmAttribute = property.GetCustomAttribute<ViewModelAttribute>();
                DisplayFormatAttribute dfAttribute = property.GetCustomAttribute<DisplayFormatAttribute>();

                if (vmAttribute != null)
                {
                    if (vmAttribute.Headline)
                    {
                        string value = property.GetValue(item).StringOutDBFormated(dfAttribute?.DataFormatString);

                        if (isNum) lvItem.SubItems.Add(value);
                        else lvItem.Text = value;
                    }
                    else if (vmAttribute.Image)
                    {
                        lvItem.ImageKey = property.GetValue(item)?.ToString();
                    }
                    else if (vmAttribute.RemovingFlag && !Convert.ToBoolean(property.GetValue(item)))
                    {
                        lvItem.BackColor = removingRowColor;
                    }
                    else
                    {
                        lvItem.SubItems.Add(property.GetValue(item).StringOutDBFormated(dfAttribute?.DataFormatString));
                    }
                }
                else
                {
                    lvItem.SubItems.Add(property.GetValue(item).StringOutDBFormated(dfAttribute?.DataFormatString));
                }

                lvItem.Tag = item;
            }

            return lvItem;
        }
    }
}
