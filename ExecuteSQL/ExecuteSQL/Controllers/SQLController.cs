namespace ExecuteSQL.Controllers
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.ServiceModel;
    using System.Web.Http;
    using Attributes;
    using Models;
    using Utils;

    [BasicAuthorize]
    public class SQLController : ApiController
    {
        [HttpGet]
        [HttpPost]
        [OperationContract]
        public DataTable SelectDataTable(string sql, string serverName = "", string databaseName = "", string username = "", string password = "")
        {
            return DatabaseHandler.GetDataTable(sql, ApplicationSettings.GetConnectionSring(serverName, databaseName, username, password));
        }

        [HttpGet]
        [HttpPost]
        [OperationContract]
        public DataSet SelectDataSet(string sql, string serverName = "", string databaseName = "", string username = "", string password = "")
        {
            return DatabaseHandler.GetDataSet(sql, ApplicationSettings.GetConnectionSring(serverName, databaseName, username, password));
        }

        [HttpGet]
        [HttpPost]
        [OperationContract]
        public ResultModel Execute(string sql, string serverName = "", string databaseName = "", string username = "", string password = "")
        {
            var resultCode = 0;
            var resultMsg = string.Empty;

            try
            {
                using (var connection = new SqlConnection(ApplicationSettings.GetConnectionSring(serverName, databaseName, username, password)))
                {
                    using (var command = new SqlCommand(sql))
                    {
                        connection.Open();
                        resultCode = command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }
            catch (Exception exp)
            {
                resultMsg = exp.Message;
            }

            var model = new ResultModel() { ResultCode = resultCode.ToString(), ResultMessage = resultMsg, Response = string.Empty };
            return model;
        }
    }
}