using System;
using System.Data;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;

namespace PoultryFarm
{
    public static class ExportManager
    {
        /// <summary>
        /// Формирование сводного отчета в Excel за месяц
        /// </summary>
        public static void ExportMonthlyReportToExcel(DatabaseManager manager)
        {
            Excel.Application excelApp = null;
            Excel.Workbook workbook = null;
            Excel.Worksheet worksheet = null;

            try
            {
                excelApp = new Excel.Application();
                excelApp.Visible = true; // Показываем Excel пользователю
                workbook = excelApp.Workbooks.Add();
                worksheet = (Excel.Worksheet)workbook.Worksheets[1];
                worksheet.Name = "Отчет за месяц";

                int currentRow = 1;

                // Заголовок
                worksheet.Cells[currentRow, 1] = "СВОДНЫЙ ОТЧЕТ О РАБОТЕ ПТИЦЕФАБРИКИ ЗА МЕСЯЦ";
                worksheet.Range[worksheet.Cells[currentRow, 1], worksheet.Cells[currentRow, 3]].Merge();
                worksheet.Cells[currentRow, 1].Font.Bold = true;
                currentRow += 2;

                // 1. Общие показатели
                DataTable dtTotalChickens = manager.ExecuteQuery("SELECT COUNT(*) FROM Chickens");
                DataTable dtTotalEggs = manager.ExecuteQuery("SELECT ISNULL(SUM(EggsPerMonth), 0) FROM Chickens");
                DataTable dtTotalWorkers = manager.ExecuteQuery("SELECT COUNT(*) FROM Workers");

                worksheet.Cells[currentRow++, 1] = $"Общее количество кур на фабрике: {dtTotalChickens.Rows[0][0]}";
                worksheet.Cells[currentRow++, 1] = $"Общее количество полученных яиц: {dtTotalEggs.Rows[0][0]}";
                worksheet.Cells[currentRow++, 1] = $"Общее количество работников: {dtTotalWorkers.Rows[0][0]}";
                currentRow++;

                // 2. Распределение по цехам
                worksheet.Cells[currentRow++, 1] = "Распределение работников по цехам:";
                DataTable dtShops = manager.ExecuteQuery(@"
                    SELECT s.Name, COUNT(DISTINCT wca.WorkerId) 
                    FROM Shops s 
                    LEFT JOIN Cages cg ON s.ShopId = cg.ShopId 
                    LEFT JOIN WorkerCageAssignments wca ON cg.CageId = wca.CageId 
                    GROUP BY s.Name");

                worksheet.Cells[currentRow, 1] = "Цех";
                worksheet.Cells[currentRow, 2] = "Кол-во работников";
                worksheet.Range[worksheet.Cells[currentRow, 1], worksheet.Cells[currentRow, 2]].Font.Bold = true;
                currentRow++;

                foreach (DataRow row in dtShops.Rows)
                {
                    worksheet.Cells[currentRow, 1] = row[0].ToString();
                    worksheet.Cells[currentRow, 2] = row[1].ToString();
                    currentRow++;
                }
                currentRow++;

                // 3. Статистика по породам
                worksheet.Cells[currentRow++, 1] = "Производительность по породам:";
                DataTable dtBreeds = manager.ExecuteQuery(@"
                    SELECT b.Name, COUNT(c.ChickenId), ISNULL(AVG(c.EggsPerMonth), 0) 
                    FROM Breeds b 
                    LEFT JOIN Chickens c ON b.BreedId = c.BreedId 
                    GROUP BY b.Name");

                worksheet.Cells[currentRow, 1] = "Порода";
                worksheet.Cells[currentRow, 2] = "Количество кур";
                worksheet.Cells[currentRow, 3] = "Средняя производительность";
                worksheet.Range[worksheet.Cells[currentRow, 1], worksheet.Cells[currentRow, 3]].Font.Bold = true;
                currentRow++;

                foreach (DataRow row in dtBreeds.Rows)
                {
                    worksheet.Cells[currentRow, 1] = row[0].ToString();
                    worksheet.Cells[currentRow, 2] = row[1].ToString();
                    worksheet.Cells[currentRow, 3] = row[2].ToString();
                    currentRow++;
                }

                // Автовыравнивание ширины колонок
                worksheet.Columns.AutoFit();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при экспорте в Excel: {ex.Message}");
                if (excelApp != null) excelApp.Quit();
            }
        }

        /// <summary>
        /// Формирование справки о породах и их курах в Word
        /// </summary>
        public static void ExportBreedsInfoToWord(DatabaseManager manager)
        {
            Word.Application wordApp = null;
            Word.Document document = null;

            try
            {
                wordApp = new Word.Application();
                wordApp.Visible = true; // Показываем Word
                document = wordApp.Documents.Add();
                Word.Paragraph p = document.Content.Paragraphs.Add();

                p.Range.Text = "СПРАВКА О ПОРОДАХ И ПОГОЛОВЬЕ КУР\n";
                p.Range.Font.Bold = 1;
                p.Range.Font.Size = 14;
                p.Format.SpaceAfter = 12;
                p.Range.InsertParagraphAfter();

                // Получаем все породы
                DataTable dtBreeds = manager.ExecuteQuery("SELECT BreedId, Name, AvgEggsPerMonth, AvgWeight FROM Breeds");

                foreach (DataRow breedRow in dtBreeds.Rows)
                {
                    string breedId = breedRow["BreedId"].ToString();
                    string breedName = breedRow["Name"].ToString();

                    Word.Paragraph pBreed = document.Content.Paragraphs.Add();
                    pBreed.Range.Text = $"Порода: {breedName} (Норма: {breedRow["AvgEggsPerMonth"]} яиц/мес, {breedRow["AvgWeight"]} кг)";
                    pBreed.Range.Font.Bold = 1;
                    pBreed.Range.Font.Size = 12;
                    pBreed.Range.InsertParagraphAfter();

                    // Получаем кур этой породы
                    DataTable dtChickens = manager.ExecuteQuery($@"
                        SELECT c.ChickenId, c.Age, c.Weight, c.EggsPerMonth, cg.CageNumber, s.Name 
                        FROM Chickens c 
                        JOIN Cages cg ON c.CageId = cg.CageId 
                        JOIN Shops s ON cg.ShopId = s.ShopId 
                        WHERE c.BreedId = {breedId}");

                    Word.Paragraph pChickens = document.Content.Paragraphs.Add();
                    pChickens.Range.Font.Bold = 0;
                    pChickens.Range.Font.Size = 11;

                    if (dtChickens.Rows.Count == 0)
                    {
                        pChickens.Range.Text = "  Нет зарегистрированных кур данной породы.\n";
                    }
                    else
                    {
                        foreach (DataRow chicken in dtChickens.Rows)
                        {
                            pChickens.Range.Text = $"  - Курица #{chicken["ChickenId"]} | Возраст: {chicken["Age"]} мес. | Вес: {chicken["Weight"]} кг | Яиц: {chicken["EggsPerMonth"]} | {chicken["Name"]}, Клетка {chicken["CageNumber"]}\n";
                        }
                    }
                    pChickens.Range.InsertParagraphAfter();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при экспорте в Word: {ex.Message}");
                if (wordApp != null) wordApp.Quit();
            }
        }
    }
}