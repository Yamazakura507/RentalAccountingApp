namespace RentalAccountingApp
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            DataBaseProvaider.Objects.CollectionParametrs collectionParametrs1 = new DataBaseProvaider.Objects.CollectionParametrs();
            DataBaseProvaider.Objects.CollectionParametrs collectionParametrs2 = new DataBaseProvaider.Objects.CollectionParametrs();
            tsMainMenu = new ToolStrip();
            tsbSetings = new ToolStripButton();
            tsbCatalogs = new ToolStripButton();
            tsbClients = new ToolStripButton();
            tsbJournal = new ToolStripButton();
            ilTabMenu = new ImageList(components);
            tpStatistic = new TabPage();
            tlpStatistic = new TableLayoutPanel();
            tsStatistic = new ToolStrip();
            tsbReloadStatistics = new ToolStripButton();
            tcStatistic = new TabControl();
            tpTasks = new TabPage();
            tlpTasks = new TableLayoutPanel();
            dmlvRentalInventory = new WinFormsComponents.Controls.DBModelListView();
            pbYearFilterInventoryRental = new PictureBox();
            lTitleYearFilterInventoryRental = new Label();
            lTitleTaskFourth = new Label();
            plineThird = new Panel();
            lTitleTaskThird = new Label();
            plineSecond = new Panel();
            btModeSerhPopular = new Button();
            lNotPopularInventory = new Label();
            pbPNotPopularInventory = new PictureBox();
            lNotPopularInventoryTitle = new Label();
            bClientCategoryReload = new Button();
            lTitleTaskSecond = new Label();
            lSumPay = new Label();
            pbClientPay = new PictureBox();
            lClientPayTitle = new Label();
            lClientPhone = new Label();
            pbClientPhone = new PictureBox();
            lbPhoneTitle = new Label();
            lClientTitle = new Label();
            lTitleTaskFirst = new Label();
            dbmpCategory = new WinFormsComponents.Controls.DBModelPicker();
            lHeaderTaskFirst = new Label();
            dmslCategory = new WinFormsComponents.Controls.DBModelSelectedList();
            pbClient = new PictureBox();
            lClientCategory = new Label();
            plineFirst = new Panel();
            lMaxCategoryTitle = new Label();
            pbMaxCat = new PictureBox();
            lMaxCategory = new Label();
            bReloadMaxCategory = new Button();
            dmlvFilterClients = new WinFormsComponents.Controls.DBModelListView();
            nudYearFilterInvevntoryRental = new NumericUpDown();
            tsInfoTasks = new ToolStrip();
            tslInfoTask = new ToolStripLabel();
            cStatisticQuarter = new System.Windows.Forms.DataVisualization.Charting.Chart();
            cStatisticAllPeriod = new System.Windows.Forms.DataVisualization.Charting.Chart();
            tsMainMenu.SuspendLayout();
            tpStatistic.SuspendLayout();
            tlpStatistic.SuspendLayout();
            tsStatistic.SuspendLayout();
            tcStatistic.SuspendLayout();
            tpTasks.SuspendLayout();
            tlpTasks.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbYearFilterInventoryRental).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbPNotPopularInventory).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbClientPay).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbClientPhone).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbClient).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbMaxCat).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudYearFilterInvevntoryRental).BeginInit();
            tsInfoTasks.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)cStatisticQuarter).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cStatisticAllPeriod).BeginInit();
            SuspendLayout();
            // 
            // tsMainMenu
            // 
            tsMainMenu.Items.AddRange(new ToolStripItem[] { tsbSetings, tsbCatalogs, tsbClients, tsbJournal });
            tsMainMenu.Location = new Point(0, 0);
            tsMainMenu.Name = "tsMainMenu";
            tsMainMenu.Size = new Size(648, 25);
            tsMainMenu.TabIndex = 0;
            tsMainMenu.Text = "toolStrip1";
            // 
            // tsbSetings
            // 
            tsbSetings.Alignment = ToolStripItemAlignment.Right;
            tsbSetings.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsbSetings.Image = Properties.Resources.setings;
            tsbSetings.ImageTransparentColor = Color.Magenta;
            tsbSetings.Name = "tsbSetings";
            tsbSetings.Size = new Size(23, 22);
            tsbSetings.ToolTipText = "Настройки(Ctrl+S)";
            tsbSetings.Click += tsbSetingsOnClick;
            // 
            // tsbCatalogs
            // 
            tsbCatalogs.Image = Properties.Resources.catalog;
            tsbCatalogs.ImageTransparentColor = Color.Magenta;
            tsbCatalogs.Name = "tsbCatalogs";
            tsbCatalogs.Size = new Size(102, 22);
            tsbCatalogs.Text = "Справочники";
            tsbCatalogs.ToolTipText = "Справочники";
            tsbCatalogs.Click += tsbCatalogsOnClick;
            // 
            // tsbClients
            // 
            tsbClients.Image = Properties.Resources.clients;
            tsbClients.ImageTransparentColor = Color.Magenta;
            tsbClients.Name = "tsbClients";
            tsbClients.Size = new Size(75, 22);
            tsbClients.Text = "Клиенты";
            tsbClients.Click += tsbClientsOnClick;
            // 
            // tsbJournal
            // 
            tsbJournal.Image = Properties.Resources.rent;
            tsbJournal.ImageTransparentColor = Color.Magenta;
            tsbJournal.Name = "tsbJournal";
            tsbJournal.Size = new Size(124, 22);
            tsbJournal.Text = "Журналы аренды";
            tsbJournal.Click += tsbJournalOnClick;
            // 
            // ilTabMenu
            // 
            ilTabMenu.ColorDepth = ColorDepth.Depth32Bit;
            ilTabMenu.ImageStream = (ImageListStreamer)resources.GetObject("ilTabMenu.ImageStream");
            ilTabMenu.TransparentColor = Color.Transparent;
            ilTabMenu.Images.SetKeyName(0, "diagrams.png");
            ilTabMenu.Images.SetKeyName(1, "task.png");
            ilTabMenu.Images.SetKeyName(2, "category.png");
            ilTabMenu.Images.SetKeyName(3, "materials.png");
            ilTabMenu.Images.SetKeyName(4, "clients.png");
            ilTabMenu.Images.SetKeyName(5, "date.png");
            // 
            // tpStatistic
            // 
            tpStatistic.Controls.Add(tlpStatistic);
            tpStatistic.Controls.Add(tsStatistic);
            tpStatistic.ImageKey = "diagrams.png";
            tpStatistic.Location = new Point(4, 24);
            tpStatistic.Name = "tpStatistic";
            tpStatistic.Padding = new Padding(3);
            tpStatistic.Size = new Size(640, 530);
            tpStatistic.TabIndex = 0;
            tpStatistic.Text = "Статистика";
            tpStatistic.UseVisualStyleBackColor = true;
            // 
            // tlpStatistic
            // 
            tlpStatistic.ColumnCount = 2;
            tlpStatistic.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpStatistic.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpStatistic.Controls.Add(cStatisticQuarter, 1, 0);
            tlpStatistic.Controls.Add(cStatisticAllPeriod, 0, 0);
            tlpStatistic.Dock = DockStyle.Fill;
            tlpStatistic.Location = new Point(3, 28);
            tlpStatistic.Name = "tlpStatistic";
            tlpStatistic.RowCount = 1;
            tlpStatistic.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlpStatistic.Size = new Size(634, 499);
            tlpStatistic.TabIndex = 3;
            // 
            // tsStatistic
            // 
            tsStatistic.Items.AddRange(new ToolStripItem[] { tsbReloadStatistics });
            tsStatistic.Location = new Point(3, 3);
            tsStatistic.Name = "tsStatistic";
            tsStatistic.Size = new Size(634, 25);
            tsStatistic.TabIndex = 2;
            // 
            // tsbReloadStatistics
            // 
            tsbReloadStatistics.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsbReloadStatistics.Image = Properties.Resources.reload;
            tsbReloadStatistics.ImageTransparentColor = Color.Magenta;
            tsbReloadStatistics.Name = "tsbReloadStatistics";
            tsbReloadStatistics.Size = new Size(23, 22);
            tsbReloadStatistics.Click += tsbReloadStatisticsOnClick;
            // 
            // tcStatistic
            // 
            tcStatistic.Controls.Add(tpTasks);
            tcStatistic.Controls.Add(tpStatistic);
            tcStatistic.Dock = DockStyle.Fill;
            tcStatistic.ImageList = ilTabMenu;
            tcStatistic.Location = new Point(0, 25);
            tcStatistic.Name = "tcStatistic";
            tcStatistic.SelectedIndex = 0;
            tcStatistic.Size = new Size(648, 558);
            tcStatistic.TabIndex = 2;
            // 
            // tpTasks
            // 
            tpTasks.Controls.Add(tlpTasks);
            tpTasks.Controls.Add(tsInfoTasks);
            tpTasks.ImageKey = "task.png";
            tpTasks.Location = new Point(4, 24);
            tpTasks.Name = "tpTasks";
            tpTasks.Padding = new Padding(3);
            tpTasks.Size = new Size(640, 530);
            tpTasks.TabIndex = 1;
            tpTasks.Text = "Задания";
            tpTasks.UseVisualStyleBackColor = true;
            // 
            // tlpTasks
            // 
            tlpTasks.AutoScroll = true;
            tlpTasks.ColumnCount = 4;
            tlpTasks.ColumnStyles.Add(new ColumnStyle());
            tlpTasks.ColumnStyles.Add(new ColumnStyle());
            tlpTasks.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpTasks.ColumnStyles.Add(new ColumnStyle());
            tlpTasks.Controls.Add(dmlvRentalInventory, 0, 16);
            tlpTasks.Controls.Add(pbYearFilterInventoryRental, 1, 15);
            tlpTasks.Controls.Add(lTitleYearFilterInventoryRental, 0, 15);
            tlpTasks.Controls.Add(lTitleTaskFourth, 0, 14);
            tlpTasks.Controls.Add(plineThird, 0, 13);
            tlpTasks.Controls.Add(lTitleTaskThird, 0, 11);
            tlpTasks.Controls.Add(plineSecond, 0, 10);
            tlpTasks.Controls.Add(btModeSerhPopular, 3, 9);
            tlpTasks.Controls.Add(lNotPopularInventory, 2, 9);
            tlpTasks.Controls.Add(pbPNotPopularInventory, 1, 9);
            tlpTasks.Controls.Add(lNotPopularInventoryTitle, 0, 9);
            tlpTasks.Controls.Add(bClientCategoryReload, 3, 3);
            tlpTasks.Controls.Add(lTitleTaskSecond, 0, 7);
            tlpTasks.Controls.Add(lSumPay, 2, 5);
            tlpTasks.Controls.Add(pbClientPay, 1, 5);
            tlpTasks.Controls.Add(lClientPayTitle, 0, 5);
            tlpTasks.Controls.Add(lClientPhone, 2, 4);
            tlpTasks.Controls.Add(pbClientPhone, 1, 4);
            tlpTasks.Controls.Add(lbPhoneTitle, 0, 4);
            tlpTasks.Controls.Add(lClientTitle, 0, 3);
            tlpTasks.Controls.Add(lTitleTaskFirst, 0, 0);
            tlpTasks.Controls.Add(dbmpCategory, 1, 1);
            tlpTasks.Controls.Add(lHeaderTaskFirst, 0, 1);
            tlpTasks.Controls.Add(dmslCategory, 1, 2);
            tlpTasks.Controls.Add(pbClient, 1, 3);
            tlpTasks.Controls.Add(lClientCategory, 2, 3);
            tlpTasks.Controls.Add(plineFirst, 0, 6);
            tlpTasks.Controls.Add(lMaxCategoryTitle, 0, 8);
            tlpTasks.Controls.Add(pbMaxCat, 1, 8);
            tlpTasks.Controls.Add(lMaxCategory, 2, 8);
            tlpTasks.Controls.Add(bReloadMaxCategory, 3, 8);
            tlpTasks.Controls.Add(dmlvFilterClients, 0, 12);
            tlpTasks.Controls.Add(nudYearFilterInvevntoryRental, 2, 15);
            tlpTasks.Dock = DockStyle.Fill;
            tlpTasks.Location = new Point(3, 3);
            tlpTasks.Name = "tlpTasks";
            tlpTasks.RowCount = 17;
            tlpTasks.RowStyles.Add(new RowStyle());
            tlpTasks.RowStyles.Add(new RowStyle());
            tlpTasks.RowStyles.Add(new RowStyle());
            tlpTasks.RowStyles.Add(new RowStyle());
            tlpTasks.RowStyles.Add(new RowStyle());
            tlpTasks.RowStyles.Add(new RowStyle());
            tlpTasks.RowStyles.Add(new RowStyle());
            tlpTasks.RowStyles.Add(new RowStyle());
            tlpTasks.RowStyles.Add(new RowStyle());
            tlpTasks.RowStyles.Add(new RowStyle());
            tlpTasks.RowStyles.Add(new RowStyle());
            tlpTasks.RowStyles.Add(new RowStyle());
            tlpTasks.RowStyles.Add(new RowStyle());
            tlpTasks.RowStyles.Add(new RowStyle());
            tlpTasks.RowStyles.Add(new RowStyle());
            tlpTasks.RowStyles.Add(new RowStyle());
            tlpTasks.RowStyles.Add(new RowStyle());
            tlpTasks.Size = new Size(634, 499);
            tlpTasks.TabIndex = 0;
            // 
            // dmlvRentalInventory
            // 
            tlpTasks.SetColumnSpan(dmlvRentalInventory, 4);
            dmlvRentalInventory.Dock = DockStyle.Fill;
            dmlvRentalInventory.Enabled = false;
            dmlvRentalInventory.FilterOffColor = Color.MistyRose;
            dmlvRentalInventory.FilterOnColor = Color.LightGreen;
            dmlvRentalInventory.IsEditor = false;
            dmlvRentalInventory.IsFilter = false;
            dmlvRentalInventory.IsGridLines = true;
            dmlvRentalInventory.IsRemoveRow = false;
            dmlvRentalInventory.IsRepairEditor = false;
            dmlvRentalInventory.IsRepairRow = false;
            dmlvRentalInventory.IsSearch = false;
            dmlvRentalInventory.IsShowCountAll = true;
            dmlvRentalInventory.IsShowCountEnter = true;
            dmlvRentalInventory.IsShowNum = false;
            dmlvRentalInventory.IsSorted = true;
            dmlvRentalInventory.Location = new Point(3, 785);
            dmlvRentalInventory.MinimumSize = new Size(600, 300);
            dmlvRentalInventory.ModelType = null;
            dmlvRentalInventory.MultiSelect = false;
            dmlvRentalInventory.Name = "dmlvRentalInventory";
            dmlvRentalInventory.NotSelect = true;
            dmlvRentalInventory.PageLimit = 0;
            dmlvRentalInventory.RemovingRowColor = Color.MistyRose;
            dmlvRentalInventory.ShowDeleted = WinFormsComponents.Classes.Enums.ShowRemooving.ExecNotRemoving;
            dmlvRentalInventory.Size = new Size(628, 324);
            dmlvRentalInventory.TabIndex = 35;
            dmlvRentalInventory.Visible = false;
            dmlvRentalInventory.VisibleMode = WinFormsComponents.Classes.Enums.VisibleMode.Row;
            // 
            // pbYearFilterInventoryRental
            // 
            pbYearFilterInventoryRental.BackgroundImage = Properties.Resources.date;
            pbYearFilterInventoryRental.BackgroundImageLayout = ImageLayout.Zoom;
            pbYearFilterInventoryRental.Dock = DockStyle.Fill;
            pbYearFilterInventoryRental.Location = new Point(111, 754);
            pbYearFilterInventoryRental.MaximumSize = new Size(25, 0);
            pbYearFilterInventoryRental.Name = "pbYearFilterInventoryRental";
            pbYearFilterInventoryRental.Size = new Size(25, 25);
            pbYearFilterInventoryRental.TabIndex = 34;
            pbYearFilterInventoryRental.TabStop = false;
            pbYearFilterInventoryRental.Visible = false;
            // 
            // lTitleYearFilterInventoryRental
            // 
            lTitleYearFilterInventoryRental.AutoSize = true;
            lTitleYearFilterInventoryRental.Dock = DockStyle.Fill;
            lTitleYearFilterInventoryRental.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            lTitleYearFilterInventoryRental.Location = new Point(3, 751);
            lTitleYearFilterInventoryRental.Name = "lTitleYearFilterInventoryRental";
            lTitleYearFilterInventoryRental.Size = new Size(102, 31);
            lTitleYearFilterInventoryRental.TabIndex = 33;
            lTitleYearFilterInventoryRental.Text = "Год выдачи";
            lTitleYearFilterInventoryRental.TextAlign = ContentAlignment.MiddleRight;
            lTitleYearFilterInventoryRental.Visible = false;
            // 
            // lTitleTaskFourth
            // 
            lTitleTaskFourth.AutoSize = true;
            tlpTasks.SetColumnSpan(lTitleTaskFourth, 4);
            lTitleTaskFourth.Dock = DockStyle.Fill;
            lTitleTaskFourth.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 204);
            lTitleTaskFourth.ForeColor = Color.Maroon;
            lTitleTaskFourth.Location = new Point(3, 701);
            lTitleTaskFourth.Name = "lTitleTaskFourth";
            lTitleTaskFourth.Size = new Size(628, 50);
            lTitleTaskFourth.TabIndex = 31;
            lTitleTaskFourth.Text = "D: Количество фактов выдачи инвентаря, за каждый месяц года, заданного пользователем";
            lTitleTaskFourth.TextAlign = ContentAlignment.TopCenter;
            lTitleTaskFourth.Click += lTitleTaskFourthOnClick;
            // 
            // plineThird
            // 
            plineThird.BackColor = Color.Black;
            tlpTasks.SetColumnSpan(plineThird, 4);
            plineThird.Dock = DockStyle.Fill;
            plineThird.Location = new Point(3, 693);
            plineThird.Name = "plineThird";
            plineThird.Size = new Size(628, 5);
            plineThird.TabIndex = 30;
            // 
            // lTitleTaskThird
            // 
            lTitleTaskThird.AutoSize = true;
            tlpTasks.SetColumnSpan(lTitleTaskThird, 4);
            lTitleTaskThird.Dock = DockStyle.Fill;
            lTitleTaskThird.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 204);
            lTitleTaskThird.ForeColor = Color.Maroon;
            lTitleTaskThird.Location = new Point(3, 347);
            lTitleTaskThird.Name = "lTitleTaskThird";
            lTitleTaskThird.Size = new Size(628, 25);
            lTitleTaskThird.TabIndex = 28;
            lTitleTaskThird.Text = "C:Клиенты, которые арендовали строго 3 товара из 6 категорий";
            lTitleTaskThird.TextAlign = ContentAlignment.TopCenter;
            lTitleTaskThird.Click += lTitleTaskThirdOnClick;
            // 
            // plineSecond
            // 
            plineSecond.BackColor = Color.Black;
            tlpTasks.SetColumnSpan(plineSecond, 4);
            plineSecond.Dock = DockStyle.Fill;
            plineSecond.Location = new Point(3, 339);
            plineSecond.Name = "plineSecond";
            plineSecond.Size = new Size(628, 5);
            plineSecond.TabIndex = 27;
            // 
            // btModeSerhPopular
            // 
            btModeSerhPopular.BackgroundImage = Properties.Resources.searhModeAll;
            btModeSerhPopular.BackgroundImageLayout = ImageLayout.Zoom;
            btModeSerhPopular.Dock = DockStyle.Fill;
            btModeSerhPopular.FlatAppearance.BorderSize = 0;
            btModeSerhPopular.FlatStyle = FlatStyle.Flat;
            btModeSerhPopular.Location = new Point(606, 318);
            btModeSerhPopular.Name = "btModeSerhPopular";
            btModeSerhPopular.Size = new Size(25, 15);
            btModeSerhPopular.TabIndex = 26;
            btModeSerhPopular.UseVisualStyleBackColor = true;
            btModeSerhPopular.Visible = false;
            btModeSerhPopular.Click += btModeSerhPopularOnClick;
            // 
            // lNotPopularInventory
            // 
            lNotPopularInventory.AutoSize = true;
            lNotPopularInventory.Dock = DockStyle.Fill;
            lNotPopularInventory.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            lNotPopularInventory.Location = new Point(142, 315);
            lNotPopularInventory.Name = "lNotPopularInventory";
            lNotPopularInventory.Size = new Size(458, 21);
            lNotPopularInventory.TabIndex = 25;
            lNotPopularInventory.TextAlign = ContentAlignment.MiddleLeft;
            lNotPopularInventory.Visible = false;
            // 
            // pbPNotPopularInventory
            // 
            pbPNotPopularInventory.BackgroundImage = Properties.Resources.inventory;
            pbPNotPopularInventory.BackgroundImageLayout = ImageLayout.Zoom;
            pbPNotPopularInventory.Dock = DockStyle.Fill;
            pbPNotPopularInventory.Location = new Point(111, 318);
            pbPNotPopularInventory.MaximumSize = new Size(25, 0);
            pbPNotPopularInventory.Name = "pbPNotPopularInventory";
            pbPNotPopularInventory.Size = new Size(25, 15);
            pbPNotPopularInventory.TabIndex = 24;
            pbPNotPopularInventory.TabStop = false;
            pbPNotPopularInventory.Visible = false;
            // 
            // lNotPopularInventoryTitle
            // 
            lNotPopularInventoryTitle.AutoSize = true;
            lNotPopularInventoryTitle.Dock = DockStyle.Fill;
            lNotPopularInventoryTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            lNotPopularInventoryTitle.Location = new Point(3, 315);
            lNotPopularInventoryTitle.Name = "lNotPopularInventoryTitle";
            lNotPopularInventoryTitle.Size = new Size(102, 21);
            lNotPopularInventoryTitle.TabIndex = 23;
            lNotPopularInventoryTitle.Text = "Инвентарь";
            lNotPopularInventoryTitle.TextAlign = ContentAlignment.MiddleRight;
            lNotPopularInventoryTitle.Visible = false;
            // 
            // bClientCategoryReload
            // 
            bClientCategoryReload.BackgroundImage = Properties.Resources.reload;
            bClientCategoryReload.BackgroundImageLayout = ImageLayout.Zoom;
            bClientCategoryReload.Dock = DockStyle.Fill;
            bClientCategoryReload.FlatAppearance.BorderSize = 0;
            bClientCategoryReload.FlatStyle = FlatStyle.Flat;
            bClientCategoryReload.Location = new Point(606, 125);
            bClientCategoryReload.Name = "bClientCategoryReload";
            bClientCategoryReload.Size = new Size(25, 25);
            bClientCategoryReload.TabIndex = 22;
            bClientCategoryReload.UseVisualStyleBackColor = true;
            bClientCategoryReload.Click += dbCategoryOnSelectedChange;
            // 
            // lTitleTaskSecond
            // 
            lTitleTaskSecond.AutoSize = true;
            tlpTasks.SetColumnSpan(lTitleTaskSecond, 4);
            lTitleTaskSecond.Dock = DockStyle.Fill;
            lTitleTaskSecond.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 204);
            lTitleTaskSecond.ForeColor = Color.Maroon;
            lTitleTaskSecond.Location = new Point(3, 234);
            lTitleTaskSecond.Name = "lTitleTaskSecond";
            lTitleTaskSecond.Size = new Size(628, 50);
            lTitleTaskSecond.TabIndex = 20;
            lTitleTaskSecond.Text = "B:Поиск категории с максимальным количеством выдачи и 2 самых не популярных товара в ней";
            lTitleTaskSecond.TextAlign = ContentAlignment.TopCenter;
            lTitleTaskSecond.Click += lTitleTaskSecondOnClick;
            // 
            // lSumPay
            // 
            lSumPay.AutoSize = true;
            tlpTasks.SetColumnSpan(lSumPay, 2);
            lSumPay.Dock = DockStyle.Fill;
            lSumPay.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            lSumPay.Location = new Point(142, 184);
            lSumPay.Name = "lSumPay";
            lSumPay.Size = new Size(489, 39);
            lSumPay.TabIndex = 13;
            lSumPay.TextAlign = ContentAlignment.MiddleLeft;
            lSumPay.Visible = false;
            // 
            // pbClientPay
            // 
            pbClientPay.BackgroundImage = Properties.Resources.pay;
            pbClientPay.BackgroundImageLayout = ImageLayout.Zoom;
            pbClientPay.Dock = DockStyle.Fill;
            pbClientPay.Location = new Point(111, 187);
            pbClientPay.MaximumSize = new Size(25, 0);
            pbClientPay.Name = "pbClientPay";
            pbClientPay.Size = new Size(25, 33);
            pbClientPay.TabIndex = 12;
            pbClientPay.TabStop = false;
            pbClientPay.Visible = false;
            // 
            // lClientPayTitle
            // 
            lClientPayTitle.AutoSize = true;
            lClientPayTitle.Dock = DockStyle.Fill;
            lClientPayTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            lClientPayTitle.Location = new Point(3, 184);
            lClientPayTitle.Name = "lClientPayTitle";
            lClientPayTitle.Size = new Size(102, 39);
            lClientPayTitle.TabIndex = 11;
            lClientPayTitle.Text = "Оплата";
            lClientPayTitle.TextAlign = ContentAlignment.MiddleRight;
            lClientPayTitle.Visible = false;
            // 
            // lClientPhone
            // 
            lClientPhone.AutoSize = true;
            tlpTasks.SetColumnSpan(lClientPhone, 2);
            lClientPhone.Dock = DockStyle.Fill;
            lClientPhone.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            lClientPhone.Location = new Point(142, 153);
            lClientPhone.Name = "lClientPhone";
            lClientPhone.Size = new Size(489, 31);
            lClientPhone.TabIndex = 10;
            lClientPhone.TextAlign = ContentAlignment.MiddleLeft;
            lClientPhone.Visible = false;
            // 
            // pbClientPhone
            // 
            pbClientPhone.BackgroundImage = Properties.Resources.phone;
            pbClientPhone.BackgroundImageLayout = ImageLayout.Zoom;
            pbClientPhone.Dock = DockStyle.Fill;
            pbClientPhone.Location = new Point(111, 156);
            pbClientPhone.MaximumSize = new Size(25, 0);
            pbClientPhone.Name = "pbClientPhone";
            pbClientPhone.Size = new Size(25, 25);
            pbClientPhone.TabIndex = 9;
            pbClientPhone.TabStop = false;
            pbClientPhone.Visible = false;
            // 
            // lbPhoneTitle
            // 
            lbPhoneTitle.AutoSize = true;
            lbPhoneTitle.Dock = DockStyle.Fill;
            lbPhoneTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            lbPhoneTitle.Location = new Point(3, 153);
            lbPhoneTitle.Name = "lbPhoneTitle";
            lbPhoneTitle.Size = new Size(102, 31);
            lbPhoneTitle.TabIndex = 8;
            lbPhoneTitle.Text = "Телефон";
            lbPhoneTitle.TextAlign = ContentAlignment.MiddleRight;
            lbPhoneTitle.Visible = false;
            // 
            // lClientTitle
            // 
            lClientTitle.AutoSize = true;
            lClientTitle.Dock = DockStyle.Fill;
            lClientTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            lClientTitle.Location = new Point(3, 122);
            lClientTitle.Name = "lClientTitle";
            lClientTitle.Size = new Size(102, 31);
            lClientTitle.TabIndex = 6;
            lClientTitle.Text = "Клиент";
            lClientTitle.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lTitleTaskFirst
            // 
            lTitleTaskFirst.AutoSize = true;
            tlpTasks.SetColumnSpan(lTitleTaskFirst, 4);
            lTitleTaskFirst.Dock = DockStyle.Fill;
            lTitleTaskFirst.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 204);
            lTitleTaskFirst.ForeColor = Color.Maroon;
            lTitleTaskFirst.Location = new Point(3, 0);
            lTitleTaskFirst.Name = "lTitleTaskFirst";
            lTitleTaskFirst.Size = new Size(628, 50);
            lTitleTaskFirst.TabIndex = 0;
            lTitleTaskFirst.Text = "A:Поиск клиента, который имеет максимальную сумму оплат в категории";
            lTitleTaskFirst.TextAlign = ContentAlignment.TopCenter;
            lTitleTaskFirst.Click += lTitleTaskFirstOnClick;
            // 
            // dbmpCategory
            // 
            dbmpCategory.BackColor = Color.Transparent;
            tlpTasks.SetColumnSpan(dbmpCategory, 3);
            dbmpCategory.Dock = DockStyle.Fill;
            dbmpCategory.ForColName = null;
            dbmpCategory.Image = Properties.Resources.category;
            dbmpCategory.IsNullVal = false;
            dbmpCategory.Location = new Point(111, 53);
            dbmpCategory.ModelType = null;
            dbmpCategory.Name = "dbmpCategory";
            collectionParametrs1.Limit = 0;
            collectionParametrs1.Offset = 0;
            collectionParametrs1.SerhingParametrsCount = 0;
            dbmpCategory.Parameters = collectionParametrs1;
            dbmpCategory.PKColName = "Id";
            dbmpCategory.SelectedVal = null;
            dbmpCategory.Size = new Size(520, 30);
            dbmpCategory.TabIndex = 1;
            dbmpCategory.SelectedChange += dbCategoryOnSelectedChange;
            // 
            // lHeaderTaskFirst
            // 
            lHeaderTaskFirst.AutoSize = true;
            lHeaderTaskFirst.Dock = DockStyle.Fill;
            lHeaderTaskFirst.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            lHeaderTaskFirst.Location = new Point(3, 50);
            lHeaderTaskFirst.Name = "lHeaderTaskFirst";
            tlpTasks.SetRowSpan(lHeaderTaskFirst, 2);
            lHeaderTaskFirst.Size = new Size(102, 72);
            lHeaderTaskFirst.TabIndex = 2;
            lHeaderTaskFirst.Text = "Категория";
            lHeaderTaskFirst.TextAlign = ContentAlignment.MiddleRight;
            // 
            // dmslCategory
            // 
            dmslCategory.BackColor = Color.Transparent;
            tlpTasks.SetColumnSpan(dmslCategory, 3);
            dmslCategory.Dock = DockStyle.Fill;
            dmslCategory.ForColName = null;
            dmslCategory.IconSelectedForm = (Icon)resources.GetObject("dmslCategory.IconSelectedForm");
            dmslCategory.ImageKey = "category.png";
            dmslCategory.ImageList = ilTabMenu;
            dmslCategory.IsNullVal = false;
            dmslCategory.Location = new Point(111, 89);
            dmslCategory.ModelType = null;
            dmslCategory.Name = "dmslCategory";
            collectionParametrs2.Limit = 0;
            collectionParametrs2.Offset = 0;
            collectionParametrs2.SerhingParametrsCount = 0;
            dmslCategory.Parameters = collectionParametrs2;
            dmslCategory.PKColName = "Id";
            dmslCategory.SelectedVal = null;
            dmslCategory.Size = new Size(520, 30);
            dmslCategory.TabIndex = 3;
            dmslCategory.TitleCatalogSelectedForm = "Категория";
            dmslCategory.Visible = false;
            dmslCategory.SelectedChange += dbCategoryOnSelectedChange;
            // 
            // pbClient
            // 
            pbClient.BackgroundImage = Properties.Resources.clients;
            pbClient.BackgroundImageLayout = ImageLayout.Zoom;
            pbClient.Dock = DockStyle.Fill;
            pbClient.Location = new Point(111, 125);
            pbClient.MaximumSize = new Size(25, 0);
            pbClient.Name = "pbClient";
            pbClient.Size = new Size(25, 25);
            pbClient.TabIndex = 7;
            pbClient.TabStop = false;
            // 
            // lClientCategory
            // 
            lClientCategory.AutoSize = true;
            lClientCategory.Dock = DockStyle.Fill;
            lClientCategory.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            lClientCategory.Location = new Point(142, 122);
            lClientCategory.Name = "lClientCategory";
            lClientCategory.Size = new Size(458, 31);
            lClientCategory.TabIndex = 5;
            lClientCategory.Text = "Категория не имеет оплат";
            lClientCategory.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // plineFirst
            // 
            plineFirst.BackColor = Color.Black;
            tlpTasks.SetColumnSpan(plineFirst, 4);
            plineFirst.Dock = DockStyle.Fill;
            plineFirst.Location = new Point(3, 226);
            plineFirst.Name = "plineFirst";
            plineFirst.Size = new Size(628, 5);
            plineFirst.TabIndex = 19;
            // 
            // lMaxCategoryTitle
            // 
            lMaxCategoryTitle.AutoSize = true;
            lMaxCategoryTitle.Dock = DockStyle.Fill;
            lMaxCategoryTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            lMaxCategoryTitle.Location = new Point(3, 284);
            lMaxCategoryTitle.Name = "lMaxCategoryTitle";
            lMaxCategoryTitle.Size = new Size(102, 31);
            lMaxCategoryTitle.TabIndex = 18;
            lMaxCategoryTitle.Text = "Категория";
            lMaxCategoryTitle.TextAlign = ContentAlignment.MiddleRight;
            lMaxCategoryTitle.Visible = false;
            // 
            // pbMaxCat
            // 
            pbMaxCat.BackgroundImage = Properties.Resources.category;
            pbMaxCat.BackgroundImageLayout = ImageLayout.Zoom;
            pbMaxCat.Dock = DockStyle.Fill;
            pbMaxCat.Location = new Point(111, 287);
            pbMaxCat.MaximumSize = new Size(25, 0);
            pbMaxCat.Name = "pbMaxCat";
            pbMaxCat.Size = new Size(25, 25);
            pbMaxCat.TabIndex = 16;
            pbMaxCat.TabStop = false;
            pbMaxCat.Visible = false;
            // 
            // lMaxCategory
            // 
            lMaxCategory.AutoSize = true;
            lMaxCategory.Dock = DockStyle.Fill;
            lMaxCategory.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            lMaxCategory.Location = new Point(142, 284);
            lMaxCategory.Name = "lMaxCategory";
            lMaxCategory.Size = new Size(458, 31);
            lMaxCategory.TabIndex = 17;
            lMaxCategory.Text = "Категории имеющие хотябы 1 выдачу не найдены";
            lMaxCategory.TextAlign = ContentAlignment.MiddleLeft;
            lMaxCategory.Visible = false;
            // 
            // bReloadMaxCategory
            // 
            bReloadMaxCategory.BackgroundImage = Properties.Resources.reload;
            bReloadMaxCategory.BackgroundImageLayout = ImageLayout.Zoom;
            bReloadMaxCategory.Dock = DockStyle.Fill;
            bReloadMaxCategory.FlatAppearance.BorderSize = 0;
            bReloadMaxCategory.FlatStyle = FlatStyle.Flat;
            bReloadMaxCategory.Location = new Point(606, 287);
            bReloadMaxCategory.Name = "bReloadMaxCategory";
            bReloadMaxCategory.Size = new Size(25, 25);
            bReloadMaxCategory.TabIndex = 21;
            bReloadMaxCategory.UseVisualStyleBackColor = true;
            bReloadMaxCategory.Visible = false;
            bReloadMaxCategory.Click += bReloadMaxCategoryOnClick;
            // 
            // dmlvFilterClients
            // 
            tlpTasks.SetColumnSpan(dmlvFilterClients, 4);
            dmlvFilterClients.Dock = DockStyle.Fill;
            dmlvFilterClients.Enabled = false;
            dmlvFilterClients.FilterOffColor = Color.MistyRose;
            dmlvFilterClients.FilterOnColor = Color.LightGreen;
            dmlvFilterClients.IsEditor = false;
            dmlvFilterClients.IsFilter = false;
            dmlvFilterClients.IsGridLines = true;
            dmlvFilterClients.IsRemoveRow = false;
            dmlvFilterClients.IsRepairEditor = false;
            dmlvFilterClients.IsRepairRow = false;
            dmlvFilterClients.IsSearch = false;
            dmlvFilterClients.IsShowCountAll = true;
            dmlvFilterClients.IsShowCountEnter = true;
            dmlvFilterClients.IsShowNum = false;
            dmlvFilterClients.IsSorted = true;
            dmlvFilterClients.Location = new Point(3, 375);
            dmlvFilterClients.MinimumSize = new Size(600, 300);
            dmlvFilterClients.ModelType = null;
            dmlvFilterClients.MultiSelect = false;
            dmlvFilterClients.Name = "dmlvFilterClients";
            dmlvFilterClients.NotSelect = true;
            dmlvFilterClients.PageLimit = 0;
            dmlvFilterClients.RemovingRowColor = Color.MistyRose;
            dmlvFilterClients.ShowDeleted = WinFormsComponents.Classes.Enums.ShowRemooving.ExecNotRemoving;
            dmlvFilterClients.Size = new Size(628, 312);
            dmlvFilterClients.TabIndex = 29;
            dmlvFilterClients.Visible = false;
            dmlvFilterClients.VisibleMode = WinFormsComponents.Classes.Enums.VisibleMode.Row;
            // 
            // nudYearFilterInvevntoryRental
            // 
            tlpTasks.SetColumnSpan(nudYearFilterInvevntoryRental, 2);
            nudYearFilterInvevntoryRental.Dock = DockStyle.Fill;
            nudYearFilterInvevntoryRental.Location = new Point(142, 754);
            nudYearFilterInvevntoryRental.Maximum = new decimal(new int[] { 2026, 0, 0, 0 });
            nudYearFilterInvevntoryRental.Minimum = new decimal(new int[] { 1992, 0, 0, 0 });
            nudYearFilterInvevntoryRental.Name = "nudYearFilterInvevntoryRental";
            nudYearFilterInvevntoryRental.Size = new Size(489, 23);
            nudYearFilterInvevntoryRental.TabIndex = 32;
            nudYearFilterInvevntoryRental.Value = new decimal(new int[] { 1992, 0, 0, 0 });
            nudYearFilterInvevntoryRental.Visible = false;
            nudYearFilterInvevntoryRental.ValueChanged += nudYearFilterInvevntoryRentalOnValueChanged;
            // 
            // tsInfoTasks
            // 
            tsInfoTasks.Dock = DockStyle.Bottom;
            tsInfoTasks.Items.AddRange(new ToolStripItem[] { tslInfoTask });
            tsInfoTasks.Location = new Point(3, 502);
            tsInfoTasks.Name = "tsInfoTasks";
            tsInfoTasks.Size = new Size(634, 25);
            tsInfoTasks.TabIndex = 1;
            tsInfoTasks.Text = "tsInfoTasks";
            // 
            // tslInfoTask
            // 
            tslInfoTask.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            tslInfoTask.ForeColor = Color.Red;
            tslInfoTask.Name = "tslInfoTask";
            tslInfoTask.Size = new Size(542, 22);
            tslInfoTask.Text = "Для получения решения задания, нажмите шапку с его названием";
            // 
            // cStatisticQuarter
            // 
            cStatisticQuarter.Dock = DockStyle.Fill;
            cStatisticQuarter.Location = new Point(320, 3);
            cStatisticQuarter.Name = "cStatisticQuarter";
            cStatisticQuarter.Size = new Size(311, 493);
            cStatisticQuarter.TabIndex = 0;
            cStatisticQuarter.Text = "chart1";
            // 
            // cStatisticAllPeriod
            // 
            cStatisticAllPeriod.Dock = DockStyle.Fill;
            cStatisticAllPeriod.Location = new Point(3, 3);
            cStatisticAllPeriod.Name = "cStatisticAllPeriod";
            cStatisticAllPeriod.Size = new Size(311, 493);
            cStatisticAllPeriod.TabIndex = 1;
            cStatisticAllPeriod.Text = "chart2";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(648, 583);
            Controls.Add(tcStatistic);
            Controls.Add(tsMainMenu);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(600, 400);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ПРОКАТ";
            Load += MainFormOnLoad;
            KeyDown += MainFormOnKeyDown;
            tsMainMenu.ResumeLayout(false);
            tsMainMenu.PerformLayout();
            tpStatistic.ResumeLayout(false);
            tpStatistic.PerformLayout();
            tlpStatistic.ResumeLayout(false);
            tsStatistic.ResumeLayout(false);
            tsStatistic.PerformLayout();
            tcStatistic.ResumeLayout(false);
            tpTasks.ResumeLayout(false);
            tpTasks.PerformLayout();
            tlpTasks.ResumeLayout(false);
            tlpTasks.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pbYearFilterInventoryRental).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbPNotPopularInventory).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbClientPay).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbClientPhone).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbClient).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbMaxCat).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudYearFilterInvevntoryRental).EndInit();
            tsInfoTasks.ResumeLayout(false);
            tsInfoTasks.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)cStatisticQuarter).EndInit();
            ((System.ComponentModel.ISupportInitialize)cStatisticAllPeriod).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip tsMainMenu;
        private ToolStripButton tsbSetings;
        private ImageList ilTabMenu;
        private ToolStripButton tsbCatalogs;
        private ToolStripButton tsbClients;
        private ToolStripButton tsbJournal;
        private TabPage tpStatistic;
        private TabControl tcStatistic;
        private TabPage tpTasks;
        private TableLayoutPanel tlpTasks;
        private Label lTitleTaskFirst;
        private WinFormsComponents.Controls.DBModelPicker dbmpCategory;
        private Label lHeaderTaskFirst;
        private WinFormsComponents.Controls.DBModelSelectedList dmslCategory;
        private Label lClientCategory;
        private Label lClientTitle;
        private PictureBox pbClient;
        private Label lbPhoneTitle;
        private Label lClientPhone;
        private PictureBox pbClientPhone;
        private Label lSumPay;
        private PictureBox pbClientPay;
        private Label lClientPayTitle;
        private Label lMaxCategory;
        private PictureBox pbMaxCat;
        private Panel plineFirst;
        private Label lTitleTaskSecond;
        private Label lMaxCategoryTitle;
        private Button bReloadMaxCategory;
        private Button bClientCategoryReload;
        private TableLayoutPanel tlpStatistic;
        private ToolStrip tsStatistic;
        private ToolStripButton tsbReloadStatistics;
        private Label lNotPopularInventory;
        private PictureBox pbPNotPopularInventory;
        private Label lNotPopularInventoryTitle;
        private Button btModeSerhPopular;
        private Panel plineSecond;
        private Label lTitleTaskThird;
        private WinFormsComponents.Controls.DBModelListView dmlvFilterClients;
        private Panel plineThird;
        private Label lTitleTaskFourth;
        private PictureBox pbYearFilterInventoryRental;
        private Label lTitleYearFilterInventoryRental;
        private NumericUpDown nudYearFilterInvevntoryRental;
        private WinFormsComponents.Controls.DBModelListView dmlvRentalInventory;
        private ToolStrip tsInfoTasks;
        private ToolStripLabel tslInfoTask;
        private System.Windows.Forms.DataVisualization.Charting.Chart cStatisticQuarter;
        private System.Windows.Forms.DataVisualization.Charting.Chart cStatisticAllPeriod;
    }
}
