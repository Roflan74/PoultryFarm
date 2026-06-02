using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;

namespace PoultryFarm
{
    public class DatabaseManager
    {
        private readonly Dictionary<Type, SqlDbType> _mapTypes =
            new Dictionary<Type, SqlDbType>
            {
                { typeof(int), SqlDbType.Int },
                { typeof(string), SqlDbType.NVarChar },
                { typeof(bool), SqlDbType.Bit },
                { typeof(long), SqlDbType.BigInt },
                { typeof(float), SqlDbType.Real },
                { typeof(double), SqlDbType.Float },
                { typeof(TimeSpan), SqlDbType.Time },
                { typeof(decimal), SqlDbType.Decimal },
                { typeof(DateTime), SqlDbType.Date }
            };

        private readonly string _connectionString;
        private readonly SqlConnection _connection;
        private readonly SqlDataAdapter _adapter;
        private readonly DataSet _dataSet;

        public DatabaseManager(string connectionString)
        {
            _connectionString = connectionString;
            _connection = new SqlConnection(_connectionString);
            _dataSet = new DataSet();
            _adapter = new SqlDataAdapter();
        }

        /// <summary>
        /// Универсальное определение имени первичного ключа на основе имени таблицы
        /// </summary>
        private string GetIdColumnName(string tableName)
        {
            if (tableName.Equals(Constants.Tables.WorkerCageAssignment, StringComparison.OrdinalIgnoreCase))
                return "AssignmentId";

            if (tableName.EndsWith("s", StringComparison.OrdinalIgnoreCase))
                return tableName.Substring(0, tableName.Length - 1) + "Id";

            return tableName + "Id";
        }

        public void LoadTable(string tableName)
        {
            if (_dataSet.Tables.Contains(tableName))
            {
                _dataSet.Tables.Remove(tableName);
            }

            string queryString = $"select * from dbo.[{tableName}]";

            try
            {
                _connection.Open();
                using (var command = new SqlCommand(queryString, _connection))
                {
                    _adapter.SelectCommand = command;
                    var table = new DataTable(tableName);
                    _adapter.Fill(table);
                    _dataSet.Tables.Add(table);
                }
            }
            finally
            {
                if (_connection.State == ConnectionState.Open)
                    _connection.Close();
            }
        }

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
            string idColumnName = GetIdColumnName(table.TableName);
            var p = new SqlParameter()
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = $"@{idColumnName}",
                SourceColumn = idColumnName
            };
            if (isIdOutput)
                p.Direction = ParameterDirection.Output;
            command.Parameters.Add(p);
        }

        private void CreateParameters(DataTable table, SqlCommand command)
        {
            var columns = table.Columns.Cast<DataColumn>().ToList();
            string idColumnName = GetIdColumnName(table.TableName);

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
            string idColumn = GetIdColumnName(tableName);
            string sqlString = $"update dbo.[{tableName}] set ";

            var columns = table.Columns.Cast<DataColumn>().ToList();
            List<string> columnNames = columns.Select(c => c.ColumnName).ToList();
            columnNames.Remove(idColumn);

            string sqlAssign = string.Join(", ", columnNames.Select(name => $"[{name}] = @{name}"));
            return sqlString + sqlAssign + $" where [{idColumn}] = @{idColumn}";
        }

        private string GenerateInsertSqlString(DataTable table)
        {
            string tableName = table.TableName;
            string idColumn = GetIdColumnName(tableName);
            var columns = table.Columns.Cast<DataColumn>().ToList();
            List<string> columnNames = columns.Select(c => c.ColumnName).ToList();
            columnNames.Remove(idColumn);

            string sqlColumnsInBrackets = "(" + string.Join(", ", columnNames.Select(n => $"[{n}]")) + ") ";
            string sqlParametersInBrackets = "(@" + string.Join(", @", columnNames) + ") ";

            return $"insert into dbo.[{tableName}] " + sqlColumnsInBrackets +
                   " values " + sqlParametersInBrackets + "; " +
                   $"select @{idColumn} = SCOPE_Identity()";
        }

        private string GenerateDeleteSqlString(DataTable table)
        {
            string tableName = table.TableName;
            string idColumn = GetIdColumnName(tableName);
            return $"delete from dbo.[{tableName}] where [{idColumn}] = @{idColumn}";
        }

        public bool UpdateDatabase(string tableName)
        {
            bool isSuccess = false;
            try
            {
                _connection.Open();
                var table = GetTable(tableName);

                var commandUpdate = new SqlCommand(GenerateUpdateSqlString(table), _connection);
                CreateParameters(table, commandUpdate);
                CreateIdParameter(commandUpdate, table, isIdOutput: false);
                _adapter.UpdateCommand = commandUpdate;

                var commandInsert = new SqlCommand(GenerateInsertSqlString(table), _connection);
                CreateParameters(table, commandInsert);
                CreateIdParameter(commandInsert, table, isIdOutput: true);
                _adapter.InsertCommand = commandInsert;

                var commandDelete = new SqlCommand(GenerateDeleteSqlString(table), _connection);
                CreateIdParameter(commandDelete, table, isIdOutput: false);
                _adapter.DeleteCommand = commandDelete;

                _adapter.Update(table);
                isSuccess = true;
            }
            finally
            {
                if (_connection.State == ConnectionState.Open)
                    _connection.Close();
            }
            return isSuccess;
        }

        /// <summary>
        /// Выполняет произвольный SQL-запрос (SELECT) и возвращает результат в виде DataTable
        /// </summary>
        public DataTable ExecuteQuery(string sqlQuery)
        {
            var table = new DataTable();
            try
            {
                _connection.Open();
                using (var command = new SqlCommand(sqlQuery, _connection))
                {
                    using (var adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(table);
                    }
                }
            }
            finally
            {
                if (_connection.State == ConnectionState.Open)
                    _connection.Close();
            }
            return table;
        }

        public DataRow CreateNewRow(string tableName) => GetTable(tableName).NewRow();
        public void AddNewRow(string tableName, DataRow row) => GetTable(tableName).Rows.Add(row);
        public void DeleteRow(DataRow selectedRow) => selectedRow.Delete();
    }
}