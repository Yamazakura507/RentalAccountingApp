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
            ilTabMenu = new ImageList(components);
            tcDBViewr = new TabControl();
            tpRental = new TabPage();
            dmlvRental = new WinFormsComponents.Controls.DBModelListView();
            tpInventory = new TabPage();
            dmlvInventory = new WinFormsComponents.Controls.DBModelListView();
            tpOplat = new TabPage();
            dmlvOplat = new WinFormsComponents.Controls.DBModelListView();
            tcDBViewr.SuspendLayout();
            tpRental.SuspendLayout();
            tpInventory.SuspendLayout();
            tpOplat.SuspendLayout();
            SuspendLayout();
            // 
            // ilTabMenu
            // 
            ilTabMenu.ColorDepth = ColorDepth.Depth32Bit;
            ilTabMenu.ImageStream = (ImageListStreamer)resources.GetObject("ilTabMenu.ImageStream");
            ilTabMenu.TransparentColor = Color.Transparent;
            ilTabMenu.Images.SetKeyName(0, "inventory.png");
            ilTabMenu.Images.SetKeyName(1, "rent.png");
            ilTabMenu.Images.SetKeyName(2, "pay.png");
            // 
            // tcDBViewr
            // 
            tcDBViewr.Controls.Add(tpRental);
            tcDBViewr.Controls.Add(tpInventory);
            tcDBViewr.Controls.Add(tpOplat);
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
            dmlvRental.IsEditor = false;
            dmlvRental.IsFilter = true;
            dmlvRental.IsGridLines = true;
            dmlvRental.IsRemoveRow = true;
            dmlvRental.IsRepairEditor = true;
            dmlvRental.IsRepairRow = true;
            dmlvRental.IsSearch = true;
            dmlvRental.IsShowCountAll = true;
            dmlvRental.IsShowCountEnter = true;
            dmlvRental.IsShowNum = false;
            dmlvRental.IsSorted = true;
            dmlvRental.IsYieldMode = false;
            dmlvRental.Location = new Point(3, 3);
            dmlvRental.MinimumSize = new Size(600, 130);
            dmlvRental.ModelType = null;
            dmlvRental.MultiSelect = true;
            dmlvRental.Name = "dmlvRental";
            dmlvRental.NotSelect = false;
            dmlvRental.PageLimit = 50;
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
            dmlvInventory.IsEditor = false;
            dmlvInventory.IsFilter = true;
            dmlvInventory.IsGridLines = true;
            dmlvInventory.IsRemoveRow = true;
            dmlvInventory.IsRepairEditor = true;
            dmlvInventory.IsRepairRow = true;
            dmlvInventory.IsSearch = true;
            dmlvInventory.IsShowCountAll = true;
            dmlvInventory.IsShowCountEnter = true;
            dmlvInventory.IsShowNum = false;
            dmlvInventory.IsSorted = true;
            dmlvInventory.IsYieldMode = false;
            dmlvInventory.Location = new Point(3, 3);
            dmlvInventory.MinimumSize = new Size(530, 130);
            dmlvInventory.ModelType = null;
            dmlvInventory.MultiSelect = true;
            dmlvInventory.Name = "dmlvInventory";
            dmlvInventory.NotSelect = false;
            dmlvInventory.PageLimit = 0;
            dmlvInventory.RemovingRowColor = Color.MistyRose;
            dmlvInventory.ShowDeleted = WinFormsComponents.Classes.Enums.ShowRemooving.ExecNotRemoving;
            dmlvInventory.Size = new Size(786, 416);
            dmlvInventory.TabIndex = 0;
            dmlvInventory.VisibleMode = WinFormsComponents.Classes.Enums.VisibleMode.Row;
            dmlvInventory.InsertChanged += dbmlvComplexOnInsertChanged;
            dmlvInventory.UpdateChanged += dbmlvComplexOnUpdateChanged;
            // 
            // tpOplat
            // 
            tpOplat.Controls.Add(dmlvOplat);
            tpOplat.ImageKey = "pay.png";
            tpOplat.Location = new Point(4, 24);
            tpOplat.Name = "tpOplat";
            tpOplat.Padding = new Padding(3);
            tpOplat.Size = new Size(792, 422);
            tpOplat.TabIndex = 4;
            tpOplat.Text = "Оплаты";
            tpOplat.UseVisualStyleBackColor = true;
            // 
            // dmlvOplat
            // 
            dmlvOplat.Dock = DockStyle.Fill;
            dmlvOplat.Enabled = false;
            dmlvOplat.FilterOffColor = Color.MistyRose;
            dmlvOplat.FilterOnColor = Color.LightGreen;
            dmlvOplat.IsEditor = false;
            dmlvOplat.IsFilter = true;
            dmlvOplat.IsGridLines = true;
            dmlvOplat.IsRemoveRow = true;
            dmlvOplat.IsRepairEditor = false;
            dmlvOplat.IsRepairRow = false;
            dmlvOplat.IsSearch = true;
            dmlvOplat.IsShowCountAll = true;
            dmlvOplat.IsShowCountEnter = true;
            dmlvOplat.IsShowNum = false;
            dmlvOplat.IsSorted = true;
            dmlvOplat.IsYieldMode = false;
            dmlvOplat.Location = new Point(3, 3);
            dmlvOplat.MinimumSize = new Size(600, 130);
            dmlvOplat.ModelType = null;
            dmlvOplat.MultiSelect = true;
            dmlvOplat.Name = "dmlvOplat";
            dmlvOplat.NotSelect = false;
            dmlvOplat.PageLimit = 0;
            dmlvOplat.RemovingRowColor = Color.MistyRose;
            dmlvOplat.ShowDeleted = WinFormsComponents.Classes.Enums.ShowRemooving.ExecNotRemoving;
            dmlvOplat.Size = new Size(786, 416);
            dmlvOplat.TabIndex = 0;
            dmlvOplat.VisibleMode = WinFormsComponents.Classes.Enums.VisibleMode.Row;
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
            tpOplat.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private ImageList ilTabMenu;
        private TabControl tcDBViewr;
        private TabPage tpInventory;
        private WinFormsComponents.Controls.DBModelListView dmlvInventory;
        private TabPage tpRental;
        private WinFormsComponents.Controls.DBModelListView dmlvRental;
        private TabPage tpOplat;
        private WinFormsComponents.Controls.DBModelListView dmlvOplat;
    }
}