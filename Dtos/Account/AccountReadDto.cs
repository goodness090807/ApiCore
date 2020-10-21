using APICore.Dtos.UserInfos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICore.Dtos.Account
{
    public class AccountReadDto
    {
        public UserInfoReadDto userInfo { get; set; }

        public string AccessToken { get; set; }
    }
}
