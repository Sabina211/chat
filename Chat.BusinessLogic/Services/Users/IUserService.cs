using Chat.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.BusinessLogic.Services.Users
{
    public interface IUserService
    {
        public Task<long> Create(UserEntity user);
        public List<UserEntity> GetAll();
        public Task<UserEntity> GetById(long id);
        public Task<UserEntity> Update(UserEntity user);
        public Task Delete(long id);
    }
}
