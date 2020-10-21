using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICore.Models
{
    public class UsersLists
    {
        public string userId { get; set; }

        public string listId { get; set; }

        public UserInfo UserInfo { get; set; }

        public TodoList TodoList { get; set; }
    }
}
