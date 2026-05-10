namespace RentalAccountingApp.Forms
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            ilSettings = new ImageList(components);
            tpConectionSettings = new TabPage();
            connectingSettingsControl1 = new WinFormsComponents.ConnectingSettingsControl();
            tcSettings = new TabControl();
            tpConectionSettings.SuspendLayout();
            tcSettings.SuspendLayout();
            SuspendLayout();
            // 
            // ilSettings
            // 
            ilSettings.ColorDepth = ColorDepth.Depth32Bit;
            ilSettings.ImageStream = (ImageListStreamer)resources.GetObject("ilSettings.ImageStream");
            ilSettings.TransparentColor = Color.Transparent;
            ilSettings.Images.SetKeyName(0, "connect.png");
            ilSettings.Images.SetKeyName(1, "setings.png");
            // 
            // tpConectionSettings
            // 
            tpConectionSettings.Controls.Add(connectingSettingsControl1);
            tpConectionSettings.ImageKey = "connect.png";
            tpConectionSettings.Location = new Point(4, 24);
            tpConectionSettings.Name = "tpConectionSettings";
            tpConectionSettings.Padding = new Padding(3);
            tpConectionSettings.Size = new Size(976, 188);
            tpConectionSettings.TabIndex = 1;
            tpConectionSettings.Text = "Соеденения";
            tpConectionSettings.UseVisualStyleBackColor = true;
            // 
            // connectingSettingsControl1
            // 
            connectingSettingsControl1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            connectingSettingsControl1.BackColor = Color.Transparent;
            connectingSettingsControl1.Location = new Point(4, 3);
            connectingSettingsControl1.Name = "connectingSettingsControl1";
            connectingSettingsControl1.Size = new Size(966, 183);
            connectingSettingsControl1.TabIndex = 0;
            // 
            // tcSettings
            // 
            tcSettings.Controls.Add(tpConectionSettings);
            tcSettings.Dock = DockStyle.Fill;
            tcSettings.ImageList = ilSettings;
            tcSettings.Location = new Point(0, 0);
            tcSettings.Name = "tcSettings";
            tcSettings.SelectedIndex = 0;
            tcSettings.Size = new Size(984, 216);
            tcSettings.TabIndex = 0;
            // 
            // SettingsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(984, 216);
            Controls.Add(tcSettings);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(1000, 255);
            Name = "SettingsForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "НАСТРОЙКИ";
            Load += SettingsFormOnLoad;
            tpConectionSettings.ResumeLayout(false);
            tcSettings.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private ImageList ilSettings;
        private TabPage tpConectionSettings;
        private WinFormsComponents.ConnectingSettingsControl connectingSettingsControl1;
        private TabControl tcSettings;
    }
}