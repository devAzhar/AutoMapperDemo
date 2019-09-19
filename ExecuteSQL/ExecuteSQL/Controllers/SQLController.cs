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
        public DataTable SelectDataTable(string sql)
        {
            return DatabaseHandler.GetDataTable(sql);
        }

        [HttpGet]
        [HttpPost]
        [OperationContract]
        public DataSet SelectDataSet(string sql)
        {
            return DatabaseHandler.GetDataSet(sql);
        }

        [HttpGet]
        [HttpPost]
        [OperationContract]
        public ResultModel Execute(string sql)
        {
            var resultCode = 0;
            var resultMsg = string.Empty;

            try
            {
                using (var connection = new SqlConnection(ApplicationSettings.ConnectionString))
                {
                    using (var command = new SqlCommand(sql, connection))
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
            // return this.Content<ResultModel>(HttpStatusCode.OK, model);
        }
    }
}