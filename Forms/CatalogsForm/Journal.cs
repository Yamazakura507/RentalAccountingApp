using RentalAccountingApp.Forms.EditForm;
using RentalDBModels.Views;

namespace RentalAccountingApp.Forms.CatalogsForm
{
    public partial class Journal : Form
    {
        public Journal()
        {
            InitializeComponent();

            dmlvInventory.ModelType = typeof(Inventory);
            dmlvRental.ModelType = typeof(Rental);
        }

        private void tcDBViewrOnKeyDown(object sender, KeyEventArgs e)
        {
            switch (tcDBViewr.SelectedIndex)
            {
                case 0:
                    dmlvRental.lvModelOnKeyDown(sender, e);
                    break;
                case 1:
                    dmlvInventory.lvModelOnKeyDown(sender, e);
                    break;
            }
        }

        private void dbmlvComplexOnUpdateChanged(object sender, Action e) => new DBModelComplexEditor(sender, e, this).Show();

        private void dbmlvComplexOnInsertChanged(object sender, Action e) => new DBModelComplexEditor(((WinFormsComponents.Controls.DBModelListView)sender).ModelType, e, this).Show();
    }
}
