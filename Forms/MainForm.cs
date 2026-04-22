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
        }

        private void tsbSetingsOnClick(object sender, EventArgs e)
        {
            new SettingsForm().Show();
        }

        private void MainFormOnLoad(object sender, EventArgs e)
        {
            
        }
    }
}
