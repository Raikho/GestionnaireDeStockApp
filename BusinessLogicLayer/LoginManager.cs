using DataLayer;
using DataTransfertObject;
using System;
using System.Linq;

namespace BusinessLogicLayer
{
    public class LoginManager
    {
        public static LoginSession _loginSession = new LoginSession();

        public static LoginSession TryToConnect(string username, string password)
        {
            User newUserIdentification = null;
            var dbContext = new StockContext();
            newUserIdentification = dbContext.Users.Where(c => c.Username == username && c.Password == password).FirstOrDefault();
            if (newUserIdentification != null)
            {
                var loginSessions = dbContext.LoginSessions;
                _loginSession = new LoginSession()
                {
                    UserName = newUserIdentification.Username,
                    ConnectionState = true,
                    ConnectionDate = DateTime.Now
                };
                loginSessions.Add(_loginSession);
                dbContext.SaveChanges();
                return _loginSession;
            }
            else
                return null;
        }
    }
}
