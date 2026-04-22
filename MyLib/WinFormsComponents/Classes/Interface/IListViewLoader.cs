using System.ComponentModel;

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
