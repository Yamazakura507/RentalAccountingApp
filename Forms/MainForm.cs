using RentalAccountingApp.Forms;
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
            dbmlvMaterials.ModelType = typeof(Materials);
            dmlvCategories.ModelType = typeof(Categories);
            dmlvInventory.ModelType = typeof(Inventory);
        }

        private void dbmlvLookupOnUpdateChanged(object sender, Action<object> e) => new DBModelLookupEditor(sender, e).Show();

        private void dbmlvLookupOnInsertChanged(object sender, Action<object> e) => new DBModelLookupEditor(((WinFormsComponents.Controls.DBModelListView)sender).ModelType, e).Show();

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
                    case 1:
                        dbmlvMaterials.lvModelOnKeyDown(sender, e);
                        break;
                    case 2:
                        dmlvCategories.lvModelOnKeyDown(sender, e);
                        break;
                }
            }
        }
    }
}
