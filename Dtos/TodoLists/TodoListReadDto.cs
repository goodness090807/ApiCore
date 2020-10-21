using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICore.Dtos.TodoLists
{
    public class TodoListReadDto
    {
        public string listId { get; set; }

        public string title { get; set; }

        public string content { get; set; }

        public string listType { get; set; }

        public DateTime createDateTime { get; set; }

        public DateTime updateDateTime { get; set; }
    }
}
