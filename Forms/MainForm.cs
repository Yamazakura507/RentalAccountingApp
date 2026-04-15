using RentalAccountingApp.Forms;

namespace RentalAccountingApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void tsbSetingsOnClick(object sender, EventArgs e)
        {
            new SettingsForm().Show();
        }
    }
}
