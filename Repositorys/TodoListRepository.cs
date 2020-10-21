using APICore.Datas;
using APICore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICore.Repositorys
{
    public class TodoListRepository
    {
        private readonly CoreDbContext _dbContext;

        public TodoListRepository(CoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<TodoList> GetTodoLists()
        {
            return _dbContext.TodoLists.ToList();
        }

        public TodoList GetTodoListById(string listId)
        {
            return _dbContext.TodoLists.FirstOrDefault(x => x.listId == listId);
        }

        public string GetUserIdByTodoListId(string listId)
        {
            return _dbContext.UserInfos.FirstOrDefault(x => x.TodoLists.FirstOrDefault(y => y.listId == listId).listId == listId).userId;
        }

        public void CreateTodoList(string userId, TodoList todoList)
        {
            if (todoList == null || string.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException();
            }

            UserInfo userInfo = _dbContext.UserInfos.FirstOrDefault(x => x.userId == userId);

            if (userInfo == null)
            {
                throw new ArgumentNullException();
            }

            List<TodoList> todoLists = new List<TodoList>()
            {
                todoList
            };

            userInfo.TodoLists = todoLists;

            _dbContext.SaveChanges();
        }
        public void UpdateTodoList()
        {
            _dbContext.SaveChanges();
        }

        public void DeleteTodoList(TodoList todoList)
        {
            if (todoList == null)
            {
                throw new ArgumentNullException();
            }

            _dbContext.TodoLists.Remove(todoList);

            _dbContext.SaveChanges();
        }
    }
}
