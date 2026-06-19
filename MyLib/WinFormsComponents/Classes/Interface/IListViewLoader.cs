using System.ComponentModel;

namespace WinFormsComponents.Classes.Interface
{
    /// <summary>
    /// Интерфейс сервиса заполнения ListView
    /// </summary>
    internal interface IListViewLoader
    {
        /// <summary>
        /// Режим ленивой загрузки
        /// </summary>
        public bool IsYieldMode { get; set; }
        /// <summary>
        /// Заполнение ListView данными
        /// </summary>
        Task PopulateListView(ListView listView, BindingList<dynamic> items);
    }
}
