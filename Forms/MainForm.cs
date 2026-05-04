using RentalAccountingApp.Forms;
using RentalAccountingApp.Forms.CatalogsForm;
using RentalAccountingApp.Forms.EditForm;
using RentalDBModels.Views;
using WinFormsComponents.Classes;

namespace RentalAccountingApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            ConnectionInfo.ConnectDB();

            dmlvInventory.ModelType = typeof(Inventory);
        }

        private void tsbSetingsOnClick(object sender, EventArgs e) => new SettingsForm().Show();

        private void tcDBViewrOnKeyDown(object sender, KeyEventArgs e)
        {
            bool isComand = false;

            switch (e.KeyCode)
            {
                case Keys.S when e.Control:
                    isComand = true;
                    new SettingsForm().Show();
                    e.SuppressKeyPress = true;
                    break;
            }

            if (!isComand)
            {
                switch (tcDBViewr.SelectedIndex)
                {
                    case 0:
                        dmlvInventory.lvModelOnKeyDown(sender, e);
                        break;
                }
            }
        }

        private void tsbCatalogsOnClick(object sender, EventArgs e) => new BaseCatalogsForm().Show();

        private void dbmlvComplexOnUpdateChanged(object sender, Action e) => new DBModelComplexEditor(sender, e, this).Show();

        private void dbmlvComplexOnInsertChanged(object sender, Action e) => new DBModelComplexEditor(((WinFormsComponents.Controls.DBModelListView)sender).ModelType, e, this).Show();
    }
}
