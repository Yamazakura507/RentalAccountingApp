namespace WinFormsComponents
{
    partial class ConnectingSettingsControl
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConnectingSettingsControl));
            lvConnections = new ListView();
            cmsConectionList = new ContextMenuStrip(components);
            tsmiAddConection = new ToolStripMenuItem();
            tsmiDeleteConection = new ToolStripMenuItem();
            tsmiConnect = new ToolStripMenuItem();
            ilSettings = new ImageList(components);
            tsMenu = new ToolStrip();
            tsbSave = new ToolStripButton();
            tsbAdd = new ToolStripButton();
            tsbDelete = new ToolStripButton();
            tsbConnect = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            tspbProgress = new ToolStripProgressBar();
            tslProgress = new ToolStripLabel();
            btLockPassword = new Button();
            tbPassword = new TextBox();
            tbLogin = new TextBox();
            tbDataBase = new TextBox();
            tbPort = new TextBox();
            tbHost = new TextBox();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            cmsConectionList.SuspendLayout();
            tsMenu.SuspendLayout();
            SuspendLayout();
            // 
            // lvConnections
            // 
            lvConnections.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lvConnections.ContextMenuStrip = cmsConectionList;
            lvConnections.FullRowSelect = true;
            lvConnections.Location = new Point(789, 28);
            lvConnections.Name = "lvConnections";
            lvConnections.Size = new Size(174, 145);
            lvConnections.SmallImageList = ilSettings;
            lvConnections.TabIndex = 25;
            lvConnections.UseCompatibleStateImageBehavior = false;
            lvConnections.View = View.List;
            lvConnections.SelectedIndexChanged += lvConnectionsOnSelectedIndexChanged;
            lvConnections.KeyDown += lvConnectionsOnKeyDown;
            // 
            // cmsConectionList
            // 
            cmsConectionList.Items.AddRange(new ToolStripItem[] { tsmiAddConection, tsmiDeleteConection, tsmiConnect });
            cmsConectionList.Name = "cmsConectionList";
            cmsConectionList.Size = new Size(180, 70);
            cmsConectionList.Opening += cmsConectionListOnOpening;
            // 
            // tsmiAddConection
            // 
            tsmiAddConection.Image = Properties.Resources.add;
            tsmiAddConection.Name = "tsmiAddConection";
            tsmiAddConection.Size = new Size(179, 22);
            tsmiAddConection.Text = "Добавить(Insert)";
            tsmiAddConection.Click += tsmiAddConectionOnClick;
            // 
            // tsmiDeleteConection
            // 
            tsmiDeleteConection.Image = Properties.Resources.delete;
            tsmiDeleteConection.Name = "tsmiDeleteConection";
            tsmiDeleteConection.Size = new Size(179, 22);
            tsmiDeleteConection.Text = "Удалить(Delete)";
            tsmiDeleteConection.Click += tsmiDeleteConectionOnClick;
            // 
            // tsmiConnect
            // 
            tsmiConnect.Image = Properties.Resources.active_connect;
            tsmiConnect.Name = "tsmiConnect";
            tsmiConnect.Size = new Size(179, 22);
            tsmiConnect.Text = "Подключить(Enter)";
            tsmiConnect.Click += tsbConnectOnClick;
            // 
            // ilSettings
            // 
            ilSettings.ColorDepth = ColorDepth.Depth32Bit;
            ilSettings.ImageStream = (ImageListStreamer)resources.GetObject("ilSettings.ImageStream");
            ilSettings.TransparentColor = Color.Transparent;
            ilSettings.Images.SetKeyName(0, "active_connect.png");
            ilSettings.Images.SetKeyName(1, "connect.png");
            ilSettings.Images.SetKeyName(2, "default_connect.png");
            ilSettings.Images.SetKeyName(3, "disconnect.png");
            // 
            // tsMenu
            // 
            tsMenu.Items.AddRange(new ToolStripItem[] { tsbSave, tsbAdd, tsbDelete, tsbConnect, toolStripSeparator1, tspbProgress, tslProgress });
            tsMenu.Location = new Point(0, 0);
            tsMenu.Name = "tsMenu";
            tsMenu.Size = new Size(966, 25);
            tsMenu.TabIndex = 24;
            tsMenu.Text = "toolStrip1";
            // 
            // tsbSave
            // 
            tsbSave.Image = Properties.Resources.save;
            tsbSave.ImageTransparentColor = Color.Magenta;
            tsbSave.Name = "tsbSave";
            tsbSave.Size = new Size(86, 22);
            tsbSave.Text = "Сохранить";
            tsbSave.ToolTipText = "Сохранить(Ctrl+S)";
            tsbSave.Click += tsbSaveOnClick;
            // 
            // tsbAdd
            // 
            tsbAdd.Image = Properties.Resources.add;
            tsbAdd.ImageTransparentColor = Color.Magenta;
            tsbAdd.Name = "tsbAdd";
            tsbAdd.Size = new Size(79, 22);
            tsbAdd.Text = "Добавить";
            tsbAdd.ToolTipText = "Добавить(Insert)";
            tsbAdd.Click += tsmiAddConectionOnClick;
            // 
            // tsbDelete
            // 
            tsbDelete.Enabled = false;
            tsbDelete.Image = Properties.Resources.delete;
            tsbDelete.ImageTransparentColor = Color.Magenta;
            tsbDelete.Name = "tsbDelete";
            tsbDelete.Size = new Size(71, 22);
            tsbDelete.Text = "Удалить";
            tsbDelete.ToolTipText = "Удалить(Delete)";
            tsbDelete.Click += tsmiDeleteConectionOnClick;
            // 
            // tsbConnect
            // 
            tsbConnect.Image = Properties.Resources.active_connect;
            tsbConnect.ImageTransparentColor = Color.Magenta;
            tsbConnect.Name = "tsbConnect";
            tsbConnect.Size = new Size(97, 22);
            tsbConnect.Text = "Подключить";
            tsbConnect.ToolTipText = "Подключить(Enter)";
            tsbConnect.Click += tsbConnectOnClick;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 25);
            // 
            // tspbProgress
            // 
            tspbProgress.MarqueeAnimationSpeed = 30;
            tspbProgress.Name = "tspbProgress";
            tspbProgress.Size = new Size(100, 22);
            tspbProgress.Style = ProgressBarStyle.Marquee;
            tspbProgress.Visible = false;
            tspbProgress.VisibleChanged += tspbProgressOnVisibleChanged;
            // 
            // tslProgress
            // 
            tslProgress.Name = "tslProgress";
            tslProgress.Size = new Size(64, 22);
            tslProgress.Text = "tslProgress";
            tslProgress.Visible = false;
            // 
            // btLockPassword
            // 
            btLockPassword.BackgroundImage = Properties.Resources.lock_text;
            btLockPassword.BackgroundImageLayout = ImageLayout.Zoom;
            btLockPassword.FlatAppearance.BorderSize = 0;
            btLockPassword.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btLockPassword.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btLockPassword.FlatStyle = FlatStyle.Flat;
            btLockPassword.Location = new Point(749, 147);
            btLockPassword.Name = "btLockPassword";
            btLockPassword.Size = new Size(34, 27);
            btLockPassword.TabIndex = 23;
            btLockPassword.UseVisualStyleBackColor = true;
            btLockPassword.EnabledChanged += btLockPasswordOnEnabledChanged;
            btLockPassword.Click += btLockPasswordOnClick;
            btLockPassword.KeyDown += lvConnectionsOnKeyDown;
            // 
            // tbPassword
            // 
            tbPassword.Font = new Font("Segoe UI", 12F);
            tbPassword.Location = new Point(168, 144);
            tbPassword.Name = "tbPassword";
            tbPassword.PasswordChar = '*';
            tbPassword.Size = new Size(575, 29);
            tbPassword.TabIndex = 22;
            tbPassword.KeyDown += lvConnectionsOnKeyDown;
            // 
            // tbLogin
            // 
            tbLogin.Font = new Font("Segoe UI", 12F);
            tbLogin.Location = new Point(168, 115);
            tbLogin.Name = "tbLogin";
            tbLogin.Size = new Size(615, 29);
            tbLogin.TabIndex = 21;
            tbLogin.KeyDown += lvConnectionsOnKeyDown;
            // 
            // tbDataBase
            // 
            tbDataBase.Font = new Font("Segoe UI", 12F);
            tbDataBase.Location = new Point(168, 86);
            tbDataBase.Name = "tbDataBase";
            tbDataBase.Size = new Size(615, 29);
            tbDataBase.TabIndex = 20;
            tbDataBase.KeyDown += lvConnectionsOnKeyDown;
            // 
            // tbPort
            // 
            tbPort.Font = new Font("Segoe UI", 12F);
            tbPort.Location = new Point(168, 57);
            tbPort.Name = "tbPort";
            tbPort.ShortcutsEnabled = false;
            tbPort.Size = new Size(615, 29);
            tbPort.TabIndex = 19;
            tbPort.TextChanged += tbPortOnTextChanged;
            tbPort.KeyDown += lvConnectionsOnKeyDown;
            tbPort.KeyPress += tbPortOnKeyPress;
            // 
            // tbHost
            // 
            tbHost.Font = new Font("Segoe UI", 12F);
            tbHost.Location = new Point(168, 28);
            tbHost.Name = "tbHost";
            tbHost.Size = new Size(615, 29);
            tbHost.TabIndex = 18;
            tbHost.TextChanged += tbHostOnTextChanged;
            tbHost.KeyDown += lvConnectionsOnKeyDown;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label5.Location = new Point(5, 89);
            label5.Name = "label5";
            label5.Size = new Size(157, 21);
            label5.TabIndex = 17;
            label5.Text = "Имя базы данных:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label4.Location = new Point(88, 147);
            label4.Name = "label4";
            label4.Size = new Size(74, 21);
            label4.TabIndex = 16;
            label4.Text = "Пароль:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label3.Location = new Point(99, 118);
            label3.Name = "label3";
            label3.Size = new Size(63, 21);
            label3.TabIndex = 15;
            label3.Text = "Логин:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label2.Location = new Point(109, 60);
            label2.Name = "label2";
            label2.Size = new Size(53, 21);
            label2.TabIndex = 14;
            label2.Text = "Порт:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label1.Location = new Point(113, 31);
            label1.Name = "label1";
            label1.Size = new Size(49, 21);
            label1.TabIndex = 13;
            label1.Text = "Хост:";
            // 
            // ConnectingSettingsControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Transparent;
            Controls.Add(lvConnections);
            Controls.Add(tsMenu);
            Controls.Add(btLockPassword);
            Controls.Add(tbPassword);
            Controls.Add(tbLogin);
            Controls.Add(tbDataBase);
            Controls.Add(tbPort);
            Controls.Add(tbHost);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "ConnectingSettingsControl";
            Size = new Size(966, 183);
            Load += SettingsConnectControlOnLoad;
            cmsConectionList.ResumeLayout(false);
            tsMenu.ResumeLayout(false);
            tsMenu.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListView lvConnections;
        private ContextMenuStrip cmsConectionList;
        private ToolStripMenuItem tsmiAddConection;
        private ToolStripMenuItem tsmiDeleteConection;
        private ToolStripMenuItem tsmiConnect;
        private ImageList ilSettings;
        private ToolStrip tsMenu;
        private ToolStripButton tsbSave;
        private ToolStripButton tsbAdd;
        private ToolStripButton tsbDelete;
        private ToolStripButton tsbConnect;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripProgressBar tspbProgress;
        private ToolStripLabel tslProgress;
        private Button btLockPassword;
        private TextBox tbPassword;
        private TextBox tbLogin;
        private TextBox tbDataBase;
        private TextBox tbPort;
        private TextBox tbHost;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
    }
}
