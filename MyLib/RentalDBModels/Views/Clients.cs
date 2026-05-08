using DataBaseProvaider.Attributes;
using RentalDBModels.Views.Abstract;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace RentalDBModels.Views
{
    public class Clients : BaseRemovingView
    {
        [ViewModel(Headline = true)]
        [Check(RegexPattern = @"^[А-ЯЁA-Z][а-яёa-z']+(-[А-ЯЁA-Z][а-яёa-z']+)?\s+[А-ЯЁA-Z][а-яёa-z']+(\s+[А-ЯЁA-Z][а-яёa-z']+)?$", 
            RegexOptions = RegexOptions.IgnoreCase | RegexOptions.Compiled,
            NotChecibleMessage = "Некоректный формат ФИО.\nКоректный формат: Фамилия Имя( Отчество) любые 2-3 слова через пробел.\nПример: Иванов Иван( Иванович)")]
        [Description("Клиент")]
        public string OwnerName { get; set; }

        [ViewModel]
        [Check(RegexPattern = @"^[78]?[0-9]{10}$", 
            NotChecibleMessage = "Некоректный формат телефона.\nКоректный формат: первая цифра 7 или 8 далее 9-10 любых цифр.\nПример: (8/7)XXXXXXXXX(X)")]
        [Description("Телефон")]
        public string Phone { get; set; }

        [ViewModel(ViewHide = true, Image = true)]
        public string ImageKey { get; set; } = "clients.png";

        [ViewModel(ViewHide = true)]
        public override Type ModelType { get => typeof(Models.Clients); }
    }
}
