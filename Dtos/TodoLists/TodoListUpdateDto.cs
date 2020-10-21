using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APICore.Dtos.TodoLists
{
    public class TodoListUpdateDto
    {
        [Required]
        [MaxLength(50)]
        public string title { get; set; }

        [Required]
        [MaxLength(1000)]
        public string content { get; set; }

        [Required]
        [MaxLength(50)]
        public string listType { get; set; }

        [DataType(DataType.Date)]
        public DateTime updateDateTime { get; set; } = DateTime.Now;
    }
}
