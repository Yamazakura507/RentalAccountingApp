using DataBaseProvaider;
using DataBaseProvaider.Attributes;
using System.ComponentModel;
using System.Reflection;
using WinFormsComponents.Classes.Enums;
using WinFormsComponents.Classes.Interface;
using WinFormsComponents.Classes.Model;
using WinFormsComponents.Controls;

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

            foreach (object item in items)
            {
                ListViewItem lvItem = CreateListViewItem(item, propertyCache);
                listView.Items.Add(lvItem);
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
        private ListViewItem CreateListViewItem(object item, PropertyInfo[] properties)
        {
            ListViewItem lvItem = new ();

            foreach (PropertyInfo property in properties)
            {
                ViewModelAttribute vmAttribute = property.GetCustomAttribute<ViewModelAttribute>();

                if (vmAttribute != null)
                {
                    if (vmAttribute.Headline)
                    {
                        lvItem.Text = property.GetValue(item)?.ToString() ?? string.Empty;
                    }
                    else if (vmAttribute.Image)
                    {
                        lvItem.ImageKey = property.GetValue(item)?.ToString();
                    }
                    else if (vmAttribute.RemovingFlag && !Convert.ToBoolean(property.GetValue(item)))
                    {
                        lvItem.BackColor = removingRowColor;
                    }
                }
                else
                {
                    lvItem.SubItems.Add(property.GetValue(item)?.ToString() ?? string.Empty);
                }

                lvItem.Tag = item;
            }

            return lvItem;
        }
    }
}
