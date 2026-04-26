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
            DataBaseProvaider.Objects.CollectionParametrs collectionParametrs1 = new DataBaseProvaider.Objects.CollectionParametrs();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            DataBaseProvaider.Objects.CollectionParametrs collectionParametrs3 = new DataBaseProvaider.Objects.CollectionParametrs();
            DataBaseProvaider.Objects.CollectionParametrs collectionParametrs2 = new DataBaseProvaider.Objects.CollectionParametrs();
            tsMainMenu = new ToolStrip();
            tsbSetings = new ToolStripButton();
            tpCategory = new TabPage();
            dmlvCategories = new WinFormsComponents.Controls.DBModelListView();
            ilTabMenu = new ImageList(components);
            tcDBViewr = new TabControl();
            tpMaterial = new TabPage();
            dbmlvMaterials = new WinFormsComponents.Controls.DBModelListView();
            tpInventory = new TabPage();
            dmlvInventory = new WinFormsComponents.Controls.DBModelListView();
            tsMainMenu.SuspendLayout();
            tpCategory.SuspendLayout();
            tcDBViewr.SuspendLayout();
            tpMaterial.SuspendLayout();
            tpInventory.SuspendLayout();
            SuspendLayout();
            // 
            // tsMainMenu
            // 
            tsMainMenu.Items.AddRange(new ToolStripItem[] { tsbSetings });
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
            // tpCategory
            // 
            tpCategory.Controls.Add(dmlvCategories);
            tpCategory.ImageKey = "category.png";
            tpCategory.Location = new Point(4, 24);
            tpCategory.Name = "tpCategory";
            tpCategory.Padding = new Padding(3);
            tpCategory.Size = new Size(792, 397);
            tpCategory.TabIndex = 1;
            tpCategory.Text = "Категории";
            tpCategory.UseVisualStyleBackColor = true;
            // 
            // dmlvCategories
            // 
            dmlvCategories.Dock = DockStyle.Fill;
            dmlvCategories.Enabled = false;
            dmlvCategories.FilterOffColor = Color.MistyRose;
            dmlvCategories.FilterOnColor = Color.LightGreen;
            dmlvCategories.ImageList = ilTabMenu;
            dmlvCategories.IsFilter = true;
            dmlvCategories.IsGridLines = true;
            dmlvCategories.IsSearch = true;
            dmlvCategories.IsShowCountAll = true;
            dmlvCategories.IsShowCountEnter = true;
            dmlvCategories.IsShowNum = false;
            dmlvCategories.IsSorted = true;
            dmlvCategories.Location = new Point(3, 3);
            dmlvCategories.MinimumSize = new Size(530, 130);
            dmlvCategories.ModelType = null;
            dmlvCategories.Name = "dmlvCategories";
            dmlvCategories.PageLimit = 0;
            collectionParametrs1.Limit = 0;
            collectionParametrs1.Offset = 0;
            collectionParametrs1.SerhingParametrsCount = 0;
            dmlvCategories.Parameters = collectionParametrs1;
            dmlvCategories.RemovingRowColor = Color.MistyRose;
            dmlvCategories.ShowDeleted = WinFormsComponents.Classes.Enums.ShowRemooving.ExecNotRemoving;
            dmlvCategories.Size = new Size(786, 391);
            dmlvCategories.TabIndex = 0;
            dmlvCategories.VisibleMode = WinFormsComponents.Classes.Enums.VisibleMode.Row;
            dmlvCategories.InsertChanged += dbmlvLookupOnInsertChanged;
            dmlvCategories.UpdateChanged += dbmlvLookupOnUpdateChanged;
            // 
            // ilTabMenu
            // 
            ilTabMenu.ColorDepth = ColorDepth.Depth32Bit;
            ilTabMenu.ImageStream = (ImageListStreamer)resources.GetObject("ilTabMenu.ImageStream");
            ilTabMenu.TransparentColor = Color.Transparent;
            ilTabMenu.Images.SetKeyName(0, "category.png");
            ilTabMenu.Images.SetKeyName(1, "inventory.png");
            ilTabMenu.Images.SetKeyName(2, "materials.png");
            // 
            // tcDBViewr
            // 
            tcDBViewr.Controls.Add(tpInventory);
            tcDBViewr.Controls.Add(tpMaterial);
            tcDBViewr.Controls.Add(tpCategory);
            tcDBViewr.Dock = DockStyle.Fill;
            tcDBViewr.ImageList = ilTabMenu;
            tcDBViewr.Location = new Point(0, 25);
            tcDBViewr.Name = "tcDBViewr";
            tcDBViewr.SelectedIndex = 0;
            tcDBViewr.Size = new Size(800, 425);
            tcDBViewr.TabIndex = 1;
            tcDBViewr.KeyDown += tcDBViewrOnKeyDown;
            // 
            // tpMaterial
            // 
            tpMaterial.Controls.Add(dbmlvMaterials);
            tpMaterial.ImageKey = "materials.png";
            tpMaterial.Location = new Point(4, 24);
            tpMaterial.Name = "tpMaterial";
            tpMaterial.Padding = new Padding(3);
            tpMaterial.Size = new Size(792, 397);
            tpMaterial.TabIndex = 0;
            tpMaterial.Text = "Матерьялы";
            tpMaterial.UseVisualStyleBackColor = true;
            // 
            // dbmlvMaterials
            // 
            dbmlvMaterials.Dock = DockStyle.Fill;
            dbmlvMaterials.Enabled = false;
            dbmlvMaterials.FilterOffColor = Color.MistyRose;
            dbmlvMaterials.FilterOnColor = Color.LightGreen;
            dbmlvMaterials.ImageList = ilTabMenu;
            dbmlvMaterials.IsFilter = true;
            dbmlvMaterials.IsGridLines = true;
            dbmlvMaterials.IsSearch = true;
            dbmlvMaterials.IsShowCountAll = true;
            dbmlvMaterials.IsShowCountEnter = true;
            dbmlvMaterials.IsShowNum = false;
            dbmlvMaterials.IsSorted = true;
            dbmlvMaterials.Location = new Point(3, 3);
            dbmlvMaterials.MinimumSize = new Size(530, 130);
            dbmlvMaterials.ModelType = null;
            dbmlvMaterials.Name = "dbmlvMaterials";
            dbmlvMaterials.PageLimit = 0;
            collectionParametrs3.Limit = 0;
            collectionParametrs3.Offset = 0;
            collectionParametrs3.SerhingParametrsCount = 0;
            dbmlvMaterials.Parameters = collectionParametrs3;
            dbmlvMaterials.RemovingRowColor = Color.MistyRose;
            dbmlvMaterials.ShowDeleted = WinFormsComponents.Classes.Enums.ShowRemooving.ExecNotRemoving;
            dbmlvMaterials.Size = new Size(786, 391);
            dbmlvMaterials.TabIndex = 0;
            dbmlvMaterials.VisibleMode = WinFormsComponents.Classes.Enums.VisibleMode.Row;
            dbmlvMaterials.InsertChanged += dbmlvLookupOnInsertChanged;
            dbmlvMaterials.UpdateChanged += dbmlvLookupOnUpdateChanged;
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
            dmlvInventory.IsFilter = true;
            dmlvInventory.IsGridLines = true;
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
            tpCategory.ResumeLayout(false);
            tcDBViewr.ResumeLayout(false);
            tpMaterial.ResumeLayout(false);
            tpInventory.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip tsMainMenu;
        private ToolStripButton tsbSetings;
        private TabPage tpCategory;
        private TabControl tcDBViewr;
        private TabPage tpMaterial;
        private ImageList ilTabMenu;
        private TabPage tpInventory;
        private WinFormsComponents.Controls.DBModelListView dbmlvMaterials;
        private WinFormsComponents.Controls.DBModelListView dmlvCategories;
        private WinFormsComponents.Controls.DBModelListView dmlvInventory;
    }
}
