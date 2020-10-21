using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APICore.Dtos.Account
{
    public class AccountSignInDto
    {
        [Required]
        public string userAccount { get; set; }

        [Required]
        public string userPassword { get; set; }
    }
}
