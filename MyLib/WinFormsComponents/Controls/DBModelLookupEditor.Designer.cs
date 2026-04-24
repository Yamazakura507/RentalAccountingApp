namespace WinFormsComponents.Controls
{
    partial class DBModelLookupEditor
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
            toolStrip1 = new ToolStrip();
            tsbSave = new ToolStripButton();
            tsbInsert = new ToolStripButton();
            tsbRemove = new ToolStripButton();
            tsbRepair = new ToolStripButton();
            tlp = new TableLayoutPanel();
            lbNameParametr = new Label();
            tbValueParametr = new TextBox();
            toolStrip1.SuspendLayout();
            tlp.SuspendLayout();
            SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.Items.AddRange(new ToolStripItem[] { tsbSave, tsbInsert, tsbRemove, tsbRepair });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(475, 25);
            toolStrip1.TabIndex = 0;
            toolStrip1.Text = "toolStrip1";
            // 
            // tsbSave
            // 
            tsbSave.Image = Properties.Resources.save;
            tsbSave.ImageTransparentColor = Color.Magenta;
            tsbSave.Name = "tsbSave";
            tsbSave.Size = new Size(86, 22);
            tsbSave.Text = "Сохранить";
            tsbSave.ToolTipText = "Сохранить(Ctrl+S)";
            tsbSave.Visible = false;
            tsbSave.Click += tsbSaveOnClick;
            // 
            // tsbInsert
            // 
            tsbInsert.Image = Properties.Resources.add;
            tsbInsert.ImageTransparentColor = Color.Magenta;
            tsbInsert.Name = "tsbInsert";
            tsbInsert.Size = new Size(79, 22);
            tsbInsert.Text = "Добавить";
            tsbInsert.ToolTipText = "Добавить(Insert)";
            tsbInsert.Visible = false;
            tsbInsert.Click += tsbAddOnClick;
            // 
            // tsbRemove
            // 
            tsbRemove.Image = Properties.Resources.delete;
            tsbRemove.ImageTransparentColor = Color.Magenta;
            tsbRemove.Name = "tsbRemove";
            tsbRemove.Size = new Size(71, 22);
            tsbRemove.Text = "Удалить";
            tsbRemove.ToolTipText = "Удалить(Delete)";
            tsbRemove.Visible = false;
            tsbRemove.Click += tsbRemove_Click;
            // 
            // tsbRepair
            // 
            tsbRepair.Image = Properties.Resources.undelete;
            tsbRepair.ImageTransparentColor = Color.Magenta;
            tsbRepair.Name = "tsbRepair";
            tsbRepair.Size = new Size(96, 22);
            tsbRepair.Text = "Востановить";
            tsbRepair.ToolTipText = "Востановить(Ctrl+R)";
            tsbRepair.Visible = false;
            tsbRepair.Click += tsbRepair_Click;
            // 
            // tlp
            // 
            tlp.ColumnCount = 2;
            tlp.ColumnStyles.Add(new ColumnStyle());
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlp.Controls.Add(lbNameParametr, 0, 0);
            tlp.Controls.Add(tbValueParametr, 1, 0);
            tlp.Dock = DockStyle.Fill;
            tlp.Location = new Point(0, 25);
            tlp.Name = "tlp";
            tlp.RowCount = 1;
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlp.Size = new Size(475, 50);
            tlp.TabIndex = 1;
            // 
            // lbNameParametr
            // 
            lbNameParametr.AutoSize = true;
            lbNameParametr.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            lbNameParametr.Location = new Point(3, 10);
            lbNameParametr.Margin = new Padding(3, 10, 3, 0);
            lbNameParametr.Name = "lbNameParametr";
            lbNameParametr.Size = new Size(146, 21);
            lbNameParametr.TabIndex = 0;
            lbNameParametr.Text = "lbNameParametr:";
            // 
            // tbValueParametr
            // 
            tbValueParametr.Dock = DockStyle.Fill;
            tbValueParametr.Location = new Point(155, 10);
            tbValueParametr.Margin = new Padding(3, 10, 3, 3);
            tbValueParametr.Name = "tbValueParametr";
            tbValueParametr.Size = new Size(317, 23);
            tbValueParametr.TabIndex = 1;
            tbValueParametr.KeyDown += DBModelLookupEditorOnKeyDown;
            // 
            // DBModelLookupEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tlp);
            Controls.Add(toolStrip1);
            MaximumSize = new Size(0, 75);
            MinimumSize = new Size(475, 75);
            Name = "DBModelLookupEditor";
            Size = new Size(475, 75);
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            tlp.ResumeLayout(false);
            tlp.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip toolStrip1;
        private ToolStripButton tsbSave;
        private ToolStripButton tsbInsert;
        private ToolStripButton tsbRemove;
        private ToolStripButton tsbRepair;
        private TableLayoutPanel tlp;
        private Label lbNameParametr;
        public TextBox tbValueParametr;
    }
}
