using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PoultryFarm
{
    public static class DataVisualHelper
    {
        /// <summary>
        /// Переименовывает и скрывает колонки DataGridView на основе JSON-настроек
        /// </summary>
        public static void LocalizeTable(string tableName, DataGridView gridView)
        {
            // загружаем данные для локализации из JSON
            var localize = TableLocalization.Load(tableName);

            // если файл локализации не найден - выходим
            if (localize == null)
            {
                return;
            }

            // Проходим по всем колонкам для локализации
            for (int i = 0; i < localize.SourceColumns.Count; i++)
            {
                string sourceName = localize.SourceColumns[i];
                string localizeName = localize.LocalizeColumns[i];

                // ПРОВЕРКА: существует ли колонка в DataGridView
                if (gridView.Columns.Contains(sourceName))
                {
                    gridView.Columns[sourceName].HeaderText = localizeName;
                }
            }

            // Скрываем ненужные колонки (например, технические ID)
            if (localize.HideColumns != null)
            {
                foreach (string columnName in localize.HideColumns)
                {
                    if (gridView.Columns.Contains(columnName))
                    {
                        gridView.Columns[columnName].Visible = false;
                    }
                }
            }
        }

        /// <summary>
        /// Настраивает DataGridView только для чтения и полного выделения строки
        /// </summary>
        public static void DataGridViewReadOnly(DataGridView gridView)
        {
            gridView.AllowUserToAddRows = false;
            gridView.AllowUserToDeleteRows = false;
            gridView.ReadOnly = true;
            gridView.AllowUserToResizeRows = false;
            gridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            // Убираем пустой столбец слева для красоты
            gridView.RowHeadersVisible = false;
        }
    }
}