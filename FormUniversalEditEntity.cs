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
    /// <summary>
    /// Универсальная форма редактирования свойств любой сущности
    /// </summary>
    public partial class FormUniversalEditEntity : Form
    {
        private DataRow _row;
        private DatabaseManager _manager;
        private int _topEdge = 20;

        public DataRow EditDataRow
        {
            get { return _row; }
            set
            {
                _row = value;
                GenerateControls();
            }
        }

        public FormUniversalEditEntity(DatabaseManager manager)
        {
            _manager = manager;
            InitializeComponent();
        }

        /// <summary>
        /// Метод для авто-создания элементов (контролов) редактирования полей из таблицы
        /// </summary>
        private void GenerateControls()
        {
            string columnIdName = _row.Table.TableName == Constants.Tables.Breed ? "BreedId" :
                                  _row.Table.TableName == Constants.Tables.Cage ? "CageId" :
                                  _row.Table.TableName == Constants.Tables.Worker ? "WorkerId" :
                                  _row.Table.TableName == Constants.Tables.Chicken ? "ChickenId" : $"{_row.Table.TableName}Id";

            var columnNames = _row.Table.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToList();
            columnNames.Remove(columnIdName);

            var localization = TableLocalization.Load(_row.Table.TableName);

            foreach (var columnName in columnNames)
            {
                DataColumn column = _row.Table.Columns[columnName];
                if (column.ReadOnly) continue;

                Control control = GetControlForColumn(columnName);

                Label label = new Label();
                label.Text = localization == null ? columnName : localization.GetLocale(columnName);

                PlaceControl(groupBox1, control, label);
                SetControlValue(control, columnName);
            }
        }

        private void SetControlValue(Control control, string columnName)
        {
            if (columnName.EndsWith("Id"))
            {
                var comboBox = (ComboBox)control;

                // Определение имени связанной таблицы (например, из BreedId получаем Breeds)
                string refTableName = "";
                if (columnName == "BreedId") refTableName = Constants.Tables.Breed;
                else if (columnName == "CageId") refTableName = Constants.Tables.Cage;
                else if (columnName == "WorkerId") refTableName = Constants.Tables.Worker;

                if (!string.IsNullOrEmpty(refTableName))
                {
                    var table = _manager.GetTable(refTableName);
                    string displayColumn = FindDisplayColumn(table, refTableName);

                    comboBox.DisplayMember = displayColumn;
                    comboBox.ValueMember = columnName;
                    comboBox.DataSource = table;

                    if (_row[columnName] != null && _row[columnName] != DBNull.Value)
                    {
                        string valueString = _row[columnName].ToString();
                        if (long.TryParse(valueString, out var longValue) && longValue > 0)
                        {
                            comboBox.SelectedValue = longValue;
                        }
                    }
                    else
                    {
                        comboBox.SelectedIndex = -1; // Если связи нет, ничего не выбрано
                    }
                }
            }
        }

        /// <summary>
        /// Автоматически находит подходящую колонку для отображения в ComboBox
        /// </summary>
        private string FindDisplayColumn(DataTable table, string tableName)
        {
            var columns = table.Columns.Cast<DataColumn>().Select(c => c.ColumnName).ToList();

            // Для Пород лучше показывать Название
            if (tableName == Constants.Tables.Breed) return "Name";
            // Для Работников - Паспорт/ФИО
            if (tableName == Constants.Tables.Worker) return "PassportData";
            // Для Клеток показываем номер клетки (или можно было бы склеить Цех+Ряд+Номер)
            if (tableName == Constants.Tables.Cage) return "CageNumber";

            var textColumn = columns.FirstOrDefault(c => table.Columns[c].DataType == typeof(string) && !c.EndsWith("Id"));
            if (textColumn != null) return textColumn;

            return columns.Count > 1 ? columns[1] : columns[0];
        }

        private void PlaceControl(GroupBox groupBox, Control control, Label label)
        {
            int leftEdge = 10;
            int marginTop = 15;

            label.Location = new Point(leftEdge, _topEdge);
            groupBox.Controls.Add(label);

            _topEdge += label.Height;

            control.Location = new Point(leftEdge, _topEdge);
            // Делаем контролы пошире для удобства
            control.Width = 300;
            groupBox.Controls.Add(control);

            _topEdge += control.Height + marginTop;
        }

        /// <summary>
        /// Создание контрола для редактирования поля под конкретный тип данных
        /// </summary>
        private Control GetControlForColumn(string columnName)
        {
            Control control = null;
            DataColumn column = _row.Table.Columns[columnName];

            if (columnName.EndsWith("Id"))
            {
                var comboBox = new ComboBox();
                comboBox.Tag = columnName;
                comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                return comboBox;
            }

            Type type = column.DataType;

            // Для строк, чисел с плавающей точкой и денег используем обычный TextBox
            if (type == typeof(string) || type == typeof(double) || type == typeof(float) || type == typeof(decimal))
            {
                control = new TextBox();
                control.Text = _row[columnName].ToString();
            }
            // Для целых чисел используем NumericUpDown
            else if (type == typeof(int))
            {
                control = new NumericUpDown();
                ((NumericUpDown)control).Maximum = int.MaxValue;
                string valueString = _row[columnName].ToString();
                int.TryParse(valueString, out int valueInt);
                ((NumericUpDown)control).Value = valueInt;
            }
            else if (type == typeof(DateTime))
            {
                control = new DateTimePicker();
            }
            else
            {
                throw new NotSupportedException($"Type not supported '{type.Name}', column '{columnName}'");
            }

            control.Tag = columnName;
            return control;
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            foreach (Control control in groupBox1.Controls)
            {
                if (control is Label) continue;

                string columnName = control.Tag.ToString();

                if (_row.Table.Columns[columnName].ReadOnly)
                    continue;

                if (control is TextBox textBox)
                {
                    // Проверка для чисел с точкой и decimal
                    Type colType = _row.Table.Columns[columnName].DataType;
                    if (colType == typeof(double) || colType == typeof(float) || colType == typeof(decimal))
                    {
                        string txt = textBox.Text.Replace('.', ','); // Универсальность ввода
                        if (string.IsNullOrEmpty(txt)) _row[columnName] = DBNull.Value;
                        else if (colType == typeof(decimal) && decimal.TryParse(txt, out decimal decVal)) _row[columnName] = decVal;
                        else if (double.TryParse(txt, out double dblVal)) _row[columnName] = dblVal;
                    }
                    else
                    {
                        _row[columnName] = string.IsNullOrEmpty(textBox.Text) ? (object)DBNull.Value : textBox.Text;
                    }
                }
                else if (control is NumericUpDown numeric)
                {
                    _row[columnName] = (int)numeric.Value;
                }
                else if (control is DateTimePicker dateTimePicker)
                {
                    _row[columnName] = dateTimePicker.Value;
                }
                else if (control is ComboBox comboBox)
                {
                    if (comboBox.SelectedValue != null)
                    {
                        _row[columnName] = comboBox.SelectedValue;
                    }
                    else
                    {
                        _row[columnName] = DBNull.Value;
                    }
                }
            }
        }
    }
}