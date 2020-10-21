using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APICore.Models
{
    public class TodoList
    {
        [Key]
        public string listId { get; set; }

        [Required]
        [MaxLength(50)]
        public string title { get; set; }

        [Required]
        [MaxLength(1000)]
        public string content { get; set; }

        [Required]
        [MaxLength(50)]
        public string listType { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime createDateTime { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime updateDateTime { get; set; }
    }
}
