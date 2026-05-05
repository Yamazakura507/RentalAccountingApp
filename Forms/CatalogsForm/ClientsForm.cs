using RentalAccountingApp.Forms.EditForm;
using RentalDBModels.Views;

namespace RentalAccountingApp.Forms.CatalogsForm
{
    public partial class ClientsForm : Form
    {
        public ClientsForm()
        {
            InitializeComponent();

            dmlvClients.ModelType = typeof(Clients);
        }

        private void dbmlvComplexOnUpdateChanged(object sender, Action e) => new DBModelComplexEditor(sender, e, this).Show();

        private void dbmlvComplexOnInsertChanged(object sender, Action e) => new DBModelComplexEditor(((WinFormsComponents.Controls.DBModelListView)sender).ModelType, e, this).Show();

        private void tcDBViewrOnKeyDown(object sender, KeyEventArgs e)
        {
            switch (tcDBViewr.SelectedIndex)
            {
                case 0:
                    dmlvClients.lvModelOnKeyDown(sender, e);
                    break;
            }
        }
    }
}
