namespace RentalAccountingApp.Forms.EditForm
{
    partial class DBModelComplexEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DBModelComplexEditor));
            dmlceEditor = new WinFormsComponents.Controls.DBModelComplexEditor();
            ilDependencies = new ImageList(components);
            SuspendLayout();
            // 
            // dmlceEditor
            // 
            dmlceEditor.BaseCatologIcon = (Icon)resources.GetObject("dmlceEditor.BaseCatologIcon");
            dmlceEditor.Dock = DockStyle.Fill;
            dmlceEditor.EditorMode = WinFormsComponents.Classes.Enums.EditorMode.Insert;
            dmlceEditor.ImageList = ilDependencies;
            dmlceEditor.Location = new Point(0, 0);
            dmlceEditor.MinimumSize = new Size(475, 75);
            dmlceEditor.Name = "dmlceEditor";
            dmlceEditor.PKStr = "Id";
            dmlceEditor.Size = new Size(514, 81);
            dmlceEditor.TabIndex = 0;
            dmlceEditor.InsertChanged += dbmlEditorOnInsertChanged;
            dmlceEditor.UpdateChanged += dbmlEditorOnUpdateChanged;
            dmlceEditor.DeleteChanged += dbmlEditorOnDeleteOrRepairChanged;
            dmlceEditor.RepairChanged += dbmlEditorOnDeleteOrRepairChanged;
            // 
            // ilDependencies
            // 
            ilDependencies.ColorDepth = ColorDepth.Depth32Bit;
            ilDependencies.ImageStream = (ImageListStreamer)resources.GetObject("ilDependencies.ImageStream");
            ilDependencies.TransparentColor = Color.Transparent;
            ilDependencies.Images.SetKeyName(0, "category.png");
            ilDependencies.Images.SetKeyName(1, "materials.png");
            ilDependencies.Images.SetKeyName(2, "inventory.png");
            ilDependencies.Images.SetKeyName(3, "clients.png");
            ilDependencies.Images.SetKeyName(4, "pay.png");
            // 
            // DBModelComplexEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(514, 81);
            Controls.Add(dmlceEditor);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(530, 120);
            Name = "DBModelComplexEditor";
            StartPosition = FormStartPosition.CenterScreen;
            ResumeLayout(false);
        }

        #endregion

        private WinFormsComponents.Controls.DBModelComplexEditor dmlceEditor;
        private ImageList ilDependencies;
    }
}