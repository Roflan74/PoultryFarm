using System;
using System.Data;
using System.Windows.Forms;

namespace PoultryFarm
{
    public partial class FormDirectorPanel : Form
    {
        private DatabaseManager _manager;

        public FormDirectorPanel(DatabaseManager manager)
        {
            InitializeComponent();
            _manager = manager;
            this.Text = "Панель Директора - Аналитика";

            // Настройка выпадающего списка
            SetupComboBox();

            // Визуальная настройка таблицы
            DataVisualHelper.DataGridViewReadOnly(dataGridViewReport);
            DataVisualHelper.ApplyModernGridStyle(dataGridViewReport);
        }

        private void SetupComboBox()
        {
            cmbReports.Items.Add("1. Производительность кур по возрасту, весу и породе");
            cmbReports.Items.Add("2. Распределение пород по цехам (где больше всего?)");
            cmbReports.Items.Add("3. Информация о клетках, диетах и возрасте кур");
            cmbReports.Items.Add("4. Суммарное количество яиц по работникам");
            cmbReports.Items.Add("5. Среднее количество яиц в день на каждого работника");
            cmbReports.Items.Add("6. Курица-рекордсменка (максимальная производительность)");
            cmbReports.Items.Add("7. Количество кур каждой породы в каждом цехе");
            cmbReports.Items.Add("8. Какое количество кур обслуживает каждый работник");
            cmbReports.Items.Add("9. Разница: производительность породы vs Средняя по фабрике");

            cmbReports.SelectedIndex = 0; // Выбираем первый пункт по умолчанию
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            string query = "";

            switch (cmbReports.SelectedIndex)
            {
                case 0:
                    query = @"SELECT b.Name AS [Порода], c.Weight AS [Вес], c.Age AS [Возраст], SUM(c.EggsPerMonth) AS [Всего яиц в месяц] 
                              FROM Chickens c JOIN Breeds b ON c.BreedId = b.BreedId 
                              GROUP BY b.Name, c.Weight, c.Age";
                    break;
                case 1:
                    query = @"SELECT s.Name AS [Цех], b.Name AS [Порода], COUNT(c.ChickenId) AS [Количество кур] 
                              FROM Chickens c JOIN Cages cg ON c.CageId = cg.CageId JOIN Shops s ON cg.ShopId = s.ShopId JOIN Breeds b ON c.BreedId = b.BreedId 
                              GROUP BY s.Name, b.Name ORDER BY [Количество кур] DESC";
                    break;
                case 2:
                    query = @"SELECT cg.CageNumber AS [Клетка], s.Name AS [Цех], c.Age AS [Возраст курицы], d.DietNumber AS [Номер диеты] 
                              FROM Chickens c JOIN Cages cg ON c.CageId = cg.CageId JOIN Shops s ON cg.ShopId = s.ShopId 
                              JOIN Breeds b ON c.BreedId = b.BreedId JOIN Diets d ON b.DietId = d.DietId";
                    break;
                case 3:
                    query = @"SELECT w.FullName AS [Работник], SUM(c.EggsPerMonth) AS [Яиц в месяц], ROUND(SUM(c.EggsPerMonth)/30.0, 2) AS [Яиц в день] 
                              FROM Workers w JOIN WorkerCageAssignments wca ON w.WorkerId = wca.WorkerId JOIN Chickens c ON wca.CageId = c.CageId 
                              GROUP BY w.FullName";
                    break;
                case 4:
                    query = @"SELECT w.FullName AS [Работник], ROUND(AVG(c.EggsPerMonth)/30.0, 2) AS [Среднее кол-во яиц в день от одной курицы] 
                              FROM Workers w JOIN WorkerCageAssignments wca ON w.WorkerId = wca.WorkerId JOIN Chickens c ON wca.CageId = c.CageId 
                              GROUP BY w.FullName";
                    break;
                case 5:
                    query = @"SELECT TOP 1 s.Name AS [Цех], b.Name AS [Порода], c.ChickenId AS [ID Курицы], c.EggsPerMonth AS [Яиц в месяц] 
                              FROM Chickens c JOIN Cages cg ON c.CageId = cg.CageId JOIN Shops s ON cg.ShopId = s.ShopId JOIN Breeds b ON c.BreedId = b.BreedId 
                              ORDER BY c.EggsPerMonth DESC";
                    break;
                case 6:
                    query = @"SELECT s.Name AS [Цех], b.Name AS [Порода], COUNT(c.ChickenId) AS [Количество кур] 
                              FROM Chickens c JOIN Cages cg ON c.CageId = cg.CageId JOIN Shops s ON cg.ShopId = s.ShopId JOIN Breeds b ON c.BreedId = b.BreedId 
                              GROUP BY s.Name, b.Name ORDER BY s.Name";
                    break;
                case 7:
                    query = @"SELECT w.FullName AS [Работник], COUNT(c.ChickenId) AS [Обслуживает кур] 
                              FROM Workers w LEFT JOIN WorkerCageAssignments wca ON w.WorkerId = wca.WorkerId LEFT JOIN Chickens c ON wca.CageId = c.CageId 
                              GROUP BY w.FullName";
                    break;
                case 8:
                    query = @"SELECT b.Name AS [Порода], ROUND(AVG(CAST(c.EggsPerMonth AS FLOAT)), 2) AS [Среднее породы], 
                                     (SELECT ROUND(AVG(CAST(EggsPerMonth AS FLOAT)), 2) FROM Chickens) AS [Среднее по фабрике],
                                     ROUND(AVG(CAST(c.EggsPerMonth AS FLOAT)) - (SELECT AVG(CAST(EggsPerMonth AS FLOAT)) FROM Chickens), 2) AS [Разница]
                              FROM Chickens c JOIN Breeds b ON c.BreedId = b.BreedId 
                              GROUP BY b.Name";
                    break;
            }

            try
            {
                dataGridViewReport.DataSource = _manager.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при формировании отчета:\n{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            ExportManager.ExportMonthlyReportToExcel(_manager);
        }

        private void btnExportWord_Click(object sender, EventArgs e)
        {
            ExportManager.ExportBreedsInfoToWord(_manager);
        }
    }
}