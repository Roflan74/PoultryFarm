using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using PoultryFarm.Domains;

namespace PoultryFarm
{
    public class DatabaseManager
    {
        private Dictionary<Type, SqlDbType> _mapTypes =
            new Dictionary<Type, SqlDbType>
            {
                { typeof(int), SqlDbType.Int },
                { typeof(string), SqlDbType.NVarChar },
                { typeof(bool), SqlDbType.Bit },
                { typeof(long), SqlDbType.BigInt },
                { typeof(float), SqlDbType.Real },
                { typeof(double), SqlDbType.Float },
                { typeof(TimeSpan), SqlDbType.Time },
                { typeof(decimal), SqlDbType.Decimal }
            };

        /// <summary>
        /// строка подключения
        /// </summary>
        private string _connectionString;

        /// <summary>
        /// подключение к серверу СУБД
        /// </summary>
        private SqlConnection _connection;

        /// <summary>
        /// для запроса и обновления таблиц
        /// </summary>
        private SqlDataAdapter _adapter;

        /// <summary>
        /// Локальный кэш
        /// </summary>
        private DataSet _dataSet;

        public DatabaseManager(string connectionString)
        {
            _connectionString = connectionString;
            _connection = new SqlConnection(_connectionString);
            _dataSet = new DataSet();
            _adapter = new SqlDataAdapter();
        }

        public void LoadTable(string tableName)
        {
            if (_dataSet.Tables.Contains(tableName))
            {
                _dataSet.Tables.Remove(tableName);
            }

            // Для наших таблиц достаточно простого SELECT *
            string queryString = $"select * from dbo.[{tableName}]";

            try
            {
                _connection.Open();
                var command = new SqlCommand(queryString, _connection);
                _adapter.SelectCommand = command;

                var table = new DataTable(tableName);
                _adapter.Fill(table);
                _dataSet.Tables.Add(table);

                _connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка загрузки таблицы");
            }
            finally
            {
                if (_connection.State == ConnectionState.Open)
                    _connection.Close();
            }
        }

        /// <summary>
        /// Получить таблицу из локального хранилища
        /// </summary>
        public DataTable GetTable(string tableName)
        {
            var table = _dataSet.Tables[tableName];
            if (table == null)
            {
                LoadTable(tableName);
            }

            return _dataSet.Tables[tableName];
        }

        private void CreateIdParameter(SqlCommand command, DataTable table, bool isIdOutput)
        {
            var p4 = new SqlParameter()
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = $"@{table.TableName}Id",
                SourceColumn = table.TableName == Constants.Tables.Breed ? "BreedId" :
                               table.TableName == Constants.Tables.Cage ? "CageId" :
                               table.TableName == Constants.Tables.Worker ? "WorkerId" :
                               table.TableName == Constants.Tables.Chicken ? "ChickenId" : $"{table.TableName}Id"
            };
            if (isIdOutput)
                p4.Direction = ParameterDirection.Output;
            command.Parameters.Add(p4);
        }

        private void CreateParameters(DataTable table, SqlCommand command)
        {
            var columns = table.Columns.Cast<DataColumn>().ToList();

            // Исключаем первичный ключ из параметров для INSERT/UPDATE
            string idColumnName = table.TableName == Constants.Tables.Breed ? "BreedId" :
                                  table.TableName == Constants.Tables.Cage ? "CageId" :
                                  table.TableName == Constants.Tables.Worker ? "WorkerId" :
                                  table.TableName == Constants.Tables.Chicken ? "ChickenId" : $"{table.TableName}Id";

            columns = columns.Where(x => x.ColumnName != idColumnName).ToList();

            foreach (DataColumn column in columns)
            {
                if (column.ReadOnly) continue;

                var p = new SqlParameter()
                {
                    ParameterName = "@" + column.ColumnName,
                    SourceColumn = column.ColumnName,
                    SqlDbType = _mapTypes[column.DataType]
                };
                command.Parameters.Add(p);
            }
        }

        private string GenerateUpdateSqlString(DataTable table)
        {
            string tableName = table.TableName;
            string sqlString = $"update dbo.[{tableName}] set ";

            var columns = table.Columns.Cast<DataColumn>().ToList();
            List<string> columnNames = columns.Select(c => c.ColumnName).ToList();

            string columnId = tableName == Constants.Tables.Breed ? "BreedId" :
                              tableName == Constants.Tables.Cage ? "CageId" :
                              tableName == Constants.Tables.Worker ? "WorkerId" :
                              tableName == Constants.Tables.Chicken ? "ChickenId" : $"{tableName}Id";

            columnNames.Remove(columnId);

            string sqlAssign = string.Join(", ", columnNames.Select(name => $"[{name}] = @{name}"));
            sqlString = sqlString + sqlAssign + $" where {columnId} = @{columnId}";

            return sqlString;
        }

        private string GenerateInsertSqlString(DataTable table)
        {
            string tableName = table.TableName;
            var columns = table.Columns.Cast<DataColumn>().ToList();
            List<string> columnNames = columns.Select(c => c.ColumnName).ToList();

            string columnId = tableName == Constants.Tables.Breed ? "BreedId" :
                              tableName == Constants.Tables.Cage ? "CageId" :
                              tableName == Constants.Tables.Worker ? "WorkerId" :
                              tableName == Constants.Tables.Chicken ? "ChickenId" : $"{tableName}Id";

            columnNames.Remove(columnId);

            string sqlColumnsInBrackets = "(" + string.Join(", ", columnNames.Select(n => $"[{n}]")) + ") ";
            string sqlParametersInBrackets = "(@" + string.Join(", @", columnNames) + ") ";

            string sqlString =
                $"insert into dbo.[{tableName}] " +
                sqlColumnsInBrackets +
                " values " +
                sqlParametersInBrackets + "; " +
                $"select @{columnId} = SCOPE_Identity()";

            return sqlString;
        }

        private string GenerateDeleteSqlString(DataTable table)
        {
            string tableName = table.TableName;
            string columnId = tableName == Constants.Tables.Breed ? "BreedId" :
                              tableName == Constants.Tables.Cage ? "CageId" :
                              tableName == Constants.Tables.Worker ? "WorkerId" :
                              tableName == Constants.Tables.Chicken ? "ChickenId" : $"{tableName}Id";

            string sqlString =
                $"delete from dbo.[{tableName}] " +
                $"where {columnId} = @{columnId}";

            return sqlString;
        }

        public bool UpdateDatabase(string tableName)
        {
            bool isSuccess = false;
            try
            {
                _connection.Open();

                var table = GetTable(tableName);
                string sqlUpdate = GenerateUpdateSqlString(table);
                string sqlInsert = GenerateInsertSqlString(table);
                string sqlDelete = GenerateDeleteSqlString(table);

                // Команда ОБНОВЛЕНИЯ
                var commandUpdate = new SqlCommand(sqlUpdate, _connection);
                CreateParameters(table, commandUpdate);
                CreateIdParameter(commandUpdate, table, isIdOutput: false);
                _adapter.UpdateCommand = commandUpdate;

                // Команда ДОБАВЛЕНИЯ
                var commandInsert = new SqlCommand(sqlInsert, _connection);
                CreateParameters(table, commandInsert);
                CreateIdParameter(commandInsert, table, isIdOutput: true);
                _adapter.InsertCommand = commandInsert;

                // Команда УДАЛЕНИЯ
                var commandDelete = new SqlCommand(sqlDelete, _connection);
                _adapter.DeleteCommand = commandDelete;
                CreateIdParameter(commandDelete, table, isIdOutput: false);

                // выполнение пакетного запроса к SQL
                _adapter.Update(table);

                _connection.Close();
                isSuccess = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка сохранения данных в БД");
            }
            finally
            {
                if (_connection.State == ConnectionState.Open)
                    _connection.Close();
            }

            return isSuccess;
        }

        #region CRUD - ПОРОДЫ (Breeds)
        public void AddBreed(Breed breed)
        {
            var table = GetTable(Constants.Tables.Breed);
            DataRow selectedRow = table.NewRow();
            table.Rows.Add(selectedRow);

            selectedRow["Name"] = breed.Name;
            selectedRow["AvgEggsPerMonth"] = breed.AvgEggsPerMonth;
            selectedRow["AvgWeight"] = breed.AvgWeight;
            selectedRow["DietNumber"] = breed.DietNumber;
        }

        public void UpdateBreed(Breed breed)
        {
            var table = GetTable(Constants.Tables.Breed);
            DataRow selectedRow = table.Select($"BreedId = {breed.BreedId}")?.FirstOrDefault();
            if (selectedRow != null)
            {
                selectedRow["Name"] = breed.Name;
                selectedRow["AvgEggsPerMonth"] = breed.AvgEggsPerMonth;
                selectedRow["AvgWeight"] = breed.AvgWeight;
                selectedRow["DietNumber"] = breed.DietNumber;
            }
        }

        public void DeleteBreed(Breed breed)
        {
            var table = GetTable(Constants.Tables.Breed);
            DataRow selectedRow = table.Select($"BreedId = {breed.BreedId}")?.FirstOrDefault();
            if (selectedRow != null) selectedRow.Delete();
        }
        #endregion

        #region CRUD - РАБОТНИКИ (Workers)
        public void AddWorker(Worker worker)
        {
            var table = GetTable(Constants.Tables.Worker);
            DataRow selectedRow = table.NewRow();
            table.Rows.Add(selectedRow);

            selectedRow["PassportData"] = worker.PassportData;
            selectedRow["Salary"] = worker.Salary;
        }

        public void UpdateWorker(Worker worker)
        {
            var table = GetTable(Constants.Tables.Worker);
            DataRow selectedRow = table.Select($"WorkerId = {worker.WorkerId}")?.FirstOrDefault();
            if (selectedRow != null)
            {
                selectedRow["PassportData"] = worker.PassportData;
                selectedRow["Salary"] = worker.Salary;
            }
        }

        public void DeleteWorker(Worker worker)
        {
            var table = GetTable(Constants.Tables.Worker);
            DataRow selectedRow = table.Select($"WorkerId = {worker.WorkerId}")?.FirstOrDefault();
            if (selectedRow != null) selectedRow.Delete();
        }
        #endregion

        #region CRUD - КЛЕТКИ (Cages)
        public void AddCage(Cage cage)
        {
            var table = GetTable(Constants.Tables.Cage);
            DataRow selectedRow = table.NewRow();
            table.Rows.Add(selectedRow);

            selectedRow["ShopNumber"] = cage.ShopNumber;
            selectedRow["RowNumber"] = cage.RowNumber;
            selectedRow["CageNumber"] = cage.CageNumber;
            selectedRow["WorkerId"] = cage.WorkerId.HasValue ? (object)cage.WorkerId.Value : DBNull.Value;
        }

        public void UpdateCage(Cage cage)
        {
            var table = GetTable(Constants.Tables.Cage);
            DataRow selectedRow = table.Select($"CageId = {cage.CageId}")?.FirstOrDefault();
            if (selectedRow != null)
            {
                selectedRow["ShopNumber"] = cage.ShopNumber;
                selectedRow["RowNumber"] = cage.RowNumber;
                selectedRow["CageNumber"] = cage.CageNumber;
                selectedRow["WorkerId"] = cage.WorkerId.HasValue ? (object)cage.WorkerId.Value : DBNull.Value;
            }
        }

        public void DeleteCage(Cage cage)
        {
            var table = GetTable(Constants.Tables.Cage);
            DataRow selectedRow = table.Select($"CageId = {cage.CageId}")?.FirstOrDefault();
            if (selectedRow != null) selectedRow.Delete();
        }
        #endregion

        #region CRUD - КУРЫ (Chickens)
        public void AddChicken(Chicken chicken)
        {
            var table = GetTable(Constants.Tables.Chicken);
            DataRow selectedRow = table.NewRow();
            table.Rows.Add(selectedRow);

            selectedRow["Weight"] = chicken.Weight;
            selectedRow["Age"] = chicken.Age;
            selectedRow["EggsPerMonth"] = chicken.EggsPerMonth;
            selectedRow["BreedId"] = chicken.BreedId.HasValue ? (object)chicken.BreedId.Value : DBNull.Value;
            selectedRow["CageId"] = chicken.CageId.HasValue ? (object)chicken.CageId.Value : DBNull.Value;
        }

        public void UpdateChicken(Chicken chicken)
        {
            var table = GetTable(Constants.Tables.Chicken);
            DataRow selectedRow = table.Select($"ChickenId = {chicken.ChickenId}")?.FirstOrDefault();
            if (selectedRow != null)
            {
                selectedRow["Weight"] = chicken.Weight;
                selectedRow["Age"] = chicken.Age;
                selectedRow["EggsPerMonth"] = chicken.EggsPerMonth;
                selectedRow["BreedId"] = chicken.BreedId.HasValue ? (object)chicken.BreedId.Value : DBNull.Value;
                selectedRow["CageId"] = chicken.CageId.HasValue ? (object)chicken.CageId.Value : DBNull.Value;
            }
        }

        public void DeleteChicken(Chicken chicken)
        {
            var table = GetTable(Constants.Tables.Chicken);
            DataRow selectedRow = table.Select($"ChickenId = {chicken.ChickenId}")?.FirstOrDefault();
            if (selectedRow != null) selectedRow.Delete();
        }
        #endregion

        #region ОБЩЕУПОТРЕБИТЕЛЬНЫЕ МЕТОДЫ УНИВЕРСАЛЬНОГО UI
        public DataRow CreateNewRow(string tableName)
        {
            DataTable table = GetTable(tableName);
            DataRow row = table.NewRow();
            return row;
        }

        public void AddNewRow(string tableName, DataRow row)
        {
            DataTable table = GetTable(tableName);
            table.Rows.Add(row);
        }

        public void DeleteRow(DataRow selectedRow)
        {
            selectedRow.Delete();
        }
        #endregion
    }
}