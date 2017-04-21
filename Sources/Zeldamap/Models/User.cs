using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace zeldassistant.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string PasswordHash { get; set; }

        public bool IsInitialized { get; set; }
    }
}
