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
            DataBaseProvaider.Objects.CollectionParametrs collectionParametrs2 = new DataBaseProvaider.Objects.CollectionParametrs();
            tsMainMenu = new ToolStrip();
            tsbSetings = new ToolStripButton();
            tsbCatalogs = new ToolStripButton();
            ilTabMenu = new ImageList(components);
            tcDBViewr = new TabControl();
            tpInventory = new TabPage();
            dmlvInventory = new WinFormsComponents.Controls.DBModelListView();
            tsbClients = new ToolStripButton();
            tsMainMenu.SuspendLayout();
            tcDBViewr.SuspendLayout();
            tpInventory.SuspendLayout();
            SuspendLayout();
            // 
            // tsMainMenu
            // 
            tsMainMenu.Items.AddRange(new ToolStripItem[] { tsbSetings, tsbCatalogs, tsbClients });
            tsMainMenu.Location = new Point(0, 0);
            tsMainMenu.Name = "tsMainMenu";
            tsMainMenu.Size = new Size(800, 25);
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
            tsbCatalogs.ToolTipText = "Справочники()";
            tsbCatalogs.Click += tsbCatalogsOnClick;
            // 
            // ilTabMenu
            // 
            ilTabMenu.ColorDepth = ColorDepth.Depth32Bit;
            ilTabMenu.ImageStream = (ImageListStreamer)resources.GetObject("ilTabMenu.ImageStream");
            ilTabMenu.TransparentColor = Color.Transparent;
            ilTabMenu.Images.SetKeyName(0, "inventory.png");
            ilTabMenu.Images.SetKeyName(1, "clients.png");
            // 
            // tcDBViewr
            // 
            tcDBViewr.Controls.Add(tpInventory);
            tcDBViewr.Dock = DockStyle.Fill;
            tcDBViewr.ImageList = ilTabMenu;
            tcDBViewr.Location = new Point(0, 25);
            tcDBViewr.Name = "tcDBViewr";
            tcDBViewr.SelectedIndex = 0;
            tcDBViewr.Size = new Size(800, 425);
            tcDBViewr.TabIndex = 1;
            tcDBViewr.KeyDown += tcDBViewrOnKeyDown;
            // 
            // tpInventory
            // 
            tpInventory.Controls.Add(dmlvInventory);
            tpInventory.ImageKey = "inventory.png";
            tpInventory.Location = new Point(4, 24);
            tpInventory.Name = "tpInventory";
            tpInventory.Padding = new Padding(3);
            tpInventory.Size = new Size(792, 397);
            tpInventory.TabIndex = 2;
            tpInventory.Text = "Инвентарь";
            tpInventory.UseVisualStyleBackColor = true;
            // 
            // dmlvInventory
            // 
            dmlvInventory.Dock = DockStyle.Fill;
            dmlvInventory.Enabled = false;
            dmlvInventory.FilterOffColor = Color.MistyRose;
            dmlvInventory.FilterOnColor = Color.LightGreen;
            dmlvInventory.ImageList = ilTabMenu;
            dmlvInventory.IsEditor = false;
            dmlvInventory.IsFilter = true;
            dmlvInventory.IsGridLines = true;
            dmlvInventory.IsRepairEditor = true;
            dmlvInventory.IsRepairRow = true;
            dmlvInventory.IsSearch = true;
            dmlvInventory.IsShowCountAll = true;
            dmlvInventory.IsShowCountEnter = true;
            dmlvInventory.IsShowNum = false;
            dmlvInventory.IsSorted = true;
            dmlvInventory.Location = new Point(3, 3);
            dmlvInventory.MinimumSize = new Size(530, 130);
            dmlvInventory.ModelType = null;
            dmlvInventory.Name = "dmlvInventory";
            dmlvInventory.PageLimit = 0;
            collectionParametrs2.Limit = 0;
            collectionParametrs2.Offset = 0;
            collectionParametrs2.SerhingParametrsCount = 0;
            dmlvInventory.Parameters = collectionParametrs2;
            dmlvInventory.RemovingRowColor = Color.MistyRose;
            dmlvInventory.ShowDeleted = WinFormsComponents.Classes.Enums.ShowRemooving.ExecNotRemoving;
            dmlvInventory.Size = new Size(786, 391);
            dmlvInventory.TabIndex = 0;
            dmlvInventory.VisibleMode = WinFormsComponents.Classes.Enums.VisibleMode.Row;
            dmlvInventory.InsertChanged += dbmlvComplexOnInsertChanged;
            dmlvInventory.UpdateChanged += dbmlvComplexOnUpdateChanged;
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
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tcDBViewr);
            Controls.Add(tsMainMenu);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ПРОКАТ";
            tsMainMenu.ResumeLayout(false);
            tsMainMenu.PerformLayout();
            tcDBViewr.ResumeLayout(false);
            tpInventory.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip tsMainMenu;
        private ToolStripButton tsbSetings;
        private TabControl tcDBViewr;
        private ImageList ilTabMenu;
        private TabPage tpInventory;
        private WinFormsComponents.Controls.DBModelListView dmlvInventory;
        private ToolStripButton tsbCatalogs;
        private ToolStripButton tsbClients;
    }
}
