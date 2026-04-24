using System.ComponentModel;
using WinFormsComponents.Classes.Enums;

namespace WinFormsComponents.Controls
{
    public partial class DBModelLookupEditor : UserControl
    {
        private Loader loader = new() { Size = new(50, 50) };
        private string parametrTitle;
        private string parametrValue;
        private EditorMode editorMode;

        /// <summary>
        /// Событие при добавлении
        /// </summary>
        public event EventHandler InsertChanged;

        /// <summary>
        /// Событие при обновлении
        /// </summary>
        public event EventHandler UpdateChanged;

        /// <summary>
        /// Событие при удалении
        /// </summary>
        public event EventHandler DeleteChanged;

        /// <summary>
        /// Событие при востановлении
        /// </summary>
        public event EventHandler RepairChanged;

        /// <summary>
        /// Режим редактирования модели
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public EditorMode EditorMode 
        { 
            get => editorMode;
            set
            {
                if (editorMode != value)
                {
                    editorMode = value;
                    CheckMode();
                }
            }
        }

        /// <summary>
        /// Подпись параметра
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string ParametrTitle
        {
            get => parametrTitle;
            set
            {
                if (parametrTitle != value)
                {
                    parametrTitle = value;
                    lbNameParametr.Text = value;
                }
            }
        }

        /// <summary>
        /// Значение параметра
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string ParametrValue
        {
            get => parametrValue;
            set
            {
                if (parametrValue != value)
                {
                    parametrValue = value;
                    tbValueParametr.Text = value;
                }
            }
        }

        /// <summary>
        /// Тэг обращения к праметру
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string ParametrTag { get; set; }

        public DBModelLookupEditor()
        {
            InitializeComponent();

            loader.AutoSetup(this);
            loader.StartAnimation();
            lbNameParametr.Text = ParametrTitle;
            tbValueParametr.Text = ParametrValue;
            loader.StopAnimation();
            CheckMode();
        }

        private void CheckMode()
        {
            tsbInsert.Visible = tsbSave.Visible = tsbRemove.Visible = tsbRepair.Visible = false;

            switch (EditorMode)
            {
                case EditorMode.Insert:
                    tsbInsert.Visible = true;
                    break;
                case EditorMode.Update:
                    tsbSave.Visible = true;
                    break;
                case EditorMode.UpdateOrDelete:
                    tsbSave.Visible = tsbRemove.Visible = true;
                    break;
                case EditorMode.UpdateOrRepair:
                    tsbSave.Visible = tsbRepair.Visible = true;
                    break;
                default:
                    break;
            }
        }

        protected virtual void OnInsertChanged()
        {
            loader.StartAnimation();
            InsertChanged?.Invoke(this, EventArgs.Empty);
            EditorMode = EditorMode.UpdateOrDelete;
            CheckMode();
            loader.StopAnimation();
        }

        protected virtual void OnUpdateChanged()
        {
            loader.StartAnimation();
            UpdateChanged?.Invoke(this, EventArgs.Empty);
            loader.StopAnimation();
        }

        protected virtual void OnDeleteChanged()
        {
            loader.StartAnimation();
            DeleteChanged?.Invoke(this, EventArgs.Empty);
            EditorMode = EditorMode.UpdateOrRepair;
            CheckMode();
            loader.StopAnimation();
        }

        protected virtual void OnRepairChanged()
        {
            loader.StartAnimation();
            RepairChanged?.Invoke(this, EventArgs.Empty);
            EditorMode = EditorMode.UpdateOrDelete;
            CheckMode();
            loader.StopAnimation();
        }

        private void tsbSaveOnClick(object sender, EventArgs e) => OnUpdateChanged();

        private void tsbAddOnClick(object sender, EventArgs e) => OnInsertChanged();

        private void tsbRemove_Click(object sender, EventArgs e) => OnDeleteChanged();

        private void tsbRepair_Click(object sender, EventArgs e) => OnRepairChanged();

        public void DBModelLookupEditorOnKeyDown(object sender, KeyEventArgs e)
        {
            bool isComand = false;

            switch (e.KeyCode)
            {
                case Keys.Delete
                when EditorMode == EditorMode.UpdateOrDelete:
                    isComand = true;
                    OnDeleteChanged();
                    break;
                case Keys.S 
                when e.Control && (EditorMode is EditorMode.UpdateOrDelete or EditorMode.UpdateOrRepair or EditorMode.Update):
                    isComand = true;
                    OnUpdateChanged();
                    break;
                case Keys.R 
                when e.Control && EditorMode == EditorMode.UpdateOrRepair:
                    isComand = true;
                    OnRepairChanged();
                    break;
                case Keys.Insert
                when EditorMode == EditorMode.Insert:
                    isComand = true;
                    OnInsertChanged();
                    break;
            }

            e.SuppressKeyPress = isComand;
        }
    }
}
