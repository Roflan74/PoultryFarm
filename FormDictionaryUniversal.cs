using PoultryFarm.Domains;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PoultryFarm
{
    public partial class FormDictionaryUniversal : Form
    {
        private DatabaseManager _databaseManager;
        private string _tableName;

        public FormDictionaryUniversal(
            DatabaseManager databaseManager,
            string tableName)
        {
            InitializeComponent();
            _databaseManager = databaseManager;
            _tableName = tableName;
            this.Text = $"Справочник '{tableName}'";
        }

        // =========================================================================
        // НОВЫЙ МЕТОД: УМНАЯ ПОДМЕНА ID НА НАЗВАНИЯ В ТАБЛИЦЕ
        // =========================================================================
        private void FormatForeignKeyColumns()
        {
            // Определяем имя первичного ключа, чтобы случайно его не скрыть
            string primaryKey = _tableName + "Id";
            if (_tableName.EndsWith("s", StringComparison.OrdinalIgnoreCase))
                primaryKey = _tableName.Substring(0, _tableName.Length - 1) + "Id";
            if (_tableName.Equals(Constants.Tables.WorkerCageAssignment, StringComparison.OrdinalIgnoreCase))
                primaryKey = "AssignmentId";

            // Собираем колонки, которые являются внешними ключами
            var columnsToReplace = new List<DataGridViewColumn>();
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                if (col.Name.EndsWith("Id") && !col.Name.Equals(primaryKey, StringComparison.OrdinalIgnoreCase))
                {
                    columnsToReplace.Add(col);
                }
            }

            foreach (var col in columnsToReplace)
            {
                string refTableName = "";
                string displayMember = "";

                // Определяем, откуда брать названия
                if (col.Name == "BreedId") { refTableName = Constants.Tables.Breed; displayMember = "Name"; }
                else if (col.Name == "CageId") { refTableName = Constants.Tables.Cage; displayMember = "CageNumber"; }
                // Было: else if (col.Name == "WorkerId") { refTableName = Constants.Tables.Worker; displayMember = "PassportData"; }
                else if (col.Name == "WorkerId") { refTableName = Constants.Tables.Worker; displayMember = "FullName"; }
                else if (col.Name == "DietId") { refTableName = Constants.Tables.Diet; displayMember = "Description"; }
                else if (col.Name == "ShopId") { refTableName = Constants.Tables.Shop; displayMember = "Name"; }

                if (!string.IsNullOrEmpty(refTableName))
                {
                    // Загружаем справочную таблицу в память (в фоновом режиме)
                    _databaseManager.LoadTable(refTableName);
                    var refTable = _databaseManager.GetTable(refTableName);

                    // Создаем "умную" колонку
                    var comboCol = new DataGridViewComboBoxColumn();
                    comboCol.Name = col.Name;
                    comboCol.DataPropertyName = col.DataPropertyName;
                    comboCol.HeaderText = col.HeaderText;             // Оставляем русское название (которое подгрузилось из JSON)
                    comboCol.DataSource = refTable;                   // Таблица с названиями
                    comboCol.ValueMember = col.Name;                  // Скрытое поле (цифра ID)
                    comboCol.DisplayMember = displayMember;           // Видимое поле (текст, например, "Леггорн")

                    // МАГИЯ: Прячем стрелочку списка, чтобы колонка выглядела как обычный текст!
                    comboCol.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                    comboCol.ReadOnly = true;

                    // Меняем глупую текстовую колонку на умную
                    int colIndex = col.Index;
                    dataGridView1.Columns.RemoveAt(colIndex);
                    dataGridView1.Columns.Insert(colIndex, comboCol);
                }
            }
        }
        // =========================================================================

        private void btnLoad_Click(object sender, EventArgs e)
        {
            _databaseManager.LoadTable(_tableName);
            dataGridView1.DataSource = _databaseManager.GetTable(_tableName);

            DataVisualHelper.DataGridViewReadOnly(dataGridView1);
            DataVisualHelper.LocalizeTable(_tableName, dataGridView1);
            DataVisualHelper.ApplyModernGridStyle(dataGridView1);

            FormatForeignKeyColumns(); // Вызываем подмену ID на текст
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var gridRow = dataGridView1.SelectedRows.Cast<DataGridViewRow>().FirstOrDefault();
            if (gridRow == null)
            {
                MessageBox.Show("Выберите строку таблицы!");
                return;
            }
            var selectedRow = ((DataRowView)gridRow.DataBoundItem).Row;

            var formUniversal = new FormUniversalEditEntity(_databaseManager);
            formUniversal.EditDataRow = selectedRow;
            formUniversal.ShowDialog();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_databaseManager.UpdateDatabase(_tableName))
            {
                _databaseManager.LoadTable(_tableName);
                dataGridView1.DataSource = _databaseManager.GetTable(_tableName);

                DataVisualHelper.DataGridViewReadOnly(dataGridView1);
                DataVisualHelper.LocalizeTable(_tableName, dataGridView1);
                DataVisualHelper.ApplyModernGridStyle(dataGridView1);

                FormatForeignKeyColumns(); // Вызываем подмену ID на текст

                MessageBox.Show("Успешно сохранено!");
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            var formUniversal = new FormUniversalEditEntity(_databaseManager);
            var row = _databaseManager.CreateNewRow(_tableName);
            formUniversal.EditDataRow = row;

            if (formUniversal.ShowDialog() == DialogResult.OK)
            {
                _databaseManager.AddNewRow(_tableName, formUniversal.EditDataRow);

                if (_databaseManager.UpdateDatabase(_tableName))
                {
                    _databaseManager.LoadTable(_tableName);
                    dataGridView1.DataSource = _databaseManager.GetTable(_tableName);

                    DataVisualHelper.DataGridViewReadOnly(dataGridView1);
                    DataVisualHelper.LocalizeTable(_tableName, dataGridView1);
                    DataVisualHelper.ApplyModernGridStyle(dataGridView1);

                    FormatForeignKeyColumns(); // Вызываем подмену ID на текст
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var gridRow = dataGridView1.SelectedRows.Cast<DataGridViewRow>().FirstOrDefault();
            if (gridRow == null)
            {
                MessageBox.Show("Выберите строку таблицы для удаления!");
                return;
            }

            var selectedRow = ((DataRowView)gridRow.DataBoundItem).Row;

            // Бизнес-логика увольнения
            if (_tableName == Constants.Tables.Worker)
            {
                string workerId = selectedRow["WorkerId"].ToString();
                DataTable assignments = _databaseManager.ExecuteQuery(
                    $"SELECT COUNT(*) FROM WorkerCageAssignments WHERE WorkerId = {workerId}");

                if (assignments.Rows.Count > 0 && Convert.ToInt32(assignments.Rows[0][0]) > 0)
                {
                    MessageBox.Show("Невозможно уволить этого работника!\n\n" +
                                    "За ним всё ещё закреплены клетки с курами. По правилам птицефабрики, " +
                                    "сначала вы должны перейти в журнал «Закрепление клеток» и передать " +
                                    "его клетки другому сотруднику.",
                                    "Ошибка бизнес-логики",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                    return;
                }
            }

            var dialogResult = MessageBox.Show(
                "Вы уверены, что хотите удалить эту запись?",
                "Предупреждение",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (dialogResult == DialogResult.No)
                return;

            _databaseManager.DeleteRow(selectedRow);
        }
    }
}