
namespace ExecuteSQL.Utils
{
    using System.Configuration;

    public static class ApplicationSettings
    {

        #region "AppSettings"
        public static string AuthorizedUsername
        {
            get
            {
                return ConfigurationManager.AppSettings["AuthorizedUsername"];
            }
        }

        public static string AuthorizedPassword
        {
            get
            {
                return ConfigurationManager.AppSettings["AuthorizedPassword"];
            }
        }
        #endregion

        #region "ConnectionString"
        public static string GetConnectionSring(string serverName = "", string databaseName = "", string username = "", string password = "")
        {
            var connectionString = string.Empty;

            if (!string.IsNullOrEmpty(serverName))
            {
                connectionString = string.Format("User id={0};Password={1};Data Source={2};Initial Catalog={3};", username, password, serverName, databaseName);
            }
            else
            {
                connectionString = ApplicationSettings.ConnectionString;
            }

            return connectionString;
        }

        public static string ConnectionString
        {
            get
            {
                var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnectionString"]?.ConnectionString;

                    return connectionString;
            }
        }
        #endregion
    }
}