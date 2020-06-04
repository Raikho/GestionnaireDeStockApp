using DataLayer;
using System;
using System.Linq;

namespace TestEntityFramework
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var dbContext = new StockContext())
            {
                var users = dbContext.Users;

                /*var newUser = new User()
                {
                    Username = "Raiko",
                    Name = "Hammana",
                    Surname = "Charif",
                    Password = "456"
                };

                users.Add(newUser);
                dbContext.SaveChanges();*/

                foreach (var user in users)
                {
                    Console.WriteLine($"Utilisateur: {user.Username}");
                }

                var utilisateurAvecADantLeurPrenom = users.Where(c => c.Surname.Contains("c"));
                foreach (var u in utilisateurAvecADantLeurPrenom)
                {
                    Console.WriteLine(u);
                }
            }
        }
    }
}
