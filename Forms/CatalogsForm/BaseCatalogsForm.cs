using RentalAccountingApp.Forms.EditForm;
using RentalDBModels.Views;

namespace RentalAccountingApp.Forms.CatalogsForm
{
    public partial class BaseCatalogsForm : Form
    {
        public Type ModelTypeSelectCatalog = null;

        public BaseCatalogsForm()
        {
            InitializeComponent();

            switch (ModelTypeSelectCatalog)
            {
                case Type t when t.Equals(typeof(Materials)):
                    dbmlvMaterials.ModelType = typeof(Materials);
                    tpCategory.Visible = false;
                    break;
                case Type t when t.Equals(typeof(Materials)):
                    dmlvCategories.ModelType = typeof(Categories);
                    tpMaterial.Visible = false;
                    break;
                default:
                    dbmlvMaterials.ModelType = typeof(Materials);
                    dmlvCategories.ModelType = typeof(Categories);
                    break;
            }
        }

        private void dbmlvLookupOnUpdateChanged(object sender, Action e) => new DBModelLookupEditor(sender, e, this).Show();

        private void dbmlvLookupOnInsertChanged(object sender, Action e) => new DBModelLookupEditor(((WinFormsComponents.Controls.DBModelListView)sender).ModelType, e, this).Show();

        private void tcDBViewrOnKeyDown(object sender, KeyEventArgs e)
        {
            switch (tcDBViewr.SelectedIndex)
            {
                case 0:
                    dbmlvMaterials.lvModelOnKeyDown(sender, e);
                    break;
                case 1:
                    dmlvCategories.lvModelOnKeyDown(sender, e);
                    break;
            }
        }
    }
}
