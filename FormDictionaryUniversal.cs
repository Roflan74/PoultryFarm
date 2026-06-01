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

        private void btnLoad_Click(object sender, EventArgs e)
        {
            _databaseManager.LoadTable(_tableName);

            dataGridView1.DataSource = _databaseManager
                .GetTable(_tableName);

            DataVisualHelper.DataGridViewReadOnly(dataGridView1);
            DataVisualHelper.LocalizeTable(_tableName, dataGridView1);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // 1) определить выбранную строку в таблице
            var gridRow = dataGridView1
                .SelectedRows
                .Cast<DataGridViewRow>()
                .FirstOrDefault();

            if (gridRow == null)
            {
                MessageBox.Show("Выберите строку таблицы!");
                return;
            }
            var selectedRow = ((DataRowView)gridRow.DataBoundItem).Row;

            // используем УНИВЕРСАЛЬНУЮ форму редактирования сущности
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
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // 1) определить выбранную строку в таблице
            var gridRow = dataGridView1
                .SelectedRows
                .Cast<DataGridViewRow>()
                .FirstOrDefault();

            if (gridRow == null)
            {
                MessageBox.Show("Выберите строку таблицы для удаления!");
                return;
            }

            // перед удалением - уточнить
            var dialogResult = MessageBox.Show(
                "Вы уверены, что хотите удалить эту запись?",
                "Предупреждение",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (dialogResult == DialogResult.No)
                return;

            var selectedRow = ((DataRowView)gridRow.DataBoundItem).Row;
            _databaseManager.DeleteRow(selectedRow);
        }
    }
}