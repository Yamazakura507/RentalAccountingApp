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
            tsMainMenu = new ToolStrip();
            tsbSetings = new ToolStripButton();
            tsbCatalogs = new ToolStripButton();
            tsbClients = new ToolStripButton();
            tsbJournal = new ToolStripButton();
            ilTabMenu = new ImageList(components);
            tsMainMenu.SuspendLayout();
            SuspendLayout();
            // 
            // tsMainMenu
            // 
            tsMainMenu.Items.AddRange(new ToolStripItem[] { tsbSetings, tsbCatalogs, tsbClients, tsbJournal });
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
            ilTabMenu.Images.SetKeyName(0, "inventory.png");
            ilTabMenu.Images.SetKeyName(1, "rent.png");
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tsMainMenu);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ПРОКАТ";
            KeyDown += MainFormOnKeyDown;
            tsMainMenu.ResumeLayout(false);
            tsMainMenu.PerformLayout();
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
    }
}
