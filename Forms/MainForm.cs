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
            tcDBViewr.KeyDown += dbmlvMaterials.lvModelOnKeyDown;

            ConnectionInfo.ConnectDB();
            dbmlvMaterials.ModelType = typeof(Materials);
            dmlvCategories.ModelType = typeof(Categories);
        }

        private void dbmlvLookupOnUpdateChanged(object sender, Action<object> e) => new DBModelLookupEditor(sender, e).Show();

        private void dbmlvLookupOnInsertChanged(object sender, Action<object> e) => new DBModelLookupEditor(((WinFormsComponents.Controls.DBModelListView)sender).ModelType, e).Show();

        private void tsbSetingsOnClick(object sender, EventArgs e) => new SettingsForm().Show();

        private void dbmlvLookupOnInsertChanged(object sender, EventArgs e)
        {

        }
    }
}
