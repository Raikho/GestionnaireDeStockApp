using DataLayer;
using DataTransfertObject;
using System;
using System.Linq;

namespace BusinessLogicLayer
{
    public class LoginManager
    {
        private static LoginSession loginSession = new LoginSession();
        public static LoginSession LoginSession { get => loginSession; set => loginSession = value; }

        private LoginManager()
        {
            
        }

        public static LoginSession TryToConnect(string username, string password)
        {
            User newUserIdentification = null;
            var dbContext = new StockContext();
            newUserIdentification = dbContext.Users.Where(c => c.Username == username && c.Password == password).FirstOrDefault();
            if (newUserIdentification != null)
            {
                var loginSessions = dbContext.LoginSessions;
                LoginSession = new LoginSession
                {
                    UserName = newUserIdentification.Username,
                    ConnectionState = true,
                    ConnectionDate = DateTime.Now
                };
                loginSessions.Add(LoginSession);
                dbContext.SaveChanges();
                return LoginSession;
            }
            else
                return null;
        }
    }
}
