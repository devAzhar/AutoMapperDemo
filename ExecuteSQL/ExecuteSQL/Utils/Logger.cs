namespace ExecuteSQL.Utils
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web;

    public static class Logger
    {
        public static void Init()
        {
            LogId = DateTime.Now.ToLongDateString().Replace(",", "") + " " + DateTime.Now.ToLongTimeString().Replace(":", "");
        }

        private static string LogId { get; set; }

        public static void Log(string message, string log = "")
        {
            if (log != "1" || string.IsNullOrEmpty(message))
            {
                return;
            }

            try
            {
                message = message + "\r\n";

                if (string.IsNullOrEmpty(LogId))
                {
                    Init();
                }

                var filePath = HttpContext.Current.Request.PhysicalApplicationPath;
                filePath = Path.Combine(filePath, "logs");

                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }

                filePath = Path.Combine(filePath, LogId + ".txt");

                File.AppendAllText(filePath, message);
            }
            catch (Exception exp)
            {
            }
        }
    }
}