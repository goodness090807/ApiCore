using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APICore.Dtos.UserInfos
{
    public class UserInfoCreateDto
    {
        [Key]
        public string userId { get; set; }

        [Required]
        public string userAccount { get; set; }

        [Required]
        public string userPassword { get; set; }

        public string userRoles { get; set; }

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
