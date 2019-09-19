namespace ExecuteSQL.Attributes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    using System.Web.Http.Controllers;
    using Utils;

    public class BasicAuthorizeAttribute : AuthorizeAttribute
    {
        #region "Private Members"
        private enum AuthType { basic, cookie };
        #endregion

        #region "Private Methods"
        private string DecodeFrom64(string encodedData)
        {

            byte[] encodedDataAsBytes
                = System.Convert.FromBase64String(encodedData);
            string returnValue =
               System.Text.Encoding.ASCII.GetString(encodedDataAsBytes);

            return returnValue;
        }

        private bool GetUserNameAndPassword(HttpActionContext actionContext, out string username, out string password, out AuthType authType)
        {
            authType = AuthType.basic;
            bool gotIt = false;
            username = string.Empty;
            password = string.Empty;
            IEnumerable<string> headerVals;

            if (actionContext.Request.Headers.TryGetValues("Authorization", out headerVals))
            {
                try
                {
                    string authHeader = headerVals.FirstOrDefault();
                    char[] delims = { ' ' };
                    string[] authHeaderTokens = authHeader.Split(new char[] { ' ' });
                    if (authHeaderTokens[0].Contains("Basic"))
                    {
                        string decodedStr = DecodeFrom64(authHeaderTokens[1]);
                        string[] unpw = decodedStr.Split(new char[] { ':' });
                        username = unpw[0];
                        password = unpw[1];
                    }
                    else
                    {
                        if (authHeaderTokens.Length > 1)
                            username = DecodeFrom64(authHeaderTokens[1]);
                        authType = AuthType.cookie;
                    }

                    gotIt = true;
                }
                catch { gotIt = false; }
            }

            return gotIt;
        }
        #endregion

        #region "AuthorizeAttribute Methods"
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            if (GetUserNameAndPassword(actionContext, out string username, out string password, out AuthType authType))
            {
                return ApplicationSettings.AuthorizedUsername.Equals(username, StringComparison.OrdinalIgnoreCase) && ApplicationSettings.AuthorizedPassword.Equals(password, StringComparison.Ordinal);
            }

            return false;
        }
        #endregion
    }
}