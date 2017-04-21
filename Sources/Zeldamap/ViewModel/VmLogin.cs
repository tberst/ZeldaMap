using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace zeldassistant.ViewModel
{
    public class VmLogin
    {
        public String Login { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public String Password { get; set; }
        public String ReturnUrl { get; set; }

    
    }
}
