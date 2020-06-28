using System.ComponentModel.DataAnnotations;

namespace DataTransfertObject
{
    public class User
    {
        [Key]
        public string Username { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        //public override string ToString() => $"{Surname} {Name}";
    }
}