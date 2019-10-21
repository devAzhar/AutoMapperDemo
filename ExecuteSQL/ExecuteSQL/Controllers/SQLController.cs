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
    using System.Web;

    [BasicAuthorize]
    public class SQLController : ApiController
    {
        public SQLController()
        {
            Logger.Init();
        }

        [HttpGet]
        [HttpPost]
        [OperationContract]
        public DataTable SelectDataTable(string sql, string serverName = "", string databaseName = "", string username = "", string password = "", string log = "")
        {
            return DatabaseHandler.GetDataTable(sql, ApplicationSettings.GetConnectionSring(serverName, databaseName, username, password), log);
        }

        [HttpGet]
        [HttpPost]
        [OperationContract]
        public DataSet SelectDataSet(string sql, string serverName = "", string databaseName = "", string username = "", string password = "", string log = "")
        {
            return DatabaseHandler.GetDataSet(sql, ApplicationSettings.GetConnectionSring(serverName, databaseName, username, password), log);
        }

        [HttpGet]
        [HttpPost]
        [OperationContract]
        public ResultModel Execute(string sql, string serverName = "", string databaseName = "", string username = "", string password = "", string log = "", bool useTransaction = false)
        {
            Logger.Log(sql, log);

            var resultCode = 0;
            var resultMsg = string.Empty;

            try
            {
                using (var connection = new SqlConnection(ApplicationSettings.GetConnectionSring(serverName, databaseName, username, password)))
                {
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = CommandType.Text;
                        connection.Open();

                        SqlTransaction transaction = null;

                        try
                        {
                            if (useTransaction)
                            {
                                transaction = connection.BeginTransaction();
                            }

                            resultCode = command.ExecuteNonQuery();

                            if (useTransaction)
                            {
                                transaction.Commit();
                            }
                        }
                        catch (Exception e)
                        {
                            if (useTransaction)
                            {
                                transaction.Rollback();
                            }

                            throw e;
                        }

                        connection.Close();
                    }
                }
            }
            catch (Exception exp)
            {
                resultMsg = exp.Message;
                Logger.Log(resultMsg, log);
                Logger.Log(exp.StackTrace, log);
            }

            var model = new ResultModel() { ResultCode = resultCode.ToString(), ResultMessage = resultMsg, Response = string.Empty };
            
            return model;
        }
    }
}