namespace WinFormsComponents.Controls
{
    partial class DBModelSelectedNewObject
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            tlpDBModelView = new TableLayoutPanel();
            btDelete = new Button();
            btAdd = new Button();
            pbIcon = new PictureBox();
            lbSelectedName = new Label();
            tlpDBModelView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbIcon).BeginInit();
            SuspendLayout();
            // 
            // tlpDBModelView
            // 
            tlpDBModelView.BackColor = Color.Transparent;
            tlpDBModelView.ColumnCount = 4;
            tlpDBModelView.ColumnStyles.Add(new ColumnStyle());
            tlpDBModelView.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpDBModelView.ColumnStyles.Add(new ColumnStyle());
            tlpDBModelView.ColumnStyles.Add(new ColumnStyle());
            tlpDBModelView.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tlpDBModelView.Controls.Add(btDelete, 3, 0);
            tlpDBModelView.Controls.Add(btAdd, 2, 0);
            tlpDBModelView.Controls.Add(pbIcon, 0, 0);
            tlpDBModelView.Controls.Add(lbSelectedName, 1, 0);
            tlpDBModelView.Dock = DockStyle.Fill;
            tlpDBModelView.Location = new Point(0, 0);
            tlpDBModelView.Name = "tlpDBModelView";
            tlpDBModelView.RowCount = 1;
            tlpDBModelView.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpDBModelView.Size = new Size(419, 30);
            tlpDBModelView.TabIndex = 1;
            tlpDBModelView.Tag = "";
            // 
            // btDelete
            // 
            btDelete.BackColor = Color.Transparent;
            btDelete.BackgroundImage = Properties.Resources.delete;
            btDelete.BackgroundImageLayout = ImageLayout.Zoom;
            btDelete.Dock = DockStyle.Fill;
            btDelete.FlatAppearance.BorderSize = 0;
            btDelete.FlatStyle = FlatStyle.Flat;
            btDelete.Location = new Point(392, 3);
            btDelete.Name = "btDelete";
            btDelete.Size = new Size(24, 24);
            btDelete.TabIndex = 5;
            btDelete.Tag = "0";
            btDelete.UseVisualStyleBackColor = false;
            btDelete.Click += btDeleteOnClick;
            // 
            // btAdd
            // 
            btAdd.BackColor = Color.Transparent;
            btAdd.BackgroundImage = Properties.Resources.add;
            btAdd.BackgroundImageLayout = ImageLayout.Zoom;
            btAdd.Dock = DockStyle.Fill;
            btAdd.FlatAppearance.BorderSize = 0;
            btAdd.FlatStyle = FlatStyle.Flat;
            btAdd.Location = new Point(362, 3);
            btAdd.Name = "btAdd";
            btAdd.Size = new Size(24, 24);
            btAdd.TabIndex = 4;
            btAdd.Tag = "0";
            btAdd.UseVisualStyleBackColor = false;
            btAdd.Click += btInsertOnClick;
            // 
            // pbIcon
            // 
            pbIcon.BackColor = Color.Transparent;
            pbIcon.BackgroundImageLayout = ImageLayout.Zoom;
            pbIcon.Dock = DockStyle.Fill;
            pbIcon.ErrorImage = null;
            pbIcon.InitialImage = null;
            pbIcon.Location = new Point(0, 0);
            pbIcon.Margin = new Padding(0);
            pbIcon.Name = "pbIcon";
            pbIcon.Size = new Size(24, 30);
            pbIcon.SizeMode = PictureBoxSizeMode.CenterImage;
            pbIcon.TabIndex = 2;
            pbIcon.TabStop = false;
            // 
            // lbSelectedName
            // 
            lbSelectedName.AutoSize = true;
            lbSelectedName.Dock = DockStyle.Fill;
            lbSelectedName.Font = new Font("Segoe UI", 14F, FontStyle.Bold | FontStyle.Underline);
            lbSelectedName.Location = new Point(27, 0);
            lbSelectedName.Name = "lbSelectedName";
            lbSelectedName.Size = new Size(329, 30);
            lbSelectedName.TabIndex = 3;
            lbSelectedName.Text = "SelectedName";
            lbSelectedName.TextAlign = ContentAlignment.MiddleLeft;
            lbSelectedName.Click += btInsertOnClick;
            // 
            // DBModelSelectedNewObject
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Transparent;
            Controls.Add(tlpDBModelView);
            Name = "DBModelSelectedNewObject";
            Size = new Size(419, 30);
            Load += DBModelPickerOnLoad;
            tlpDBModelView.ResumeLayout(false);
            tlpDBModelView.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pbIcon).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private TableLayoutPanel tlpDBModelView;
        private PictureBox pbIcon;
        private Label lbSelectedName;
        private Button btAdd;
        private Button btDelete;
    }
}
