namespace ExecuteSQL.Utils
{
    using System.Data;
    using System.Data.SqlClient;

    public static class DatabaseHandler
    {
        #region "Public Methods"
        public static DataTable GetDataTable(string sql)
        {
            DataTable dataTable = null;

            using (var dataSet = GetDataSet(sql))
            {
                if (dataSet.Tables.Count > 0)
                {
                    dataTable = dataSet.Tables[0];
                }
            }

            return dataTable;
        }

        public static DataSet GetDataSet(string sql)
        {
            using (var connection = new SqlConnection(ApplicationSettings.ConnectionString))
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