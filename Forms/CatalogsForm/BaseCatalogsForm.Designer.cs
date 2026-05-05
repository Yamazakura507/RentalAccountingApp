namespace RentalAccountingApp.Forms.CatalogsForm
{
    partial class BaseCatalogsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            DataBaseProvaider.Objects.CollectionParametrs collectionParametrs1 = new DataBaseProvaider.Objects.CollectionParametrs();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BaseCatalogsForm));
            DataBaseProvaider.Objects.CollectionParametrs collectionParametrs2 = new DataBaseProvaider.Objects.CollectionParametrs();
            tpCategory = new TabPage();
            dmlvCategories = new WinFormsComponents.Controls.DBModelListView();
            ilTabMenu = new ImageList(components);
            tpMaterial = new TabPage();
            dbmlvMaterials = new WinFormsComponents.Controls.DBModelListView();
            tcDBViewr = new TabControl();
            tpCategory.SuspendLayout();
            tpMaterial.SuspendLayout();
            tcDBViewr.SuspendLayout();
            SuspendLayout();
            // 
            // tpCategory
            // 
            tpCategory.Controls.Add(dmlvCategories);
            tpCategory.ImageKey = "category.png";
            tpCategory.Location = new Point(4, 24);
            tpCategory.Name = "tpCategory";
            tpCategory.Padding = new Padding(3);
            tpCategory.Size = new Size(792, 422);
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
            dmlvCategories.IsEditor = false;
            dmlvCategories.IsFilter = true;
            dmlvCategories.IsGridLines = true;
            dmlvCategories.IsRepairEditor = true;
            dmlvCategories.IsRepairRow = true;
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
            dmlvCategories.Size = new Size(786, 416);
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
            ilTabMenu.Images.SetKeyName(1, "materials.png");
            // 
            // tpMaterial
            // 
            tpMaterial.Controls.Add(dbmlvMaterials);
            tpMaterial.ImageKey = "materials.png";
            tpMaterial.Location = new Point(4, 24);
            tpMaterial.Name = "tpMaterial";
            tpMaterial.Padding = new Padding(3);
            tpMaterial.Size = new Size(792, 422);
            tpMaterial.TabIndex = 0;
            tpMaterial.Text = "Материалы";
            tpMaterial.UseVisualStyleBackColor = true;
            // 
            // dbmlvMaterials
            // 
            dbmlvMaterials.Dock = DockStyle.Fill;
            dbmlvMaterials.Enabled = false;
            dbmlvMaterials.FilterOffColor = Color.MistyRose;
            dbmlvMaterials.FilterOnColor = Color.LightGreen;
            dbmlvMaterials.ImageList = ilTabMenu;
            dbmlvMaterials.IsEditor = false;
            dbmlvMaterials.IsFilter = true;
            dbmlvMaterials.IsGridLines = true;
            dbmlvMaterials.IsRepairEditor = true;
            dbmlvMaterials.IsRepairRow = true;
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
            collectionParametrs2.Limit = 0;
            collectionParametrs2.Offset = 0;
            collectionParametrs2.SerhingParametrsCount = 0;
            dbmlvMaterials.Parameters = collectionParametrs2;
            dbmlvMaterials.RemovingRowColor = Color.MistyRose;
            dbmlvMaterials.ShowDeleted = WinFormsComponents.Classes.Enums.ShowRemooving.ExecNotRemoving;
            dbmlvMaterials.Size = new Size(786, 416);
            dbmlvMaterials.TabIndex = 0;
            dbmlvMaterials.VisibleMode = WinFormsComponents.Classes.Enums.VisibleMode.Row;
            dbmlvMaterials.InsertChanged += dbmlvLookupOnInsertChanged;
            dbmlvMaterials.UpdateChanged += dbmlvLookupOnUpdateChanged;
            // 
            // tcDBViewr
            // 
            tcDBViewr.Controls.Add(tpMaterial);
            tcDBViewr.Controls.Add(tpCategory);
            tcDBViewr.Dock = DockStyle.Fill;
            tcDBViewr.ImageList = ilTabMenu;
            tcDBViewr.Location = new Point(0, 0);
            tcDBViewr.Name = "tcDBViewr";
            tcDBViewr.SelectedIndex = 0;
            tcDBViewr.Size = new Size(800, 450);
            tcDBViewr.TabIndex = 2;
            tcDBViewr.KeyDown += tcDBViewrOnKeyDown;
            // 
            // BaseCatalogsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tcDBViewr);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "BaseCatalogsForm";
            Text = "Справочники";
            tpCategory.ResumeLayout(false);
            tpMaterial.ResumeLayout(false);
            tcDBViewr.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabPage tpCategory;
        private WinFormsComponents.Controls.DBModelListView dmlvCategories;
        private TabPage tpMaterial;
        private WinFormsComponents.Controls.DBModelListView dbmlvMaterials;
        private ImageList ilTabMenu;
        private TabControl tcDBViewr;
    }
}