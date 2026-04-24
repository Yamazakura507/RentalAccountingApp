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
        }

        private void dbmlvMaterialsOnUpdateChanged(object sender, Action<object> e) => new DBModelLookupEditor(sender, e).Show();

        private void dbmlvMaterialsOnInsertChanged(object sender, Action<object> e) => new DBModelLookupEditor(typeof(Materials), e).Show();

        private void tsbSetingsOnClick(object sender, EventArgs e) => new SettingsForm().Show();
    }
}
