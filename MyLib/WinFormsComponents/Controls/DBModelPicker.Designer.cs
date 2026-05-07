namespace WinFormsComponents.Controls
{
    partial class DBModelPicker
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
            cbDBModel = new ComboBox();
            tlpDBModelView = new TableLayoutPanel();
            btNullVal = new Button();
            pbIcon = new PictureBox();
            tlpDBModelView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbIcon).BeginInit();
            SuspendLayout();
            // 
            // cbDBModel
            // 
            cbDBModel.Dock = DockStyle.Fill;
            cbDBModel.DropDownStyle = ComboBoxStyle.DropDownList;
            cbDBModel.FormattingEnabled = true;
            cbDBModel.Location = new Point(27, 3);
            cbDBModel.Name = "cbDBModel";
            cbDBModel.Size = new Size(359, 23);
            cbDBModel.TabIndex = 0;
            cbDBModel.SelectedIndexChanged += cbDBModelOnSelectedIndexChanged;
            // 
            // tlpDBModelView
            // 
            tlpDBModelView.BackColor = Color.Transparent;
            tlpDBModelView.ColumnCount = 3;
            tlpDBModelView.ColumnStyles.Add(new ColumnStyle());
            tlpDBModelView.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpDBModelView.ColumnStyles.Add(new ColumnStyle());
            tlpDBModelView.Controls.Add(btNullVal, 2, 0);
            tlpDBModelView.Controls.Add(cbDBModel, 1, 0);
            tlpDBModelView.Controls.Add(pbIcon, 0, 0);
            tlpDBModelView.Dock = DockStyle.Fill;
            tlpDBModelView.Location = new Point(0, 0);
            tlpDBModelView.Name = "tlpDBModelView";
            tlpDBModelView.RowCount = 1;
            tlpDBModelView.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpDBModelView.Size = new Size(419, 30);
            tlpDBModelView.TabIndex = 1;
            tlpDBModelView.Tag = "";
            // 
            // btNullVal
            // 
            btNullVal.BackColor = Color.Transparent;
            btNullVal.BackgroundImage = Properties.Resources.checkible;
            btNullVal.BackgroundImageLayout = ImageLayout.Zoom;
            btNullVal.Dock = DockStyle.Fill;
            btNullVal.FlatAppearance.BorderSize = 0;
            btNullVal.FlatStyle = FlatStyle.Popup;
            btNullVal.Location = new Point(392, 3);
            btNullVal.Name = "btNullVal";
            btNullVal.Size = new Size(24, 24);
            btNullVal.TabIndex = 1;
            btNullVal.Tag = "0";
            btNullVal.UseVisualStyleBackColor = false;
            btNullVal.Click += btNullValOnClick;
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
            // DBModelPicker
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Transparent;
            Controls.Add(tlpDBModelView);
            Name = "DBModelPicker";
            Size = new Size(419, 30);
            Load += DBModelPickerOnLoad;
            tlpDBModelView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pbIcon).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private ComboBox cbDBModel;
        private TableLayoutPanel tlpDBModelView;
        private Button btNullVal;
        private PictureBox pbIcon;
    }
}
