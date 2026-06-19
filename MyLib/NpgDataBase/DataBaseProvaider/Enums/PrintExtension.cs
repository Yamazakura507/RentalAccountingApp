using DataBaseProvaider.Attributes;
using System.ComponentModel;

namespace DataBaseProvaider.Enums
{
    /// <summary>
    /// Перечесление расширений файлов для экспорта
    /// </summary>
    /// <remarks>
    /// Word - Docx,
    /// PDF - Pdf,
    /// FPX - FastReport,
    /// Image - Jpg
    /// </remarks>
    public enum PrintExtension
    {
        /// <summary>
        /// Word - Docx
        /// </summary>
        /// <value>0</value>
        [Comment("wordDoc")]
        [Description("Word File|*.docx")]
        Word = 0,
        /// <summary>
        /// PDF - Pdf
        /// </summary>
        /// <value>1</value>
        [Comment("pdfDoc")]
        [Description("PDF File|*.pdf")]
        PDF = 1,
        /// <summary>
        /// FPX - FastReport
        /// </summary>
        /// <value>2</value>
        [Comment("frxDoc")]
        [Description("FastReport File|*.fpx")]
        FPX = 2,
        /// <summary>
        /// Image - Jpg
        /// </summary>
        /// <value>3</value>
        [Comment("imageDoc")]
        [Description("Image File|*.jpg")]
        Image = 3
    }
}
