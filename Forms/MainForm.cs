using DataBaseProvaider;
using DataBaseProvaider.Attributes;
using DataBaseProvaider.Enums;
using DataBaseProvaider.Objects;
using RentalAccountingApp.Classes.Enums;
using RentalAccountingApp.Forms;
using RentalAccountingApp.Forms.CatalogsForm;
using RentalDBModels;
using RentalDBModels.Views.DBViews;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms.DataVisualization.Charting;
using WinFormsComponents.Classes;
using WinFormsComponents.Controls;

namespace RentalAccountingApp
{
    public partial class MainForm : Form
    {
        private int cbCategoryMaxCount = 20;
        private int maxCountQuarter = 4;
        private readonly SeriesChartType[] seriesChartTypes = [SeriesChartType.Pie, SeriesChartType.Funnel, SeriesChartType.Doughnut, SeriesChartType.Pyramid];
        private Loader loader = new();
        private bool isLoad = true;

        /// <summary>
        /// Параметры фильтрации и сортировки для вывода статистики
        /// </summary>
        private CollectionParametrs ParametrsStatisticAccounting => new()
        {
            Orders = [new(nameof(StatisticView.Year), OrderType.Desc)]
        };
        /// <summary>
        /// Список с полными данными бухгалтерской статистики
        /// </summary>
        private BindingList<StatisticView> StaticticAccountingItems { get; set; }
        /// <summary>
        /// Список со всеми категориями Категории + Матерьялы
        /// </summary>
        private BindingList<AllCategoryView> AllCategoryViewItems { get; set; }
        /// <summary>
        /// Объект статистики с иформацией за все время
        /// </summary>
        private StatisticView AllStatistics { get; set; } = new StatisticView();
        /// <summary>
        /// Режим поиска не популярных инвентаряй
        /// </summary>
        private NotPopularInventeriesSearhMode NotPopularInventeriesSearhMode { get; set; } = NotPopularInventeriesSearhMode.Foreach;

        public MainForm()
        {
            InitializeComponent();

            ConnectionInfo.ConnectDB();

            loader.AutoSetup(this);
        }

        private void tsbSetingsOnClick(object sender, EventArgs e) => new SettingsForm().Show();

        private void MainFormOnKeyDown(object sender, KeyEventArgs e)
        {
            bool isComand = false;

            switch (e.KeyCode)
            {
                case Keys.S when e.Control:
                    isComand = true;
                    new SettingsForm().Show();
                    e.SuppressKeyPress = true;
                    break;
            }
        }

        private void tsbCatalogsOnClick(object sender, EventArgs e) => new BaseCatalogsForm().Show();

        private void tsbClientsOnClick(object sender, EventArgs e) => new ClientsForm().Show();

        private void tsbJournalOnClick(object sender, EventArgs e) => new Journal().Show();

        private async void MainFormOnLoad(object sender, EventArgs e)
        {
            loader.StartAnimation();

            await StatisticDiagramsLoad();
            await CreateTasks();

            loader.StopAnimation();
        }

        /// <summary>
        /// Получение информации по заданиям курсового
        /// </summary>
        /// <returns>Процес</returns>
        private async Task CreateTasks()
        {
            await LoadFirsTask();
            await LoadSecondTask();
            await LoadFourthTask();
            LoadThirdTask();
        }

        /// <summary>
        /// Получение информации по первому заданию курсового
        /// </summary>
        /// <remarks>
        /// Найти человека, который имеет максимальную сумму оплат в категории, заданной пользователем.
        /// </remarks>
        /// <returns>Процес</returns>
        public async Task LoadFirsTask()
        {
            AllCategoryViewItems = await DBProvider.GetCollectionModel<AllCategoryView>();

            dbmpCategory.Visible = AllCategoryViewItems.Count <= cbCategoryMaxCount;
            dbmpCategory.ForColName = dmslCategory.ForColName = [nameof(AllCategoryView.IsTypeCategory)];
            dmslCategory.Visible = !dbmpCategory.Visible;

            if (dbmpCategory.Visible)
            {
                dbmpCategory.ModelType = typeof(AllCategoryView);

            }
            else dmslCategory.ModelType = typeof(AllCategoryView);

            if (isLoad)
            {
                isLoad = lHeaderTaskFirst.Visible = lClientTitle.Visible = pbClient.Visible =
                lClientCategory.Visible = bClientCategoryReload.Visible =
                dbmpCategory.Visible = dmslCategory.Visible = false;
            }
        }

        /// <summary>
        /// Получение информации по второму заданию курсового
        /// </summary>
        /// <remarks>
        /// Найти категорию с максимальным количеством выдачи и в ней вывести 2 самых не популярных товара.
        /// </remarks>
        /// <returns>Процес</returns>
        public async Task LoadSecondTask()
        {
            int maxCountInventory = Convert.ToInt32(await DBProvider.Max<NumberOfIssuesByCategory>(nameof(NumberOfIssuesByCategory.CountInventory)) ?? 0);

            if (maxCountInventory == 0)
            {
                lMaxCategory.Text = "Категории имеющие хотябы 1 выдачу не найдены";
                lNotPopularInventoryTitle.Visible = lNotPopularInventory.Visible =
                    pbPNotPopularInventory.Visible = btModeSerhPopular.Visible = false;
                lNotPopularInventory.Text = String.Empty;
                return;
            }
            else
            {
                lNotPopularInventoryTitle.Visible = lNotPopularInventory.Visible =
                    pbPNotPopularInventory.Visible = btModeSerhPopular.Visible = lMaxCategory.Visible;
            }

            CollectionParametrs parametrs = new()
            {
                Conditions =
                [
                    new (nameof(NumberOfIssuesByCategory.CountInventory), ConditionalOperators.Equal, maxCountInventory)
                ]
            };

            BindingList<NumberOfIssuesByCategory> numberOfIssuesByCategories = await DBProvider.GetCollectionModel<NumberOfIssuesByCategory>(parametrs);
            string namesCategory = String.Empty;
            string namesInventory = String.Empty;
            parametrs = new()
            {
                Orders =
                [
                    new (nameof(NotPopularInventoryGroupByCategories.UsageCount), OrderType.Asc)
                ],
                Limit = 2
            };

            if (numberOfIssuesByCategories.Count > 1)
            {
                Image icon = CombineIcons(Properties.Resources.category, Properties.Resources.materials);
                pbMaxCat.BackgroundImage = icon;
                btModeSerhPopular.Visible = lMaxCategory.Visible;
            }
            else
            {
                pbMaxCat.BackgroundImage = numberOfIssuesByCategories[0].IsTypeCategory
                    ? Properties.Resources.category
                    : Properties.Resources.materials;
                btModeSerhPopular.Visible = false;
            }

            foreach (NumberOfIssuesByCategory numberOfIssuesByCategory in numberOfIssuesByCategories)
            {
                namesCategory += ", " + numberOfIssuesByCategory.Name;

                if (NotPopularInventeriesSearhMode.Equals(NotPopularInventeriesSearhMode.Foreach))
                {
                    namesInventory += await GetNotPopularInventoryForeah(numberOfIssuesByCategory);
                }
                else
                {
                    parametrs.Conditions += new ConditionsParametr(nameof(NotPopularInventoryGroupByCategories.IdCategory), ConditionalOperators.Equal, LogicOperators.And, numberOfIssuesByCategory.Id);
                    parametrs.Conditions += new ConditionsParametr(nameof(NotPopularInventoryGroupByCategories.IsTypeCategory), ConditionalOperators.Equal, LogicOperators.Or, numberOfIssuesByCategory.IsTypeCategory);
                }
            }

            if (NotPopularInventeriesSearhMode.Equals(NotPopularInventeriesSearhMode.All))
            {
                namesInventory += await GetNotPopularInventoryAll(parametrs);
            }

            lMaxCategory.Text = $"{namesCategory.TrimStart(',', ' ')} - было выдано {maxCountInventory} {maxCountInventory.GetDeclension("раз", "раза", "раз")}";
            lNotPopularInventory.Text = namesInventory.TrimEnd('\n', ' ');
        }

        /// <summary>
        /// Получение информации по третьему заданию курсового
        /// </summary>
        /// <remarks>
        /// Клиенты, которые арендовали строго 3 товара из 6 категорий
        /// </remarks>
        /// <returns>Процес</returns>
        public void LoadThirdTask()
        {
            dmlvFilterClients.ImageList = ilTabMenu;
            dmlvFilterClients.ModelType = typeof(ViewCliensWithThreeInvInSixCat);
        }

        /// <summary>
        /// Получение информации по четвертому заданию курсового
        /// </summary>
        /// <remarks>
        /// Вывести за каждый месяц года, заданного пользователем, количество фактов выдачи инвентаря
        /// </remarks>
        /// <returns>Процес</returns>
        public async Task LoadFourthTask()
        {
            dmlvRentalInventory.IsSorted = false;
            dmlvRentalInventory.ImageList = ilTabMenu;
            int minYear = ((int?)await DBProvider.Min<IssueToCountInventoryByGroup>(nameof(IssueToCountInventoryByGroup.IssueYear))) ?? 1991;
            int maxYear = ((int?)await DBProvider.Max<IssueToCountInventoryByGroup>(nameof(IssueToCountInventoryByGroup.IssueYear))) ?? DateTime.Now.Year;

            nudYearFilterInvevntoryRental.Minimum = minYear;
            nudYearFilterInvevntoryRental.Maximum = maxYear;
            dmlvRentalInventory.Parameters.Conditions +=
                new ConditionsParametr(nameof(IssueToCountInventoryByGroup.IssueYear), ConditionalOperators.Equal, nudYearFilterInvevntoryRental.Value);
            dmlvRentalInventory.Parameters.Orders += new OrderParametr(nameof(IssueToCountInventoryByGroup.IssueYear), OrderType.Asc);
            dmlvRentalInventory.Parameters.Orders += new OrderParametr(nameof(IssueToCountInventoryByGroup.IssueMonth), OrderType.Asc);
            dmlvRentalInventory.ModelType = typeof(IssueToCountInventoryByGroup);
        }

        /// <summary>
        /// Формирование строки с информацией о не популярном инвентаре в режиме по каждой категории
        /// </summary>
        /// <param name="numberOfIssuesByCategory">Информация о категории с максимальным числом выдачи</param>
        /// <returns>Строка с информацией о не популярном инвентаре</returns>
        private async Task<string> GetNotPopularInventoryForeah(NumberOfIssuesByCategory numberOfIssuesByCategory)
        {
            CollectionParametrs parametrs = new()
            {
                Conditions =
                [
                    new (nameof(NotPopularInventoryGroupByCategories.IdCategory), ConditionalOperators.Equal, LogicOperators.And, numberOfIssuesByCategory.Id),
                    new (nameof(NotPopularInventoryGroupByCategories.IsTypeCategory), ConditionalOperators.Equal, LogicOperators.And, numberOfIssuesByCategory.IsTypeCategory)
                ]
            };

            BindingList<NotPopularInventoryGroupByCategories> notPopularInventories = await DBProvider.GetCollectionModel<NotPopularInventoryGroupByCategories>(parametrs);

            string InvetoryStr = $"Не популярный инвентарь категории {numberOfIssuesByCategory.Name}:\n";
            string spacesTab = "    ";

            foreach (NotPopularInventoryGroupByCategories item in notPopularInventories)
            {
                InvetoryStr += $"{spacesTab}-{item.InventoryName} был выдан {item.UsageCount} {item.UsageCount.GetDeclension("раз", "раза", "раз")}\n";
            }

            return InvetoryStr;
        }

        /// <summary>
        /// Формирование строки с информацией о не популярном инвентаре в режиме по всем категориям
        /// </summary>
        /// <param name="numberOfIssuesByCategories">Информация о категориях с максимальным числом выдачи</param>
        /// <returns>Строка с информацией о не популярном инвентаре</returns>
        private async Task<string> GetNotPopularInventoryAll(CollectionParametrs parametrs)
        {
            BindingList<NotPopularInventoryGroupByCategories> notPopularInventories = await DBProvider.GetCollectionModel<NotPopularInventoryGroupByCategories>(parametrs);

            string InvetoryStr = $"Не популярный инвентарь среди всех категорий:\n";
            string spacesTab = "    ";

            foreach (NotPopularInventoryGroupByCategories item in notPopularInventories)
            {
                InvetoryStr += $"{spacesTab}-{item.InventoryName} был выдан {item.UsageCount} {item.UsageCount.GetDeclension("раз", "раза", "раз")}\n";
            }

            return InvetoryStr;
        }

        /// <summary>
        /// Загрузка диаграмм
        /// </summary>
        /// <returns>Процес</returns>
        private async Task StatisticDiagramsLoad()
        {
            AllStatistics.Year = AllStatistics.Quarter = 0;
            AllStatistics.Profit = AllStatistics.Income = AllStatistics.DebetSum = AllStatistics.RentalSum = 0;
            cStatisticQuarter.Series.Clear();
            cStatisticQuarter.ChartAreas.Clear();
            cStatisticQuarter.Legends.Clear();
            cStatisticQuarter.Titles.Clear();

            cStatisticQuarter.Titles.Add(new Title($"Статистика бухгалтерии за последние {maxCountQuarter} квартала")
            {
                Font = new("Microsoft Sans Serif", 12, FontStyle.Bold)
            });


            StaticticAccountingItems = await DBProvider.GetCollectionModel<StatisticView>(ParametrsStatisticAccounting);
            (PropertyInfo, List<PropertyInfo>) structSeries = GetStructSeries();

            int chartIndex = 0;

            foreach (StatisticView statistic in StaticticAccountingItems)
            {
                UpdateAllStatistic(statistic);

                if (chartIndex < maxCountQuarter)
                {
                    chartIndex++;
                    string chartAreaName = $"ChartArea{chartIndex}";

                    ChartArea chartArea = CreateSattisticChartArea(chartAreaName);

                    cStatisticQuarter.ChartAreas.Add(chartArea);

                    (Series series, Legend legend) = CreateSattisticSeries(statistic, structSeries, chartAreaName);

                    cStatisticQuarter.Series.Add(series);
                    cStatisticQuarter.Legends.Add(legend);
                }
            }

            AllStatisticLoad(structSeries);
            AdjustLabelsForSize(cStatisticQuarter);
            AdjustLabelsForSize(cStatisticAllPeriod);

        }

        /// <summary>
        /// Получение структуры объекта статистики
        /// </summary>
        /// <returns>Свойство заголовка и свойства значений</returns>
        private (PropertyInfo, List<PropertyInfo>) GetStructSeries()
        {
            PropertyInfo[] propertiesStatisticView = typeof(StatisticView).GetProperties();
            PropertyInfo propertyTitle = null;
            List<PropertyInfo> propertyValues = new();

            foreach (PropertyInfo property in propertiesStatisticView)
            {
                if (property.GetCustomAttribute<ViewModelAttribute>()?.Headline ?? false)
                {
                    propertyTitle = property;
                    continue;
                }

                DescriptionAttribute da = property.GetCustomAttribute<DescriptionAttribute>();

                if (da is not null)
                {
                    propertyValues.Add(property);
                }
            }

            return (propertyTitle, propertyValues);
        }

        /// <summary>
        /// Создание области диаграммы
        /// </summary>
        /// <param name="name">Имя области диаграммы</param>
        /// <returns>Область диаграммы</returns>
        private ChartArea CreateSattisticChartArea(string name)
        {
            ChartArea chartArea = new ChartArea(name)
            {
                BackColor = Color.WhiteSmoke,
                BorderColor = Color.LightGray,
                BorderDashStyle = ChartDashStyle.Solid
            };

            return chartArea;
        }

        /// <summary>
        /// Создание диаграммы и ее легенды
        /// </summary>
        /// <param name="statistic">Объект статистики</param>
        /// <param name="structSeries">Структура объекта статистики</param>
        /// <param name="chartArea">Имя области диаграммы</param>
        /// <returns>диаграмма и ее легенда</returns>
        private (Series, Legend) CreateSattisticSeries(StatisticView statistic, (PropertyInfo propertyTitle, List<PropertyInfo> propertiesValue) structSeries, string chartArea)
        {
            string title = structSeries.propertyTitle.GetValue(statistic).ToString();

            Series pieSeries = new($"Series_{chartArea}")
            {
                ChartType = seriesChartTypes[new Random().Next(0, 3)],
                ChartArea = chartArea,
                Label = "#VALX: #VAL{N2} ₽\n(#PERCENT{P0})",
                Font = new Font("Sans Serif", 10),
                ["PieLabelStyle"] = "Outside",
                ["PieLineColor"] = "Gray"
            };

            Legend legend = new($"Legend_{chartArea}")
            {
                Title = title,
                DockedToChartArea = chartArea,
                Docking = Docking.Bottom,
                TitleFont = new Font("Sans Serif", 10, FontStyle.Bold),
                Font = new Font("Sans Serif", 9),
                IsDockedInsideChartArea = false,
                AutoFitMinFontSize = 8,
                MaximumAutoSize = 50
            };

            pieSeries.Legend = legend.Name;

            foreach (PropertyInfo property in structSeries.propertiesValue)
            {
                double value = (double)property.GetValue(statistic);
                string description = property.GetCustomAttribute<DescriptionAttribute>().Description;
                Color color = Extensions.RandomColor();

                if (value != 0)
                {
                    int pointIndex = pieSeries.Points.AddXY(description, value);
                    pieSeries.Points[pointIndex].Color = color;
                    pieSeries.Points[pointIndex].LegendText = description;
                    pieSeries.Points[pointIndex].ToolTip = $"{description}: {value:N2} ₽";
                }
            }

            return (pieSeries, legend);
        }

        /// <summary>
        /// Обновление значений полной статистики
        /// </summary>
        /// <param name="statistic">Объект статистики</param>
        private void UpdateAllStatistic(StatisticView statistic)
        {
            if (AllStatistics.Year < statistic.Year) AllStatistics.Year = statistic.Year;
            if (AllStatistics.Quarter == 0 || AllStatistics.Quarter > statistic.Year) AllStatistics.Quarter = statistic.Year;

            AllStatistics.DebetSum += statistic.DebetSum;
            AllStatistics.RentalSum += statistic.RentalSum;
            AllStatistics.Income += statistic.Income;
            AllStatistics.Profit += statistic.Profit;
        }

        /// <summary>
        /// Загрузка диаграммы с общими данными
        /// </summary>
        private void AllStatisticLoad((PropertyInfo propertyTitle, List<PropertyInfo> propertiesValue) structSeries)
        {
            cStatisticAllPeriod.Series.Clear();
            cStatisticAllPeriod.ChartAreas.Clear();
            cStatisticAllPeriod.Legends.Clear();

            string chartAreaName = $"ChartArea1";

            ChartArea chartArea = CreateSattisticChartArea(chartAreaName);

            cStatisticAllPeriod.ChartAreas.Add(chartArea);

            (Series series, Legend legend) = CreateSattisticSeries(AllStatistics, structSeries, chartAreaName);

            cStatisticAllPeriod.Series.Add(series);
            cStatisticAllPeriod.Legends.Add(legend);
        }

        /// <summary>
        /// Обновление форматирования легенды и анотаций при изменении размера
        /// </summary>
        private void AdjustLabelsForSize(Chart chart)
        {
            if (chart.ChartAreas.Count == 0) return;

            foreach (ChartArea area in cStatisticQuarter.ChartAreas)
            {
                ElementPosition position = area.Position;
                float width = position.Width * chart.Width / 100f;
                float height = position.Height * chart.Height / 100f;

                bool isSmall = width < 150 || height < 100;

                foreach (Series series in chart.Series)
                {
                    if (series.ChartArea == area.Name)
                    {
                        if (isSmall)
                        {
                            series.Label = string.Empty;
                            series["PieLabelStyle"] = "Disabled";
                            series.Font = new Font("Sans Serif", 7);
                        }
                        else
                        {
                            series.Label = "#VALX: #VAL{N2} ₽\n(#PERCENT{P0})";
                            series["PieLabelStyle"] = "Outside";
                            series.Font = new Font("Sans Serif", 10);
                        }

                        UpdateLegendText(series, isSmall);
                    }
                }
            }

            chart.Invalidate();
        }

        /// <summary>
        /// Обновление размеров
        /// </summary>
        /// <param name="series">Диаграммы</param>
        /// <param name="showDetailsInLegend">Изменения в легенде</param>
        private void UpdateLegendText(Series series, bool showDetailsInLegend)
        {
            foreach (DataPoint point in series.Points)
            {
                if (showDetailsInLegend)
                {
                    double value = point.YValues[0];
                    double total = series.Points.Sum(p => p.YValues[0]);
                    double percent = total > 0 ? (value / total) * 100 : 0;
                    point.LegendText = $"{point.AxisLabel}: {value:N2} ₽ ({percent:F1}%)";
                }
                else
                {
                    point.LegendText = point.AxisLabel;
                }
            }
        }

        private void cStatisticOnResize(object sender, EventArgs e) => AdjustLabelsForSize((Chart)sender);

        private async void dbCategoryOnSelectedChange(object sender, EventArgs e)
        {
            AllCategoryView allCategoryViewSel;

            if (dmslCategory.Visible)
            {
                if (dmslCategory.SelectedVal is null) return;

                allCategoryViewSel =
                    AllCategoryViewItems.First(i => i.Id == dmslCategory.SelectedVal && i.IsTypeCategory == (bool)dmslCategory.ForSelectedWhere[nameof(AllCategoryView.IsTypeCategory)]);

                dmslCategory.ImageKey = allCategoryViewSel.ImageKey;
            }
            else
            {
                if (dbmpCategory.SelectedVal is null) return;

                allCategoryViewSel =
                    AllCategoryViewItems.First(i => i.Id == dbmpCategory.SelectedVal && i.IsTypeCategory == (bool)(dbmpCategory.ForSelectedWhere[nameof(AllCategoryView.IsTypeCategory)]));

                dbmpCategory.Image = ilTabMenu.Images[allCategoryViewSel.ImageKey];
            }

            await SetCategoryClient(allCategoryViewSel);
        }

        /// <summary>
        /// Нахождение и вывод записи с максимальной суммой оплат или нескольких записей, если таких клиентов множество
        /// </summary>
        /// <param name="allCategoryViewSel">Объект выбраной категории</param>
        /// <returns>Процесс</returns>
        private async Task SetCategoryClient(AllCategoryView allCategoryViewSel)
        {
            lClientCategory.Text = lClientPhone.Text = lSumPay.Text = String.Empty;

            CollectionParametrs parametrs = new()
            {
                Conditions =
                [
                    new ("IdCategory", ConditionalOperators.Equal, LogicOperators.And, allCategoryViewSel.Id),
                    new (nameof(AllCategoryView.IsTypeCategory), ConditionalOperators.Equal, LogicOperators.And, allCategoryViewSel.IsTypeCategory)
                ],
                Orders =
                [
                    new (nameof(ClientCategoryOplatInfo.OplatCategory), OrderType.Desc)
                ]
            };
            double maxOplat = Convert.ToDouble(await DBProvider.Max<ClientCategoryOplatInfo>(nameof(ClientCategoryOplatInfo.OplatCategory), parametrs.Conditions) ?? 0);

            lbPhoneTitle.Visible = lClientPayTitle.Visible =
            lClientPhone.Visible = lSumPay.Visible =
            pbClientPhone.Visible = pbClientPay.Visible = maxOplat != 0;

            if (!lbPhoneTitle.Visible)
            {
                lClientCategory.Text = "Категория не имеет оплат";
                return;
            }

            parametrs.Conditions += new ConditionsParametr(nameof(ClientCategoryOplatInfo.OplatCategory), ConditionalOperators.Equal, maxOplat);
            BindingList<ClientCategoryOplatInfo> clientsCategoryOplat = await DBProvider.GetCollectionModel<ClientCategoryOplatInfo>(parametrs);

            foreach (ClientCategoryOplatInfo clientCategoryOplat in clientsCategoryOplat)
            {
                lClientCategory.Text += ", " + clientCategoryOplat.OwnerName;
                lClientPhone.Text += ", " + clientCategoryOplat.Phone;
            }

            lSumPay.Text = $"{maxOplat:N2} ₽";
            lClientCategory.Text = lClientCategory.Text.TrimStart(',', ' ');
            lClientPhone.Text = lClientPhone.Text.TrimStart(',', ' ');
        }

        private async void bReloadMaxCategoryOnClick(object sender, EventArgs e) => await LoadSecondTask();

        private async void tsbReloadStatisticsOnClick(object sender, EventArgs e) => await StatisticDiagramsLoad();

        /// <summary>
        /// Метод перерисовки двух картинок в одну
        /// </summary>
        /// <param name="imageFirst">Первая картинка</param>
        /// <param name="imageSecond">Вторая картинка</param>
        /// <param name="spacing">Отступ</param>
        /// <returns>Новая картинка</returns>
        public Image CombineIcons(Image imageFirst, Image imageSecond, int spacing = 0)
        {
            int totalWidth = imageFirst.Width + spacing + imageSecond.Width;
            int maxHeight = Math.Max(imageFirst.Height, imageSecond.Height);

            Bitmap combined = new Bitmap(totalWidth, maxHeight);

            using (Graphics g = Graphics.FromImage(combined))
            {
                g.Clear(Color.Transparent);
                g.DrawImage(imageFirst, 0, (maxHeight - imageFirst.Height) / 2);
                g.DrawImage(imageSecond, imageFirst.Width + spacing, (maxHeight - imageSecond.Height) / 2);
            }

            return combined;
        }

        private async void btModeSerhPopularOnClick(object sender, EventArgs e)
        {
            if (NotPopularInventeriesSearhMode.Equals(NotPopularInventeriesSearhMode.Foreach))
            {
                NotPopularInventeriesSearhMode = NotPopularInventeriesSearhMode.All;
                btModeSerhPopular.BackgroundImage = Properties.Resources.searhModeForeah;
            }
            else
            {
                NotPopularInventeriesSearhMode = NotPopularInventeriesSearhMode.Foreach;
                btModeSerhPopular.BackgroundImage = Properties.Resources.searhModeAll;
            }

            await LoadSecondTask();
        }

        private void lTitleTaskFourthOnClick(object sender, EventArgs e)
        {
            lTitleYearFilterInventoryRental.Visible =
                pbYearFilterInventoryRental.Visible =
                nudYearFilterInvevntoryRental.Visible =
                dmlvRentalInventory.Visible = !lTitleYearFilterInventoryRental.Visible;
        }

        private void lTitleTaskThirdOnClick(object sender, EventArgs e) => dmlvFilterClients.Visible = !dmlvFilterClients.Visible;

        private void lTitleTaskSecondOnClick(object sender, EventArgs e)
        {
            lMaxCategoryTitle.Visible = pbMaxCat.Visible =
                lMaxCategory.Visible = bReloadMaxCategory.Visible = !lMaxCategoryTitle.Visible;

            lNotPopularInventoryTitle.Visible = lNotPopularInventory.Visible =
                pbPNotPopularInventory.Visible = btModeSerhPopular.Visible =
                lMaxCategoryTitle.Visible && !String.IsNullOrEmpty(lNotPopularInventory.Text);
        }

        private void lTitleTaskFirstOnClick(object sender, EventArgs e)
        {
            lHeaderTaskFirst.Visible = lClientTitle.Visible = pbClient.Visible =
                lClientCategory.Visible = bClientCategoryReload.Visible = !pbClient.Visible;

            dbmpCategory.Visible = pbClient.Visible && AllCategoryViewItems.Count <= cbCategoryMaxCount;
            dmslCategory.Visible = pbClient.Visible && !dbmpCategory.Visible;

            lbPhoneTitle.Visible = lClientPayTitle.Visible = pbClientPhone.Visible =
                pbClientPay.Visible = lClientPhone.Visible = lSumPay.Visible =
                pbClient.Visible && !String.IsNullOrEmpty(lSumPay.Text);
        }

        private async void nudYearFilterInvevntoryRentalOnValueChanged(object sender, EventArgs e)
        {
            ConditionsParametr parametr = dmlvRentalInventory.Parameters.Conditions.FirstOrDefault(i => i.ColumnName == nameof(IssueToCountInventoryByGroup.IssueYear));

            if (parametr is not null && Convert.ToInt32(parametr.Value) != nudYearFilterInvevntoryRental.Value)
            {
                parametr.Value = nudYearFilterInvevntoryRental.Value;
                await dmlvRentalInventory.LoadListAsync();
            }
        }
    }
}
