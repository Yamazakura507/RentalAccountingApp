using DataBaseProvaider.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using WinFormsComponents.Classes.Interface;
using WinFormsComponents.Classes.Services.NativeSeervice;

namespace WinFormsComponents.Classes.Services
{
    /// <summary>
    /// Сервис заполнения ListView
    /// </summary>
    public class ListViewLoader : IListViewLoader
    {
        /// <summary>
        /// Режим ленивой загрузки
        /// </summary>
        public bool IsYieldMode { get; set; } = false;

        /// <summary>
        /// Цвет удаленных строк
        /// </summary>
        private Color removingRowColor = Color.MistyRose;

        //Поля для виртуального режима
        private BindingList<object> virtualItems;
        private ListView virtualListView;
        private int virtualStartIndex = 0;
        private int virtualEndIndex = 0;
        private const int BufferSize = 10;
        private const int UpdateScrollBufferSize = 5;
        private int lastTopIndex = 0;
        private bool isVirtualModeActive = false;
        private bool isLoading = false;

        //Делегат для обработки сообщений
        private ListViewNativeWindow listViewNativeWindow;

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
            if (!IsYieldMode)
            {
                await PopulateNormalMode(listView, items);
            }
            else
            {
                await SetupVirtualMode(listView, items);
            }
        }

        /// <summary>
        /// Заполнение ListView данными в обычном режиме
        /// </summary>
        public async Task PopulateNormalMode(ListView listView, BindingList<object> items)
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
        /// Заполнение ListView данными в ленивом режиме
        /// </summary>
        private async Task SetupVirtualMode(ListView listView, BindingList<object> items)
        {
            virtualItems = items;
            virtualListView = listView;
            virtualStartIndex = 0;
            virtualEndIndex = 0;
            lastTopIndex = 0;

            listView.VirtualMode = true;
            listView.VirtualListSize = items.Count;

            listView.Items.Clear();

            for (int i = 0; i < items.Count; i++)
            {
                listView.Items.Add(new ListViewItem());
            }

            listViewNativeWindow = new ListViewNativeWindow(listView);
            listViewNativeWindow.ReleaseHandle();

            listView.RetrieveVirtualItem += OnRetrieveVirtualItem;
            listView.CacheVirtualItems += OnCacheVirtualItems;
            listView.HandleCreated += ListViewOnHandleCreated;
            listView.HandleDestroyed += ListViewOnHandleDestroyed;

            if (listView.IsHandleCreated)
            {
                SubscribeToScroll(listView);
            }

            isVirtualModeActive = true;

            await LoadVisibleRange(0);
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
                        if (vmAttribute.ViewHide) continue;

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

        private void SubscribeToScroll(ListView listView)
        {
            if (listViewNativeWindow != null)
            {
                listViewNativeWindow.ReleaseHandle();
            }

            listViewNativeWindow = new ListViewNativeWindow(listView);
            listViewNativeWindow.ScrollMessageReceived += OnScrollMessageReceived;
        }

        private void UnsubscribeFromScroll()
        {
            if (listViewNativeWindow != null)
            {
                listViewNativeWindow.ScrollMessageReceived -= OnScrollMessageReceived;
                listViewNativeWindow.ReleaseHandle();
                listViewNativeWindow = null;
            }
        }

        /// <summary>
        /// Загрузка видимого диапазона
        /// </summary>
        /// <param name="centerIndex">Индекс центрового элемента</param>
        /// <returns>Процес</returns>
        private async Task LoadVisibleRange(int centerIndex)
        {
            if (virtualItems == null || virtualListView == null) return;

            int visibleCount = GetVisibleItemsCount();
            int startIndex = Math.Max(0, centerIndex - BufferSize);
            int endIndex = Math.Min(virtualItems.Count - 1, centerIndex + visibleCount + BufferSize);

            await LoadRange(startIndex, endIndex);
        }

        /// <summary>
        /// Получение диапазона вывода от начального до конечного индекса
        /// </summary>
        /// <param name="startIndex">Начальный индекс</param>
        /// <param name="endIndex">Конечный индекс</param>
        /// <returns>Процес</returns>
        private async Task LoadRange(int startIndex, int endIndex)
        {
            if (virtualItems == null || virtualListView == null || isLoading) return;

            isLoading = true;

            try
            {
                await ClearRange(virtualStartIndex, virtualEndIndex);

                virtualStartIndex = startIndex;
                virtualEndIndex = endIndex;

                bool isNum = virtualListView.Columns[0].Name == "numColumn";

                virtualListView.BeginUpdate();

                for (int i = startIndex; i <= endIndex; i++)
                {
                    if (i < virtualItems.Count)
                    {
                        int num = isNum ? i + 1 : 0;
                        ListViewItem lvItem = await CreateListViewItem(virtualItems[i], num);
                        virtualListView.Items[i] = lvItem;
                    }
                }

                virtualListView.EndUpdate();
            }
            finally
            {
                isLoading = false;
            }
        }

        /// <summary>
        /// Очищаем диапазон
        /// </summary>
        /// <param name="startIndex">Начальный индекс</param>
        /// <param name="endIndex">Конечный индекс</param>
        /// <returns>Процес</returns>
        private async Task ClearRange(int startIndex, int endIndex)
        {
            if (virtualListView == null) return;

            for (int i = startIndex; i <= endIndex && i < virtualListView.Items.Count; i++)
            {
                if (virtualListView.Items[i].Tag != null)
                {
                    virtualListView.Items[i] = new ListViewItem();
                }
            }

            await Task.CompletedTask;
        }

        /// <summary>
        /// Получаем количество элементов находящихся в видимой области
        /// </summary>
        /// <returns>Количество элементов находящихся в видимой области</returns>
        private int GetVisibleItemsCount()
        {
            if (virtualListView == null || virtualListView.Items.Count == 0)
                return 10;

            try
            {
                // Получаем высоту клиентской области
                int clientHeight = virtualListView.ClientSize.Height;

                // Получаем высоту элемента
                Rectangle itemRect = virtualListView.GetItemRect(0);
                int itemHeight = itemRect.Height;

                if (itemHeight > 0)
                {
                    return Math.Max(1, clientHeight / itemHeight + 1);
                }
            }
            catch
            {
                // Если не удалось получить размеры, возвращаем значение по умолчанию
            }

            return 10;
        }

        /// <summary>
        /// Отключение виртуального режима
        /// </summary>
        public void StopVirtualMode()
        {
            if (virtualListView != null && isVirtualModeActive)
            {
                virtualListView.RetrieveVirtualItem -= OnRetrieveVirtualItem;
                virtualListView.CacheVirtualItems -= OnCacheVirtualItems;
                virtualListView.HandleCreated -= ListViewOnHandleCreated;
                virtualListView.HandleDestroyed -= ListViewOnHandleDestroyed;

                UnsubscribeFromScroll();

                virtualListView.VirtualMode = false;
                isVirtualModeActive = false;

                virtualItems = null;
                virtualListView = null;
            }
        }

        /// <summary>
        /// Обновление виртуального списка элементов
        /// </summary>
        /// <returns>Процес</returns>
        public async Task RefreshVirtualItems()
        {
            if (isVirtualModeActive && virtualListView != null)
            {
                virtualListView.VirtualListSize = virtualItems.Count;

                virtualListView.Items.Clear();
                for (int i = 0; i < virtualItems.Count; i++)
                {
                    virtualListView.Items.Add(new ListViewItem());
                }

                await LoadVisibleRange(virtualStartIndex);
            }
        }

        private void ListViewOnHandleCreated(object sender, EventArgs e)
        {
            if (sender is ListView listView)
            {
                SubscribeToScroll(listView);
            }
        }

        private void ListViewOnHandleDestroyed(object sender, EventArgs e) => UnsubscribeFromScroll();

        private async void OnScrollMessageReceived(object sender, EventArgs e)
        {
            if (!isVirtualModeActive || virtualListView == null || isLoading) return;

            // Небольшая задержка для предотвращения множественных вызовов
            await Task.Delay(50);

            int currentTopIndex = virtualListView.TopItem?.Index ?? 0;
            int scrollDelta = Math.Abs(currentTopIndex - lastTopIndex);

            // При прокрутке на 5 элементов, обновляем видимую область
            if (scrollDelta >= UpdateScrollBufferSize)
            {
                lastTopIndex = currentTopIndex;

                int visibleCount = GetVisibleItemsCount();
                int newStartIndex = Math.Max(0, currentTopIndex - BufferSize);
                int newEndIndex = Math.Min(virtualItems.Count - 1, currentTopIndex + visibleCount + BufferSize);

                await LoadRange(newStartIndex, newEndIndex);
            }
        }

        private async void OnRetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            if (!isVirtualModeActive || virtualItems == null || isLoading) return;

            // Проверяем, нужно ли подгрузить новые элементы
            if (e.ItemIndex < virtualStartIndex || e.ItemIndex > virtualEndIndex)
            {
                isLoading = true;
                try
                {
                    await LoadVisibleRange(e.ItemIndex);
                }
                finally
                {
                    isLoading = false;
                }
            }

            // Получаем элемент из кэша
            if (e.ItemIndex >= virtualStartIndex && e.ItemIndex <= virtualEndIndex)
            {
                ListViewItem cachedItem = virtualListView.Items[e.ItemIndex];

                if (cachedItem != null && cachedItem.Tag != null)
                {
                    e.Item = cachedItem;
                }
            }
        }

        private async void OnCacheVirtualItems(object sender, CacheVirtualItemsEventArgs e)
        {
            // Очищаем элементы, которые вышли за пределы буфера
            if (e.StartIndex > virtualEndIndex + BufferSize ||
                e.EndIndex < virtualStartIndex - BufferSize)
            {
                await LoadVisibleRange(e.StartIndex);
            }
        }
    }
}
