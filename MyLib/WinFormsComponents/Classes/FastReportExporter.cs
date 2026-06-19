using DataBaseProvaider;
using DataBaseProvaider.Attributes;
using DataBaseProvaider.Classes;
using DataBaseProvaider.Enums;
using FastReport;
using FastReport.Export;
using FastReport.Export.Image;
using FastReport.Export.OoXML;
using FastReport.Export.Pdf;
using System.Reflection;

namespace WinFormsComponents.Classes
{
    public static class FastReportExporter
    {
        /// <summary>
        /// Диалог фильтр для экспорта отчета
        /// </summary>
        public static SaveFileDialog ExportFileDialog => new SaveFileDialog()
        {
            AddExtension = true,
            Title = "Выберите место для сохранения",
            OverwritePrompt = true,
            CheckPathExists = true,
            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
        };

        /// <summary>
        /// Диалог папки для экспорта отчетов
        /// </summary>
        public static FolderBrowserDialog ExportFolderDialog => new FolderBrowserDialog()
        {
            UseDescriptionForTitle = true,
            Description = "Выберите место для сохранения",
            ShowNewFolderButton = true,
            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
        };

        /// <summary>
        /// Настройка Pdf экспорта
        /// </summary>
        private static PDFExport pdfExport => new PDFExport()
        {
            // Основные настройки
            ShowProgress = false, //Прогресс бар
            OpenAfterExport = false, //Открыть после экспорта
            // Настройки качества
            Compressed = false, //Сжатие(может ухудшить качество)
            EmbeddingFonts = true, // Встраивание шрифтов
            PdfCompliance = PDFExport.PdfStandard.PdfA_1a, // Стандарт PDF/A
            // Настройки документа
            Author = "Пункт проката",
            Keywords = "отчет, статистика, данные",
            // Настройки страниц
            PrintOptimized = true,
            Background = false,
            // Настройки безопасности
            AllowPrint = true,
            AllowModify = false,
            AllowCopy = true
        };
        /// <summary>
        /// Настройка Word экспорта
        /// </summary>
        private static Word2007Export wordExport => new Word2007Export()
        {
            // Основные настройки
            ShowProgress = false, //Прогресс бар
            OpenAfterExport = false, //Открыть после экспорта
            // Настройки качества
            JpegCompression = false, //Сжатие(может ухудшить качество)
            EmbeddingFonts = true, // Встраивание шрифтов
            // Настройки страниц
            PrintOptimized = true,
            MemoryOptimized = true
        };
        /// <summary>
        /// Настройка Image экспорта
        /// </summary>
        private static ImageExport imageExport => new ImageExport()
        {
            // Основные настройки
            ShowProgress = false, // Скрыть прогресс-бар
            OpenAfterExport = false, // Не открывать после экспорта
            // Формат и качество изображения
            ImageFormat = ImageExportFormat.Png, // Формат: Png, Jpeg, Bmp, Gif, Tiff, Emf, Wmf
            Resolution = 96, // Разрешение в DPI (96 - экранное, 300 - печатное)
            JpegQuality = 100, // Качество JPEG (1-100, только для Jpeg)
            // Настройки TIFF (если выбран ImageExportFormat.Tiff)
            MultiFrameTiff = false, // Создавать один многостраничный TIFF или несколько файлов
            MonochromeTiffCompression = System.Drawing.Imaging.EncoderValue.CompressionLZW, //Сжатие для TIFF: LZW, CCITT3, CCITT4, None
        };

        /// <summary>
        /// Экспорт отчета модели
        /// </summary>
        /// <param name="print">Объект настроки печати</param>
        /// <param name="model">Модель печати</param>
        /// <param name="printExtension">Тип экспортируемого файла</param>
        /// <returns>Масив байт файла экспорта указаного типа</returns>
        public async static Task<byte[]> ExportReport(this PrintAttribute print, PrintExtension printExtension, object model = null)
        {
            RegisterSettingFR();

            byte[] exportDoc;

            using (Report report = new Report())
            {
                report.Load(new MemoryStream(print.NameResourceReport.GetByteArrayResources()));

                report.FileName = print.Title;
                report.Preview = null;

                Dictionary<string, object> parametrs = print.GetParametrs(model);

                foreach (KeyValuePair<string, object> parametr in parametrs)
                {
                    report.SetParameterValue(parametr.Key, parametr.Value);
                }

                await report.PrepareAsync();

                ExportBase exportBase = print.GetExportSetting(printExtension, parametrs);

                using (MemoryStream ms = new MemoryStream())
                {
                    if (exportBase is null) report.SavePrepared(ms);
                    else report.Export(exportBase, ms);
                    
                    exportDoc = ms.ToArray();
                }
            }

            return exportDoc;
        }

        /// <summary>
        /// Регистрация расширений FastReport
        /// </summary>
        private static void RegisterSettingFR()
        {
            if (!FastReport.Utils.RegisteredObjects.IsTypeRegistered(typeof(FastReport.Data.PostgresDataConnection)))
            {
                FastReport.Utils.RegisteredObjects.AddConnection(typeof(FastReport.Data.PostgresDataConnection));
            }

            FastReport.Utils.Config.ReportSettings.ShowProgress = false;
        }

        /// <summary>
        /// Получение формата экспорта запрашиваемого к печати
        /// </summary>
        /// <param name="print">Объект настройки печати</param>
        /// <param name="printExtension">Тип расширения</param>
        /// <param name="parametrs">Набор параметров</param>
        /// <returns>Запрошеную настройку экспорта</returns>
        private static ExportBase GetExportSetting(this PrintAttribute print, PrintExtension printExtension, Dictionary<string, object> parametrs)
        {
            ExportBase exportBase = null;
            string title = $"{print.Title}";
            string subject = parametrs.Count > 0 ? String.Join(" ", parametrs.Select(i => i.Value.ToString())) : title;

            switch (printExtension)
            {
                case PrintExtension.Word:
                    wordExport.BaseName = title;
                    exportBase = wordExport;
                    break;
                case PrintExtension.PDF:
                    pdfExport.Title = title;
                    pdfExport.Subject = subject;
                    exportBase = pdfExport;
                    break;
                case PrintExtension.Image:
                    imageExport.BaseName = title;
                    exportBase = imageExport;
                    break;
            }

            return exportBase;
        }

        /// <summary>
        /// Получение списка параметров
        /// </summary>
        /// <param name="print">Объект настройки печати</param>
        /// <param name="model">Модель печати</param>
        /// <returns>Словарь параметров для отчета</returns>
        public static Dictionary<string, object> GetParametrs(this PrintAttribute print, object model)
        {
            if (model is null) return new();

            PropertyInfo[] propertyModel = model.GetType().GetProperties();

            return print.ReportParameters?.ToDictionary().ToDictionary(i => i.Key, i => propertyModel.First(j => j.Name == i.Value).GetValue(model)) ?? new();
        }

        /// <summary>
        /// Вызов диалога соответствующего отчету/отчетам
        /// </summary>
        /// <param name="print">Объект настройки печати</param>
        /// <param name="extension">Тип экспортируемого файла</param>
        /// <param name="isMany">Множественный экспорт</param>
        /// <param name="countSelect">Количество экспортируемых файлов</param>
        /// <param name="selectedModels">Список выбраных для экспорта объектов</param>
        /// <returns>Объект диалога сохранения</returns>
        public static CommonDialog GetPrintDialog(this PrintAttribute print, PrintExtension extension, bool isMany, int countSelect, IEnumerable<object> selectedModels)
        {
            CommonDialog dialog;
            string filter = extension.GetDescription();

            if (isMany)
            {
                dialog = ExportFolderDialog;
            }
            else
            {
                SaveFileDialog saveDialog = ExportFileDialog;

                saveDialog.Filter = filter;
                saveDialog.DefaultExt = filter.Split('.').Last();
                saveDialog.FileName = print.CombainExportName(filter.Split('.').Last(), countSelect > 0 ? selectedModels.First() : null);

                dialog = saveDialog;
            }

            return dialog;
        }

        /// <summary>
        /// Сгенирировать имя файла
        /// </summary>
        /// <param name="print">Объект настройки печати</param>
        /// <param name="extension">Расширение файла</param>
        /// <param name="model">Модель печати, если есть</param>
        /// <returns>Имя файла</returns>
        public static string CombainExportName(this PrintAttribute print, string extension, object model = null) => String.Format(
                                        "{0}_{1}_{2:dd_MM_yyyy}.{3}",
                                        print.Title.Replace(" | ", "_"),
                                        model is null ? String.Empty : String.Join("_", print.GetParametrs(model).Select(i => $"{i.Value}")),
                                        DateTime.Now,
                                        extension);
    }
}
