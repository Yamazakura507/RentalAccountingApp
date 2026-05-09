namespace WinFormsComponents.Classes.Interface
{
    internal interface ISelected
    {
        string PKColName { get; set; }
        Type ModelType { get; set; }
        bool IsNullVal { get; set; }
        int? SelectedVal { get; set; }

        event EventHandler SelectedChange;
    }
}
