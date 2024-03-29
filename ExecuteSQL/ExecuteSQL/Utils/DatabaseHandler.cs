﻿namespace ExecuteSQL.Utils
{
    using System.Data;
    using System.Data.SqlClient;

    public static class DatabaseHandler
    {
        #region "Public Methods"
        public static DataTable GetDataTable(string sql, string connectionString, string log = "")
        {
            Logger.Log(sql, log);

            DataTable dataTable = null;

            using (var dataSet = GetDataSet(sql, connectionString, log))
            {
                if (dataSet.Tables.Count > 0)
                {
                    dataTable = dataSet.Tables[0];
                }
            }

            return dataTable;
        }

        public static DataSet GetDataSet(string sql, string connectionString, string log = "")
        {
            Logger.Log(sql, log);

            if (string.IsNullOrEmpty(connectionString))
            {
                connectionString = ApplicationSettings.ConnectionString;
            }

            using (var connection = new SqlConnection(connectionString))
            {
                using (var dataAdapter = new SqlDataAdapter())
                {
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = sql;
                        dataAdapter.SelectCommand = command;
                        var dataSet = new DataSet();

                        connection.Open();
                        dataAdapter.Fill(dataSet);
                        connection.Close();

                        return dataSet;
                    }
                }
            }
        }
        #endregion
    }
}