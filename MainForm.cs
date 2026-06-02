using System;
using System.Configuration;
using System.Windows.Forms;

namespace PoultryFarm
{
    public partial class MainForm : Form
    {
        private string _connectionString = @"";
        private DatabaseManager _databaseManager;

        public MainForm()
        {
            InitializeComponent();
            ReadConnectionString();
            _databaseManager = new DatabaseManager(_connectionString);
        }

        private void ReadConnectionString()
        {
            // Читаем строку подключения из App.config (название должно совпадать)
            _connectionString = ConfigurationManager.ConnectionStrings["PoultryFarmDB"].ConnectionString;
        }

        // --- ВСПОМОГАТЕЛЬНЫЙ МЕТОД ДЛЯ БЕЗОПАСНОГО ОТКРЫТИЯ СЛОВАРЕЙ ---
        private void OpenUniversalForm(string tableName)
        {
            try
            {
                var form = new FormDictionaryUniversal(_databaseManager, tableName);
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при загрузке таблицы '{tableName}':\n{ex.Message}",
                                "Ошибка интерфейса", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- КНОПКИ УПРАВЛЕНИЯ ПТИЦЕФАБРИКОЙ ---

        // Кнопка открытия справочника "Куры"
        private void btnDictionaryChickens_Click(object sender, EventArgs e)
        {
            OpenUniversalForm(Constants.Tables.Chicken);
        }

        // Кнопка открытия справочника "Породы"
        private void btnDictionaryBreeds_Click(object sender, EventArgs e)
        {
            OpenUniversalForm(Constants.Tables.Breed);
        }

        // Кнопка открытия справочника "Клетки"
        private void btnDictionaryCages_Click(object sender, EventArgs e)
        {
            OpenUniversalForm(Constants.Tables.Cage);
        }

        // Кнопка открытия справочника "Работники"
        private void btnDictionaryWorkers_Click(object sender, EventArgs e)
        {
            OpenUniversalForm(Constants.Tables.Worker);
        }

        // Кнопка открытия справочника "Диеты" (Новая)
        private void btnDictionaryDiets_Click(object sender, EventArgs e)
        {
            OpenUniversalForm(Constants.Tables.Diet);
        }

        // Кнопка открытия справочника "Цеха" (Новая)
        private void btnDictionaryShops_Click(object sender, EventArgs e)
        {
            OpenUniversalForm(Constants.Tables.Shop);
        }

        // Кнопка открытия журнала "Закрепление клеток за работниками" (Новая)
        private void btnDictionaryAssignments_Click(object sender, EventArgs e)
        {
            OpenUniversalForm(Constants.Tables.WorkerCageAssignment);
        }

        private void btnDirectorPanel_Click(object sender, EventArgs e)
        {
            var directorForm = new FormDirectorPanel(_databaseManager);
            directorForm.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Время и дата
            lblTime.Text = DateTime.Now.ToString("HH:mm:ss");
            lblDate.Text = DateTime.Now.ToString("dd MMMM yyyy");

            // Приветствие
            int hour = DateTime.Now.Hour;
            if (hour >= 5 && hour < 12) lblGreeting.Text = "Доброе утро, Директор!";
            else if (hour >= 12 && hour < 18) lblGreeting.Text = "Добрый день, Директор!";
            else if (hour >= 18 && hour < 23) lblGreeting.Text = "Добрый вечер, Директор!";
            else lblGreeting.Text = "Доброй ночи, Директор!";
        }
    }
}