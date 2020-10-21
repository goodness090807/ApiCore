using APICore.Datas;
using APICore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICore.Repositorys
{
    public class UserInfoRepository
    {
        private readonly CoreDbContext _dbContext;

        public UserInfoRepository(CoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<UserInfo> GetUserInfos()
        {
            return _dbContext.UserInfos.ToList();
        }

        public List<TodoList> GetTodoListsByUserId(string userId)
        {
            return _dbContext.UserInfos.Include(x => x.TodoLists).Where(x => x.userId == userId).FirstOrDefault().TodoLists;
        }

        public UserInfo GetUserInfoById(string userId)
        {
            return _dbContext.UserInfos.FirstOrDefault(x => x.userId == userId);
        }

        public UserInfo GetUserInfoBySignIn(string userAccount, string userPassword)
        {
            return _dbContext.UserInfos.FirstOrDefault(x => x.userAccount == userAccount && x.userPassword == userPassword);
        }

        public void CreateUser(UserInfo userInfo)
        {
            if(userInfo == null)
            {
                throw new ArgumentNullException();
            }

            _dbContext.Add(userInfo);

            _dbContext.SaveChanges();
        }

        public void UpdateUser()
        {
            _dbContext.SaveChanges();
        }

        public void DeleteUser(UserInfo userInfo)
        {
            if (userInfo == null)
            {
                throw new ArgumentNullException();
            }

            _dbContext.UserInfos.Remove(userInfo);

            _dbContext.SaveChanges();
        }
    }
}
