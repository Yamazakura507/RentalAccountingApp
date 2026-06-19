using OfficeOpenXml;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace WinFormsComponents.Classes
{
    public static class ExcelReporter
    {
        /// <summary>
        /// Диалог фильтр для Excel файла
        /// </summary>
        public static SaveFileDialog ExportDialog => new SaveFileDialog()
        {
            Filter = "Excel Files|*.xlsx",
            DefaultExt = "xlsx",
            AddExtension = true,
            Title = "Выберите место для сохранения",
            OverwritePrompt = true,
            CheckPathExists = true,
            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
        };

        /// <summary>
        /// Выгрузка объекта списка <see cref="ListView"/> в Excel документ
        /// </summary>
        /// <param name="listView">Объект списка</param>
        /// <param name="filePath">Путь сохранения</param>
        /// <param name="sheetName">Наименование листа</param>
        public static async Task ExportListViewToExcel(ListView listView, string filePath, string sheetName = "Данные")
        {
            ExcelPackage.License.SetNonCommercialPersonal("KuptsovDaniil");

            using (ExcelPackage package = new ())
            {
                ExcelWorksheet? worksheet = package.Workbook.Worksheets.Add(sheetName);

                CreateHeaders(worksheet, listView);
                FillData(worksheet, listView);
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                await File.WriteAllBytesAsync(filePath, await package.GetAsByteArrayAsync());
            }
        }

        /// <summary>
        /// Заполнение шапки страницы документа
        /// </summary>
        /// <param name="worksheet">Лист документа</param>
        /// <param name="listView">Объект списка</param>
        private static void CreateHeaders(ExcelWorksheet worksheet, ListView listView)
        {
            for (int col = 0; col < listView.Columns.Count; col++)
            {
                ExcelRange? cell = worksheet.Cells[1, col + 1];
                cell.Value = listView.Columns[col].Text;

                cell.Style.Font.Bold = true;
                cell.Style.Font.Size = 12;
                cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                cell.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                cell.Style.Border.BorderAround(ExcelBorderStyle.Thin);
            }
        }

        /// <summary>
        /// Заполнение данных старницы документа
        /// </summary>
        /// <param name="worksheet">Лист документа</param>
        /// <param name="listView">Объект списка</param>
        private static void FillData(ExcelWorksheet worksheet, ListView listView)
        {
            for (int row = 0; row < listView.Items.Count; row++)
            {
                ListViewItem item = listView.Items[row];

                for (int col = 0; col < listView.Columns.Count; col++)
                {
                    ExcelRange? cell = worksheet.Cells[row + 2, col + 1];

                    if (col == 0)
                    {
                        cell.Value = item.Text;
                    }
                    else if (col - 1 < item.SubItems.Count)
                    {
                        cell.Value = item.SubItems[col - 1].Text;
                    }

                    cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    cell.Style.Fill.BackgroundColor.SetColor(item.BackColor);
                    cell.Style.Font.Color.SetColor(item.ForeColor);

                    if (item.Font.Bold) cell.Style.Font.Bold = true;

                    cell.Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                }
            }
        }
    }
}
