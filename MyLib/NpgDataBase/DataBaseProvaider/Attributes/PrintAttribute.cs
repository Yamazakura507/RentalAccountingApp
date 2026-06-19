using DataBaseProvaider.Enums;
using System.Runtime.CompilerServices;

namespace DataBaseProvaider.Attributes
{
    /// <summary>
    /// Атрибут указывающий, что модель имеет функционал печати
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = true)]
    public class PrintAttribute : Attribute
    {
        /// <summary>
        /// Заголовок отчета
        /// </summary>
        public string Title { get; set; } = String.Empty;

        /// <summary>
        /// Имя файла ресурса отчета шаблона для печати .frx
        /// </summary>
        public string NameResourceReport { get; set; } = String.Empty;

        /// <summary>
        /// Масив параметров отчета, вида (Имя параметра=Имя параметра модели)
        /// </summary>
        public string[]? ReportParameters { get; set; } = null;

        /// <summary>
        /// Настройка указывающая на возможность массовой выгрузки отчетов
        /// </summary>
        public bool IsManyExport = true;

        /// <summary>
        /// Масив расширений, (Ключ: Подпись, Значение: Расширение файла)
        /// </summary>
        public PrintExtension[]? ExtensionsExport { get; set; } = [ PrintExtension.PDF, PrintExtension.Word, PrintExtension.FPX, PrintExtension.Image ];

        
    }
}
