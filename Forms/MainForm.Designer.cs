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
            tsMainMenu = new ToolStrip();
            tsbSetings = new ToolStripButton();
            tpCategory = new TabPage();
            tcDBViewr = new TabControl();
            tpMaterial = new TabPage();
            dbmlvMaterials = new WinFormsComponents.Controls.DBModelListView();
            ilTabMenu = new ImageList(components);
            tpInventory = new TabPage();
            tsMainMenu.SuspendLayout();
            tcDBViewr.SuspendLayout();
            tpMaterial.SuspendLayout();
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
            tsbSetings.Click += tsbSetingsOnClick;
            // 
            // tpCategory
            // 
            tpCategory.ImageKey = "category.png";
            tpCategory.Location = new Point(4, 24);
            tpCategory.Name = "tpCategory";
            tpCategory.Padding = new Padding(3);
            tpCategory.Size = new Size(792, 397);
            tpCategory.TabIndex = 1;
            tpCategory.Text = "Категории";
            tpCategory.UseVisualStyleBackColor = true;
            // 
            // tcDBViewr
            // 
            tcDBViewr.Controls.Add(tpMaterial);
            tcDBViewr.Controls.Add(tpCategory);
            tcDBViewr.Controls.Add(tpInventory);
            tcDBViewr.Dock = DockStyle.Fill;
            tcDBViewr.ImageList = ilTabMenu;
            tcDBViewr.Location = new Point(0, 25);
            tcDBViewr.Name = "tcDBViewr";
            tcDBViewr.SelectedIndex = 0;
            tcDBViewr.Size = new Size(800, 425);
            tcDBViewr.TabIndex = 1;
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
            dbmlvMaterials.FilterOffColor = Color.MistyRose;
            dbmlvMaterials.FilterOnColor = Color.LightGreen;
            dbmlvMaterials.ImageList = ilTabMenu;
            dbmlvMaterials.Location = new Point(3, 3);
            dbmlvMaterials.ModelType = null;
            dbmlvMaterials.Name = "dbmlvMaterials";
            collectionParametrs1.Limit = 0;
            collectionParametrs1.Offset = 0;
            collectionParametrs1.SerhingParametrsCount = 0;
            dbmlvMaterials.Parameters = collectionParametrs1;
            dbmlvMaterials.RemovingRowColor = Color.MistyRose;
            dbmlvMaterials.Size = new Size(786, 391);
            dbmlvMaterials.TabIndex = 0;
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
            // tpInventory
            // 
            tpInventory.ImageKey = "inventory.png";
            tpInventory.Location = new Point(4, 24);
            tpInventory.Name = "tpInventory";
            tpInventory.Padding = new Padding(3);
            tpInventory.Size = new Size(792, 397);
            tpInventory.TabIndex = 2;
            tpInventory.Text = "Инвентарь";
            tpInventory.UseVisualStyleBackColor = true;
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
            Load += MainForm_Load;
            tsMainMenu.ResumeLayout(false);
            tsMainMenu.PerformLayout();
            tcDBViewr.ResumeLayout(false);
            tpMaterial.ResumeLayout(false);
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
    }
}
