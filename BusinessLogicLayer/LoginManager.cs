using DataLayer;
using DataTransfertObject;
using System;
using System.Linq;

namespace BusinessLogicLayer
{
    public class LoginManager
    {
        public static bool ConnectionState { get; private set; }
        public static string Username { get; private set; }

        public static LoginSession TryToConnect(string username, string password)
        {
            User newUserIdentification = null;
            LoginSession loginSession = null;

            var dbContext = new StockContext();
            newUserIdentification = dbContext.Users.Where(c => c.Username == username && c.Password == password).FirstOrDefault();

            if (newUserIdentification != null)
            {
                var loginSessions = dbContext.LoginSessions;

                loginSession = new LoginSession()
                {
                    UserName = Username = newUserIdentification.Username,
                    ConnectionState = ConnectionState = true,
                    ConnectionDate = DateTime.Now
                };
                loginSessions.Add(loginSession);
                dbContext.SaveChanges();
                return loginSession;
            }
            else
                return null;
        }
    }
}
