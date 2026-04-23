using System.ComponentModel;
using WinFormsComponents.Classes.Enums;
using WinFormsComponents.Controls;

namespace WinFormsComponents.Classes.Interface
{
    /// <summary>
    /// Интерфейс сервиса заполнения ListView
    /// </summary>
    internal interface IListViewLoader
    {
        /// <summary>
        /// Заполнение ListView данными
        /// </summary>
        void PopulateListView(ListView listView, Type modelType, BindingList<dynamic> items);
    }
}
