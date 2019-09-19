
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