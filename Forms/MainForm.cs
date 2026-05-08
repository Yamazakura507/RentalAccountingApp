using RentalAccountingApp.Forms;
using RentalAccountingApp.Forms.CatalogsForm;
using WinFormsComponents.Classes;

namespace RentalAccountingApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            ConnectionInfo.ConnectDB();
        }

        private void tsbSetingsOnClick(object sender, EventArgs e) => new SettingsForm().Show();

        private void MainFormOnKeyDown(object sender, KeyEventArgs e)
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
        }

        private void tsbCatalogsOnClick(object sender, EventArgs e) => new BaseCatalogsForm().Show();

        private void tsbClientsOnClick(object sender, EventArgs e) => new ClientsForm().Show();

        private void tsbJournal_Click(object sender, EventArgs e) => new Journal().Show();
    }
}
