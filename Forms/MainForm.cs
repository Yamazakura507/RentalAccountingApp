using RentalAccountingApp.Forms;
using RentalDBModels.Views;

namespace RentalAccountingApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            dbmlvMaterials.ModelType = typeof(Materials);
        }

        private void tsbSetingsOnClick(object sender, EventArgs e)
        {
            new SettingsForm().Show();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            
        }
    }
}
