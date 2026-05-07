namespace RentalAccountingApp.Forms.CatalogsForm
{
    partial class Journal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Journal));
            DataBaseProvaider.Objects.CollectionParametrs collectionParametrs1 = new DataBaseProvaider.Objects.CollectionParametrs();
            DataBaseProvaider.Objects.CollectionParametrs collectionParametrs2 = new DataBaseProvaider.Objects.CollectionParametrs();
            ilTabMenu = new ImageList(components);
            tcDBViewr = new TabControl();
            tpRental = new TabPage();
            dmlvRental = new WinFormsComponents.Controls.DBModelListView();
            tpInventory = new TabPage();
            dmlvInventory = new WinFormsComponents.Controls.DBModelListView();
            tcDBViewr.SuspendLayout();
            tpRental.SuspendLayout();
            tpInventory.SuspendLayout();
            SuspendLayout();
            // 
            // ilTabMenu
            // 
            ilTabMenu.ColorDepth = ColorDepth.Depth32Bit;
            ilTabMenu.ImageStream = (ImageListStreamer)resources.GetObject("ilTabMenu.ImageStream");
            ilTabMenu.TransparentColor = Color.Transparent;
            ilTabMenu.Images.SetKeyName(0, "inventory.png");
            ilTabMenu.Images.SetKeyName(1, "rent.png");
            // 
            // tcDBViewr
            // 
            tcDBViewr.Controls.Add(tpRental);
            tcDBViewr.Controls.Add(tpInventory);
            tcDBViewr.Dock = DockStyle.Fill;
            tcDBViewr.ImageList = ilTabMenu;
            tcDBViewr.Location = new Point(0, 0);
            tcDBViewr.Name = "tcDBViewr";
            tcDBViewr.SelectedIndex = 0;
            tcDBViewr.Size = new Size(800, 450);
            tcDBViewr.TabIndex = 2;
            tcDBViewr.KeyDown += tcDBViewrOnKeyDown;
            // 
            // tpRental
            // 
            tpRental.Controls.Add(dmlvRental);
            tpRental.ImageKey = "rent.png";
            tpRental.Location = new Point(4, 24);
            tpRental.Name = "tpRental";
            tpRental.Padding = new Padding(3);
            tpRental.Size = new Size(792, 422);
            tpRental.TabIndex = 3;
            tpRental.Text = "Арендные заявки";
            tpRental.UseVisualStyleBackColor = true;
            // 
            // dmlvRental
            // 
            dmlvRental.Dock = DockStyle.Fill;
            dmlvRental.Enabled = false;
            dmlvRental.FilterOffColor = Color.MistyRose;
            dmlvRental.FilterOnColor = Color.LightGreen;
            dmlvRental.ImageList = ilTabMenu;
            dmlvRental.IsEditor = false;
            dmlvRental.IsFilter = true;
            dmlvRental.IsGridLines = true;
            dmlvRental.IsRepairEditor = true;
            dmlvRental.IsRepairRow = true;
            dmlvRental.IsSearch = true;
            dmlvRental.IsShowCountAll = true;
            dmlvRental.IsShowCountEnter = true;
            dmlvRental.IsShowNum = false;
            dmlvRental.IsSorted = true;
            dmlvRental.Location = new Point(3, 3);
            dmlvRental.MinimumSize = new Size(600, 130);
            dmlvRental.ModelType = null;
            dmlvRental.Name = "dmlvRental";
            dmlvRental.PageLimit = 0;
            collectionParametrs1.Limit = 0;
            collectionParametrs1.Offset = 0;
            collectionParametrs1.SerhingParametrsCount = 0;
            dmlvRental.Parameters = collectionParametrs1;
            dmlvRental.RemovingRowColor = Color.MistyRose;
            dmlvRental.ShowDeleted = WinFormsComponents.Classes.Enums.ShowRemooving.ExecNotRemoving;
            dmlvRental.Size = new Size(786, 416);
            dmlvRental.TabIndex = 0;
            dmlvRental.VisibleMode = WinFormsComponents.Classes.Enums.VisibleMode.Row;
            dmlvRental.InsertChanged += dbmlvComplexOnInsertChanged;
            dmlvRental.UpdateChanged += dbmlvComplexOnUpdateChanged;
            // 
            // tpInventory
            // 
            tpInventory.Controls.Add(dmlvInventory);
            tpInventory.ImageKey = "inventory.png";
            tpInventory.Location = new Point(4, 24);
            tpInventory.Name = "tpInventory";
            tpInventory.Padding = new Padding(3);
            tpInventory.Size = new Size(792, 422);
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
            dmlvInventory.Size = new Size(786, 416);
            dmlvInventory.TabIndex = 0;
            dmlvInventory.VisibleMode = WinFormsComponents.Classes.Enums.VisibleMode.Row;
            dmlvInventory.InsertChanged += dbmlvComplexOnInsertChanged;
            dmlvInventory.UpdateChanged += dbmlvComplexOnUpdateChanged;
            // 
            // Journal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tcDBViewr);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Journal";
            Text = "Журналы аренды";
            tcDBViewr.ResumeLayout(false);
            tpRental.ResumeLayout(false);
            tpInventory.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private ImageList ilTabMenu;
        private TabControl tcDBViewr;
        private TabPage tpInventory;
        private WinFormsComponents.Controls.DBModelListView dmlvInventory;
        private TabPage tpRental;
        private WinFormsComponents.Controls.DBModelListView dmlvRental;
    }
}