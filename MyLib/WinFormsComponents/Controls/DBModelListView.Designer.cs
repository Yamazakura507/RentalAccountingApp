namespace WinFormsComponents.Controls
{
    partial class DBModelListView
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            tsListMenu = new ToolStrip();
            tsbAdd = new ToolStripButton();
            tsbDel = new ToolStripButton();
            tsbRepair = new ToolStripButton();
            tsbEdit = new ToolStripButton();
            tssFilter = new ToolStripSeparator();
            tstbSearh = new ToolStripTextBox();
            tsbSearh = new ToolStripButton();
            tsddbFilter = new ToolStripDropDownButton();
            tsmiSearh = new ToolStripMenuItem();
            tsmiFilter = new ToolStripMenuItem();
            tsmiShowDeleted = new ToolStripMenuItem();
            tsmiShowAlways = new ToolStripMenuItem();
            tsmiShowExacRemoving = new ToolStripMenuItem();
            tsmiShowExacNotRemoving = new ToolStripMenuItem();
            tsmiSorted = new ToolStripMenuItem();
            tsddbSettingsListView = new ToolStripDropDownButton();
            tsmiPager = new ToolStripMenuItem();
            tsmiPagerCheckit = new ToolStripMenuItem();
            tslPager = new ToolStripLabel();
            tsmitbLimitPage = new ToolStripTextBox();
            tsmiRepairLimitPage = new ToolStripMenuItem();
            tsmiAllCountShow = new ToolStripMenuItem();
            tsmiEnterCountShow = new ToolStripMenuItem();
            tsmiNumeretorVisible = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            tsbTileMode = new ToolStripButton();
            tsbRowMode = new ToolStripButton();
            tsbGrid = new ToolStripButton();
            tsbNonGrid = new ToolStripButton();
            tssPager = new ToolStripSeparator();
            tsbStartPage = new ToolStripButton();
            tsbBackPage = new ToolStripButton();
            tstbActualPage = new ToolStripTextBox();
            tslCountPages = new ToolStripLabel();
            tsbNextPage = new ToolStripButton();
            tsbEndPage = new ToolStripButton();
            lvModel = new ListView();
            cmsModel = new ContextMenuStrip(components);
            tsmiAdd = new ToolStripMenuItem();
            tsmiDel = new ToolStripMenuItem();
            tsmiRepair = new ToolStripMenuItem();
            tsmiEdit = new ToolStripMenuItem();
            tsIformationBar = new ToolStrip();
            tslAllCount = new ToolStripLabel();
            tslEnterCount = new ToolStripLabel();
            tlp = new TableLayoutPanel();
            cmsSorted = new ContextMenuStrip(components);
            tsListMenu.SuspendLayout();
            cmsModel.SuspendLayout();
            tsIformationBar.SuspendLayout();
            tlp.SuspendLayout();
            SuspendLayout();
            // 
            // tsListMenu
            // 
            tsListMenu.Items.AddRange(new ToolStripItem[] { tsbAdd, tsbDel, tsbRepair, tsbEdit, tssFilter, tstbSearh, tsbSearh, tsddbFilter, tsddbSettingsListView, toolStripSeparator1, tsbTileMode, tsbRowMode, tsbGrid, tsbNonGrid, tssPager, tsbStartPage, tsbBackPage, tstbActualPage, tslCountPages, tsbNextPage, tsbEndPage });
            tsListMenu.Location = new Point(0, 0);
            tsListMenu.Name = "tsListMenu";
            tsListMenu.Size = new Size(750, 31);
            tsListMenu.TabIndex = 2;
            tsListMenu.Text = "toolStrip1";
            // 
            // tsbAdd
            // 
            tsbAdd.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsbAdd.Image = Properties.Resources.add;
            tsbAdd.ImageTransparentColor = Color.Magenta;
            tsbAdd.Name = "tsbAdd";
            tsbAdd.Size = new Size(23, 28);
            tsbAdd.Text = "Добавить";
            tsbAdd.ToolTipText = "Добавить(Insert)";
            tsbAdd.Click += tsbInsertOnClick;
            // 
            // tsbDel
            // 
            tsbDel.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsbDel.Image = Properties.Resources.delete;
            tsbDel.ImageTransparentColor = Color.Magenta;
            tsbDel.Name = "tsbDel";
            tsbDel.Size = new Size(23, 28);
            tsbDel.Text = "Удалить";
            tsbDel.ToolTipText = "Удалить(Delete)";
            tsbDel.Visible = false;
            tsbDel.Click += tsbDelOrRepairOnClick;
            // 
            // tsbRepair
            // 
            tsbRepair.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsbRepair.Image = Properties.Resources.undelete;
            tsbRepair.ImageTransparentColor = Color.Magenta;
            tsbRepair.Name = "tsbRepair";
            tsbRepair.Size = new Size(23, 28);
            tsbRepair.Text = "Востановить";
            tsbRepair.ToolTipText = "Востановить(Ctrl+R)";
            tsbRepair.Visible = false;
            tsbRepair.Click += tsbDelOrRepairOnClick;
            // 
            // tsbEdit
            // 
            tsbEdit.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsbEdit.Image = Properties.Resources.editor;
            tsbEdit.ImageTransparentColor = Color.Magenta;
            tsbEdit.Name = "tsbEdit";
            tsbEdit.Size = new Size(23, 28);
            tsbEdit.Text = "Редактировать(Enter)";
            tsbEdit.ToolTipText = "Редактировать(Enter)";
            tsbEdit.Visible = false;
            tsbEdit.Click += tsbEditOnClick;
            // 
            // tssFilter
            // 
            tssFilter.Name = "tssFilter";
            tssFilter.Size = new Size(6, 31);
            // 
            // tstbSearh
            // 
            tstbSearh.BorderStyle = BorderStyle.FixedSingle;
            tstbSearh.Name = "tstbSearh";
            tstbSearh.Size = new Size(100, 31);
            tstbSearh.ToolTipText = "Поиск(Enter)";
            tstbSearh.KeyPress += tstbSearhOnKeyPress;
            // 
            // tsbSearh
            // 
            tsbSearh.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsbSearh.Image = Properties.Resources.searh;
            tsbSearh.ImageTransparentColor = Color.Magenta;
            tsbSearh.Name = "tsbSearh";
            tsbSearh.Size = new Size(23, 28);
            tsbSearh.Text = "Поиск";
            tsbSearh.ToolTipText = "Поиск";
            tsbSearh.Click += tsbSearhOnClick;
            // 
            // tsddbFilter
            // 
            tsddbFilter.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsddbFilter.DropDownItems.AddRange(new ToolStripItem[] { tsmiSearh, tsmiFilter, tsmiShowDeleted, tsmiSorted });
            tsddbFilter.Image = Properties.Resources.filter;
            tsddbFilter.ImageTransparentColor = Color.Magenta;
            tsddbFilter.Name = "tsddbFilter";
            tsddbFilter.Size = new Size(29, 28);
            tsddbFilter.Text = "Фильтр";
            tsddbFilter.ToolTipText = "Фильтр(Поиск/Фильтрация/Сортировка)";
            // 
            // tsmiSearh
            // 
            tsmiSearh.Image = Properties.Resources.searh;
            tsmiSearh.Name = "tsmiSearh";
            tsmiSearh.Size = new Size(187, 22);
            tsmiSearh.Text = "Параметры поиска";
            tsmiSearh.ToolTipText = "Параметры поиска";
            tsmiSearh.Visible = false;
            // 
            // tsmiFilter
            // 
            tsmiFilter.Image = Properties.Resources.filter;
            tsmiFilter.Name = "tsmiFilter";
            tsmiFilter.Size = new Size(187, 22);
            tsmiFilter.Text = "Фильтрация";
            tsmiFilter.ToolTipText = "Фильтрация";
            tsmiFilter.Visible = false;
            // 
            // tsmiShowDeleted
            // 
            tsmiShowDeleted.DropDownItems.AddRange(new ToolStripItem[] { tsmiShowAlways, tsmiShowExacRemoving, tsmiShowExacNotRemoving });
            tsmiShowDeleted.Image = Properties.Resources.visible_delete;
            tsmiShowDeleted.Name = "tsmiShowDeleted";
            tsmiShowDeleted.Size = new Size(187, 22);
            tsmiShowDeleted.Text = "Показать удаленные";
            tsmiShowDeleted.ToolTipText = "Показать удаленные";
            tsmiShowDeleted.DropDownOpened += tsmiShowDeletedOnDropDownOpened;
            // 
            // tsmiShowAlways
            // 
            tsmiShowAlways.Image = Properties.Resources.undelete;
            tsmiShowAlways.Name = "tsmiShowAlways";
            tsmiShowAlways.Size = new Size(234, 22);
            tsmiShowAlways.Tag = "0";
            tsmiShowAlways.Text = "Все(Ctrl+A)";
            tsmiShowAlways.ToolTipText = "Отобразить все(Ctrl+S+A)";
            tsmiShowAlways.Click += tsmiRemoveModeShowOnClick;
            // 
            // tsmiShowExacRemoving
            // 
            tsmiShowExacRemoving.Image = Properties.Resources.delete;
            tsmiShowExacRemoving.Name = "tsmiShowExacRemoving";
            tsmiShowExacRemoving.Size = new Size(234, 22);
            tsmiShowExacRemoving.Tag = "1";
            tsmiShowExacRemoving.Text = "Только удаленные(Ctrl+D)";
            tsmiShowExacRemoving.ToolTipText = "Отобразить только удаленные(Ctrl+S+R)";
            tsmiShowExacRemoving.Click += tsmiRemoveModeShowOnClick;
            // 
            // tsmiShowExacNotRemoving
            // 
            tsmiShowExacNotRemoving.Image = Properties.Resources.visible_delete;
            tsmiShowExacNotRemoving.Name = "tsmiShowExacNotRemoving";
            tsmiShowExacNotRemoving.Size = new Size(234, 22);
            tsmiShowExacNotRemoving.Tag = "2";
            tsmiShowExacNotRemoving.Text = "Только не удаленные(Ctrl+V)";
            tsmiShowExacNotRemoving.ToolTipText = "Отобразить только не удаленные(Ctrl+S+V)";
            tsmiShowExacNotRemoving.Click += tsmiRemoveModeShowOnClick;
            // 
            // tsmiSorted
            // 
            tsmiSorted.Image = Properties.Resources.order;
            tsmiSorted.Name = "tsmiSorted";
            tsmiSorted.Size = new Size(187, 22);
            tsmiSorted.Text = "Сортировка";
            tsmiSorted.ToolTipText = "Сортировка";
            // 
            // tsddbSettingsListView
            // 
            tsddbSettingsListView.Alignment = ToolStripItemAlignment.Right;
            tsddbSettingsListView.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsddbSettingsListView.DropDownItems.AddRange(new ToolStripItem[] { tsmiPager, tsmiAllCountShow, tsmiEnterCountShow, tsmiNumeretorVisible });
            tsddbSettingsListView.Image = Properties.Resources.setings;
            tsddbSettingsListView.ImageTransparentColor = Color.Magenta;
            tsddbSettingsListView.Name = "tsddbSettingsListView";
            tsddbSettingsListView.Size = new Size(29, 28);
            tsddbSettingsListView.Text = "Настройки и расширения списка";
            // 
            // tsmiPager
            // 
            tsmiPager.DropDownItems.AddRange(new ToolStripItem[] { tsmiPagerCheckit, tslPager, tsmitbLimitPage, tsmiRepairLimitPage });
            tsmiPager.Image = Properties.Resources.pager;
            tsmiPager.Name = "tsmiPager";
            tsmiPager.Size = new Size(307, 22);
            tsmiPager.Text = "Страничник";
            tsmiPager.ToolTipText = "Страничник";
            // 
            // tsmiPagerCheckit
            // 
            tsmiPagerCheckit.BackColor = Color.MistyRose;
            tsmiPagerCheckit.CheckOnClick = true;
            tsmiPagerCheckit.Image = Properties.Resources.uncheckible;
            tsmiPagerCheckit.Name = "tsmiPagerCheckit";
            tsmiPagerCheckit.Size = new Size(171, 22);
            tsmiPagerCheckit.Text = "Включить(Ctrl+P)";
            tsmiPagerCheckit.ToolTipText = "Включить постраничный вывод(Ctrl+P)";
            tsmiPagerCheckit.CheckedChanged += tsmiPagerCheckitOnCheckedChanged;
            // 
            // tslPager
            // 
            tslPager.Name = "tslPager";
            tslPager.Size = new Size(106, 15);
            tslPager.Text = "Количество строк";
            tslPager.Visible = false;
            // 
            // tsmitbLimitPage
            // 
            tsmitbLimitPage.BorderStyle = BorderStyle.FixedSingle;
            tsmitbLimitPage.Name = "tsmitbLimitPage";
            tsmitbLimitPage.Size = new Size(100, 23);
            tsmitbLimitPage.ToolTipText = "Укажите количество выводимых строк или плиток на одной странице";
            tsmitbLimitPage.Visible = false;
            tsmitbLimitPage.KeyPress += tsmitbLimitPageOnKeyPress;
            // 
            // tsmiRepairLimitPage
            // 
            tsmiRepairLimitPage.Image = Properties.Resources.repair_setting;
            tsmiRepairLimitPage.Name = "tsmiRepairLimitPage";
            tsmiRepairLimitPage.Size = new Size(171, 22);
            tsmiRepairLimitPage.Text = "Востановить";
            tsmiRepairLimitPage.ToolTipText = "Востановить значение из настроек";
            tsmiRepairLimitPage.Visible = false;
            tsmiRepairLimitPage.Click += tsmiRepairLimitPageOnClick;
            // 
            // tsmiAllCountShow
            // 
            tsmiAllCountShow.BackColor = Color.MistyRose;
            tsmiAllCountShow.CheckOnClick = true;
            tsmiAllCountShow.Image = Properties.Resources.uncheckible;
            tsmiAllCountShow.Name = "tsmiAllCountShow";
            tsmiAllCountShow.Size = new Size(307, 22);
            tsmiAllCountShow.Text = "Отобразить общее количество(Ctrl+F)";
            tsmiAllCountShow.ToolTipText = "Отобразить общее количество строк(Ctrl+F)";
            tsmiAllCountShow.CheckedChanged += tsmiAllCountShowOnCheckedChanged;
            // 
            // tsmiEnterCountShow
            // 
            tsmiEnterCountShow.BackColor = Color.MistyRose;
            tsmiEnterCountShow.CheckOnClick = true;
            tsmiEnterCountShow.Image = Properties.Resources.uncheckible;
            tsmiEnterCountShow.Name = "tsmiEnterCountShow";
            tsmiEnterCountShow.Size = new Size(307, 22);
            tsmiEnterCountShow.Text = "Отобразить выбраное количество(Ctrl+Q)";
            tsmiEnterCountShow.ToolTipText = "Отобразить выбраное количество строк(Ctrl+Q)";
            tsmiEnterCountShow.CheckedChanged += tsmiEnterCountShowOnCheckedChanged;
            // 
            // tsmiNumeretorVisible
            // 
            tsmiNumeretorVisible.BackColor = Color.MistyRose;
            tsmiNumeretorVisible.CheckOnClick = true;
            tsmiNumeretorVisible.Image = Properties.Resources.uncheckible;
            tsmiNumeretorVisible.Name = "tsmiNumeretorVisible";
            tsmiNumeretorVisible.Size = new Size(307, 22);
            tsmiNumeretorVisible.Text = "Включить нумерацию строк(Ctrl+I)";
            tsmiNumeretorVisible.ToolTipText = "Отобразить колонку с номерами строк(Ctrl+I)";
            tsmiNumeretorVisible.CheckedChanged += tsmiNumeretorVisibleOnCheckedChanged;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Alignment = ToolStripItemAlignment.Right;
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 31);
            // 
            // tsbTileMode
            // 
            tsbTileMode.Alignment = ToolStripItemAlignment.Right;
            tsbTileMode.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsbTileMode.Image = Properties.Resources.tile_mode;
            tsbTileMode.ImageTransparentColor = Color.Magenta;
            tsbTileMode.Name = "tsbTileMode";
            tsbTileMode.Size = new Size(23, 28);
            tsbTileMode.Tag = "0";
            tsbTileMode.Text = "Отображать плиткой";
            tsbTileMode.ToolTipText = "Отображать плиткой(Ctrl+U)";
            tsbTileMode.Click += tsbVisibleModeOnClick;
            // 
            // tsbRowMode
            // 
            tsbRowMode.Alignment = ToolStripItemAlignment.Right;
            tsbRowMode.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsbRowMode.Image = Properties.Resources.row_mode;
            tsbRowMode.ImageTransparentColor = Color.Magenta;
            tsbRowMode.Name = "tsbRowMode";
            tsbRowMode.Size = new Size(23, 28);
            tsbRowMode.Tag = "1";
            tsbRowMode.Text = "Отображать строками";
            tsbRowMode.ToolTipText = "Отображать строками(Ctrl+U)";
            tsbRowMode.Visible = false;
            tsbRowMode.Click += tsbVisibleModeOnClick;
            // 
            // tsbGrid
            // 
            tsbGrid.Alignment = ToolStripItemAlignment.Right;
            tsbGrid.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsbGrid.Image = Properties.Resources.grid;
            tsbGrid.ImageTransparentColor = Color.Magenta;
            tsbGrid.Name = "tsbGrid";
            tsbGrid.Size = new Size(23, 28);
            tsbGrid.Text = "Отобразить сетку";
            tsbGrid.ToolTipText = "Отобразить сетку(Ctrl+G)";
            tsbGrid.Visible = false;
            tsbGrid.Click += tsbGridOnClick;
            // 
            // tsbNonGrid
            // 
            tsbNonGrid.Alignment = ToolStripItemAlignment.Right;
            tsbNonGrid.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsbNonGrid.Image = Properties.Resources.non_grid;
            tsbNonGrid.ImageTransparentColor = Color.Magenta;
            tsbNonGrid.Name = "tsbNonGrid";
            tsbNonGrid.Size = new Size(23, 28);
            tsbNonGrid.Text = "Скрыть сетку";
            tsbNonGrid.ToolTipText = "Скрыть сетку(Ctrl+G)";
            tsbNonGrid.Click += tsbGridOnClick;
            // 
            // tssPager
            // 
            tssPager.Name = "tssPager";
            tssPager.Size = new Size(6, 31);
            // 
            // tsbStartPage
            // 
            tsbStartPage.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsbStartPage.Image = Properties.Resources.left_dou_arrow;
            tsbStartPage.ImageTransparentColor = Color.Magenta;
            tsbStartPage.Name = "tsbStartPage";
            tsbStartPage.Size = new Size(23, 28);
            tsbStartPage.Tag = "2";
            tsbStartPage.Text = "В начало списка(Ctrl+H)";
            tsbStartPage.Click += tsbActionPageOnClick;
            // 
            // tsbBackPage
            // 
            tsbBackPage.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsbBackPage.Image = Properties.Resources.left_arrow;
            tsbBackPage.ImageTransparentColor = Color.Magenta;
            tsbBackPage.Name = "tsbBackPage";
            tsbBackPage.Size = new Size(23, 28);
            tsbBackPage.Tag = "1";
            tsbBackPage.Text = "Предыдущая страница(Ctrl+B)";
            tsbBackPage.Click += tsbActionPageOnClick;
            // 
            // tstbActualPage
            // 
            tstbActualPage.BorderStyle = BorderStyle.FixedSingle;
            tstbActualPage.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            tstbActualPage.Margin = new Padding(1, 0, 1, 2);
            tstbActualPage.Name = "tstbActualPage";
            tstbActualPage.Size = new Size(40, 29);
            tstbActualPage.Tag = "0";
            tstbActualPage.Text = "1";
            tstbActualPage.TextBoxTextAlign = HorizontalAlignment.Center;
            tstbActualPage.ToolTipText = "Текущая старница(Enter)";
            tstbActualPage.KeyPress += tstbActualPageOnKeyPress;
            // 
            // tslCountPages
            // 
            tslCountPages.Font = new Font("Segoe UI", 12F);
            tslCountPages.Name = "tslCountPages";
            tslCountPages.Size = new Size(37, 28);
            tslCountPages.Text = "/cnt";
            tslCountPages.ToolTipText = "Всего cnt страниц";
            // 
            // tsbNextPage
            // 
            tsbNextPage.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsbNextPage.Image = Properties.Resources.right_arrow;
            tsbNextPage.ImageTransparentColor = Color.Magenta;
            tsbNextPage.Name = "tsbNextPage";
            tsbNextPage.Size = new Size(23, 28);
            tsbNextPage.Tag = "0";
            tsbNextPage.Text = "Следующая страница(Ctrl+N)";
            tsbNextPage.ToolTipText = "Следующая страница(Ctrl+N)";
            tsbNextPage.Click += tsbActionPageOnClick;
            // 
            // tsbEndPage
            // 
            tsbEndPage.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsbEndPage.Image = Properties.Resources.right_dou_arrow;
            tsbEndPage.ImageTransparentColor = Color.Magenta;
            tsbEndPage.Name = "tsbEndPage";
            tsbEndPage.Size = new Size(23, 28);
            tsbEndPage.Tag = "3";
            tsbEndPage.Text = "Последняя страница(Ctrl+E)";
            tsbEndPage.Click += tsbActionPageOnClick;
            // 
            // lvModel
            // 
            lvModel.ContextMenuStrip = cmsModel;
            lvModel.Dock = DockStyle.Fill;
            lvModel.FullRowSelect = true;
            lvModel.GridLines = true;
            lvModel.Location = new Point(3, 3);
            lvModel.Name = "lvModel";
            lvModel.Size = new Size(744, 363);
            lvModel.TabIndex = 3;
            lvModel.UseCompatibleStateImageBehavior = false;
            lvModel.View = View.Details;
            lvModel.SelectedIndexChanged += lvModelOnSelectedIndexChanged;
            lvModel.MouseDoubleClick += lvModelOnMouseDoubleClick;
            // 
            // cmsModel
            // 
            cmsModel.Items.AddRange(new ToolStripItem[] { tsmiAdd, tsmiDel, tsmiRepair, tsmiEdit });
            cmsModel.Name = "cmsModel";
            cmsModel.Size = new Size(190, 92);
            cmsModel.Opening += cmsModelOnOpening;
            // 
            // tsmiAdd
            // 
            tsmiAdd.Image = Properties.Resources.add;
            tsmiAdd.Name = "tsmiAdd";
            tsmiAdd.Size = new Size(189, 22);
            tsmiAdd.Text = "Добавить(Insert)";
            tsmiAdd.ToolTipText = "Добавить(Insert)";
            tsmiAdd.Click += tsbInsertOnClick;
            // 
            // tsmiDel
            // 
            tsmiDel.Image = Properties.Resources.delete;
            tsmiDel.Name = "tsmiDel";
            tsmiDel.Size = new Size(189, 22);
            tsmiDel.Text = "Удалить(Delete)";
            tsmiDel.ToolTipText = "Удалить(Delete)";
            tsmiDel.Click += tsbDelOrRepairOnClick;
            // 
            // tsmiRepair
            // 
            tsmiRepair.Image = Properties.Resources.undelete;
            tsmiRepair.Name = "tsmiRepair";
            tsmiRepair.Size = new Size(189, 22);
            tsmiRepair.Text = "Востановить(Ctrl+R)";
            tsmiRepair.ToolTipText = "Востановить(Ctrl+R)";
            tsmiRepair.Visible = false;
            tsmiRepair.Click += tsbDelOrRepairOnClick;
            // 
            // tsmiEdit
            // 
            tsmiEdit.Image = Properties.Resources.editor;
            tsmiEdit.Name = "tsmiEdit";
            tsmiEdit.Size = new Size(189, 22);
            tsmiEdit.Text = "Редактировать(Enter)";
            tsmiEdit.ToolTipText = "Редактировать(Enter)";
            tsmiEdit.Visible = false;
            tsmiEdit.Click += tsbEditOnClick;
            // 
            // tsIformationBar
            // 
            tsIformationBar.Dock = DockStyle.Bottom;
            tsIformationBar.Items.AddRange(new ToolStripItem[] { tslAllCount, tslEnterCount });
            tsIformationBar.Location = new Point(0, 344);
            tsIformationBar.Name = "tsIformationBar";
            tsIformationBar.Size = new Size(750, 25);
            tsIformationBar.TabIndex = 4;
            tsIformationBar.Visible = false;
            // 
            // tslAllCount
            // 
            tslAllCount.Alignment = ToolStripItemAlignment.Right;
            tslAllCount.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            tslAllCount.Name = "tslAllCount";
            tslAllCount.Size = new Size(71, 22);
            tslAllCount.Text = "Всего: 0";
            tslAllCount.ToolTipText = "Количество строк в списке всего";
            tslAllCount.Visible = false;
            // 
            // tslEnterCount
            // 
            tslEnterCount.Alignment = ToolStripItemAlignment.Right;
            tslEnterCount.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            tslEnterCount.Name = "tslEnterCount";
            tslEnterCount.Size = new Size(99, 22);
            tslEnterCount.Text = "Выбрано: 0";
            tslEnterCount.ToolTipText = "Количество выбраных строк";
            tslEnterCount.Visible = false;
            // 
            // tlp
            // 
            tlp.ColumnCount = 1;
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlp.Controls.Add(lvModel, 0, 0);
            tlp.Controls.Add(tsIformationBar, 0, 1);
            tlp.Dock = DockStyle.Fill;
            tlp.Location = new Point(0, 31);
            tlp.Name = "tlp";
            tlp.RowCount = 2;
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlp.RowStyles.Add(new RowStyle());
            tlp.Size = new Size(750, 369);
            tlp.TabIndex = 5;
            // 
            // cmsSorted
            // 
            cmsSorted.Name = "cmsSorted";
            cmsSorted.Size = new Size(61, 4);
            // 
            // DBModelListView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tlp);
            Controls.Add(tsListMenu);
            MinimumSize = new Size(530, 130);
            Name = "DBModelListView";
            Size = new Size(750, 400);
            Load += DBModelListViewOnLoad;
            tsListMenu.ResumeLayout(false);
            tsListMenu.PerformLayout();
            cmsModel.ResumeLayout(false);
            tsIformationBar.ResumeLayout(false);
            tsIformationBar.PerformLayout();
            tlp.ResumeLayout(false);
            tlp.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip tsListMenu;
        private ToolStripButton tsbAdd;
        private ToolStripButton tsbDel;
        private ToolStripSeparator tssFilter;
        private ToolStripTextBox tstbSearh;
        private ToolStripButton tsbSearh;
        private ToolStripDropDownButton tsddbFilter;
        private ToolStripMenuItem tsmiSearh;
        private ToolStripMenuItem tsmiFilter;
        private ContextMenuStrip cmsModel;
        public ListView lvModel;
        private ToolStripMenuItem tsmiShowDeleted;
        private ToolStripButton tsbRepair;
        private ToolStripMenuItem tsmiAdd;
        private ToolStripMenuItem tsmiDel;
        private ToolStripMenuItem tsmiRepair;
        private ToolStripMenuItem tsmiShowAlways;
        private ToolStripMenuItem tsmiShowExacRemoving;
        private ToolStripMenuItem tsmiShowExacNotRemoving;
        private ToolStripButton tsbTileMode;
        private ToolStripButton tsbRowMode;
        private ToolStripButton tsbGrid;
        private ToolStripButton tsbNonGrid;
        private ToolStripSeparator tssPager;
        private ToolStripButton tsbStartPage;
        private ToolStripButton tsbBackPage;
        private ToolStripTextBox tstbActualPage;
        private ToolStripLabel tslCountPages;
        private ToolStripButton tsbNextPage;
        private ToolStripButton tsbEndPage;
        private ToolStripDropDownButton tsddbSettingsListView;
        private ToolStripMenuItem tsmiPagerCheckit;
        private ToolStripLabel tslPager;
        private ToolStripTextBox tsmitbLimitPage;
        private ToolStripMenuItem tsmiRepairLimitPage;
        private ToolStrip tsIformationBar;
        private ToolStripLabel tslAllCount;
        private ToolStripLabel tslEnterCount;
        private ToolStripSeparator toolStripSeparator1;
        public ToolStripMenuItem tsmiAllCountShow;
        public ToolStripMenuItem tsmiEnterCountShow;
        public ToolStripMenuItem tsmiPager;
        private TableLayoutPanel tlp;
        private ToolStripMenuItem tsmiEdit;
        private ToolStripButton tsbEdit;
        private ToolStripMenuItem tsmiNumeretorVisible;
        private ToolStripMenuItem tsmiSorted;
        private ContextMenuStrip cmsSorted;
    }
}
