using System;
using System.ComponentModel.DataAnnotations;

namespace DataTransfertObject
{
    public class LoginSession
    {
        [Key]
        public int LoginSessionId { get; set; }
        public string UserName { get; set; }
        public bool ConnectionState { get; set; }
        public DateTime ConnectionDate { get; set; }
    }
}
