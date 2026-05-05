namespace RentalAccountingApp.Forms.CatalogsForm
{
    partial class ClientsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClientsForm));
            tcDBViewr = new TabControl();
            tabPage2 = new TabPage();
            dmlvClients = new WinFormsComponents.Controls.DBModelListView();
            ilClients = new ImageList(components);
            tcDBViewr.SuspendLayout();
            tabPage2.SuspendLayout();
            SuspendLayout();
            // 
            // tcDBViewr
            // 
            tcDBViewr.Controls.Add(tabPage2);
            tcDBViewr.Dock = DockStyle.Fill;
            tcDBViewr.ImageList = ilClients;
            tcDBViewr.Location = new Point(0, 0);
            tcDBViewr.Name = "tcDBViewr";
            tcDBViewr.SelectedIndex = 0;
            tcDBViewr.Size = new Size(800, 450);
            tcDBViewr.TabIndex = 0;
            tcDBViewr.KeyDown += tcDBViewrOnKeyDown;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(dmlvClients);
            tabPage2.ImageKey = "clients.png";
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(792, 422);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Клиенты";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // dmlvClients
            // 
            dmlvClients.Dock = DockStyle.Fill;
            dmlvClients.Enabled = false;
            dmlvClients.FilterOffColor = Color.MistyRose;
            dmlvClients.FilterOnColor = Color.LightGreen;
            dmlvClients.ImageList = ilClients;
            dmlvClients.IsEditor = false;
            dmlvClients.IsFilter = true;
            dmlvClients.IsGridLines = true;
            dmlvClients.IsRepairEditor = true;
            dmlvClients.IsRepairRow = true;
            dmlvClients.IsSearch = true;
            dmlvClients.IsShowCountAll = true;
            dmlvClients.IsShowCountEnter = true;
            dmlvClients.IsShowNum = false;
            dmlvClients.IsSorted = true;
            dmlvClients.Location = new Point(3, 3);
            dmlvClients.MinimumSize = new Size(600, 130);
            dmlvClients.ModelType = null;
            dmlvClients.Name = "dmlvClients";
            dmlvClients.PageLimit = 0;
            collectionParametrs1.Limit = 0;
            collectionParametrs1.Offset = 0;
            collectionParametrs1.SerhingParametrsCount = 0;
            dmlvClients.Parameters = collectionParametrs1;
            dmlvClients.RemovingRowColor = Color.MistyRose;
            dmlvClients.ShowDeleted = WinFormsComponents.Classes.Enums.ShowRemooving.ExecNotRemoving;
            dmlvClients.Size = new Size(786, 416);
            dmlvClients.TabIndex = 0;
            dmlvClients.VisibleMode = WinFormsComponents.Classes.Enums.VisibleMode.Row;
            dmlvClients.InsertChanged += dbmlvComplexOnInsertChanged;
            dmlvClients.UpdateChanged += dbmlvComplexOnUpdateChanged;
            // 
            // ilClients
            // 
            ilClients.ColorDepth = ColorDepth.Depth32Bit;
            ilClients.ImageStream = (ImageListStreamer)resources.GetObject("ilClients.ImageStream");
            ilClients.TransparentColor = Color.Transparent;
            ilClients.Images.SetKeyName(0, "clients.png");
            // 
            // ClientsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tcDBViewr);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "ClientsForm";
            Text = "Клиенты";
            tcDBViewr.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabControl tcDBViewr;
        private TabPage tabPage2;
        private WinFormsComponents.Controls.DBModelListView dmlvClients;
        private ImageList ilClients;
    }
}