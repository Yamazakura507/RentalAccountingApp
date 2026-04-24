namespace RentalAccountingApp.Forms
{
    partial class DBModelLookupEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DBModelLookupEditor));
            dbmlEditor = new WinFormsComponents.Controls.DBModelLookupEditor();
            SuspendLayout();
            // 
            // dbmlEditor
            // 
            dbmlEditor.Dock = DockStyle.Fill;
            dbmlEditor.EditorMode = WinFormsComponents.Classes.Enums.EditorMode.Insert;
            dbmlEditor.Location = new Point(0, 0);
            dbmlEditor.MaximumSize = new Size(0, 75);
            dbmlEditor.MinimumSize = new Size(475, 75);
            dbmlEditor.Name = "dbmlEditor";
            dbmlEditor.ParametrTag = null;
            dbmlEditor.ParametrTitle = null;
            dbmlEditor.ParametrValue = null;
            dbmlEditor.Size = new Size(514, 75);
            dbmlEditor.TabIndex = 0;
            dbmlEditor.InsertChanged += dbmlEditorOnInsertChanged;
            dbmlEditor.UpdateChanged += dbmlEditorOnUpdateChanged;
            dbmlEditor.DeleteChanged += dbmlEditorOnDeleteOrRepairChanged;
            dbmlEditor.RepairChanged += dbmlEditorOnDeleteOrRepairChanged;
            // 
            // DBModelLookupEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(514, 81);
            Controls.Add(dbmlEditor);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(530, 120);
            Name = "DBModelLookupEditor";
            StartPosition = FormStartPosition.CenterScreen;
            ResumeLayout(false);
        }

        #endregion

        private WinFormsComponents.Controls.DBModelLookupEditor dbmlEditor;
    }
}