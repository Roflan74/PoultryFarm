using System;
using System.Configuration; // Не забудьте добавить ссылку на System.Configuration в References проекта
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

        // --- КНОПКИ ПТИЦЕФАБРИКИ ---

        // Кнопка открытия справочника "Куры"
        private void btnDictionaryChickens_Click(object sender, EventArgs e)
        {
            var form = new FormDictionaryUniversal(
                _databaseManager, Constants.Tables.Chicken);
            form.ShowDialog();
        }

        // Кнопка открытия справочника "Породы"
        private void btnDictionaryBreeds_Click(object sender, EventArgs e)
        {
            var form = new FormDictionaryUniversal(
                _databaseManager, Constants.Tables.Breed);
            form.ShowDialog();
        }

        // Кнопка открытия справочника "Клетки"
        private void btnDictionaryCages_Click(object sender, EventArgs e)
        {
            var form = new FormDictionaryUniversal(
                _databaseManager, Constants.Tables.Cage);
            form.ShowDialog();
        }

        // Кнопка открытия справочника "Работники"
        private void btnDictionaryWorkers_Click(object sender, EventArgs e)
        {
            var form = new FormDictionaryUniversal(
                _databaseManager, Constants.Tables.Worker);
            form.ShowDialog();
        }
    }
}