using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WinFormsComponents.Classes.Model
{
    /// <summary>
    /// Параметр модели
    /// </summary>
    public class ModelParametrEditor : INotifyPropertyChanged
    {
        private object value;

        /// <summary>
        /// Подпись параметар
        /// </summary>
        public string Title { get; }
        /// <summary>
        /// Значение параметра
        /// </summary>
        public object Value 
        { 
            get => value;
            set
            {
                if (this.value != value)
                {
                    this.value = value;
                    OnPropertyChanged();
                }
            } 
        }
        /// <summary>
        /// Тэг параметра
        /// </summary>
        public string Tag { get; }
        /// <summary>
        /// Тип параметар
        /// </summary>
        public Type Type {  get; }
        /// <summary>
        /// Ограничитель ввода значения
        /// </summary>
        public SettingFilter SettingFilter { get; set; }

        /// <summary>
        /// Конструктор параметра модели
        /// </summary>
        /// <param name="title">Подпись параметра</param>
        /// <param name="value">Значение параметара</param>
        /// <param name="tag">Тэг параметра</param>
        /// <param name="type">Тип параметра</param>
        public ModelParametrEditor(string title, object value, string tag, Type type)
        {
            Title = title;
            Value = value;
            Tag = tag;
            Type = type;
        }

        public ModelParametrEditor() 
        { 
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
