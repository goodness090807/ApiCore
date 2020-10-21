using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APICore.Dtos.UserInfos
{
    public class UserInfoUpdateDto
    {
        [Required]
        [MaxLength(50)]
        public string userName { get; set; }

        [Required]
        public bool sex { get; set; }

        [Required]
        [EmailAddress]
        public string mail { get; set; }
    }
}
