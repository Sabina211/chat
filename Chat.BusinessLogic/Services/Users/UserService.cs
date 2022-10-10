using Chat.DataAccess;
using Chat.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Chat.BusinessLogic.Services.Users
{
    public class UserService : IUserService
    {
        private readonly ChatDbContext _context;

        public UserService(ChatDbContext context)
        {
            _context = context;
        }

        public async Task<long> Create(UserEntity user)
        {
            var result = await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return result.Entity.Id;
        }

        public async Task Delete(long id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x=>x.Id == id);
            if (user == null) throw new Exception("Entity not found exception");
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public List<UserEntity> GetAll()
        {
            var users = _context.Users.ToList();
            return users;
        }

        public async Task<UserEntity> GetById(long id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x=>x.Id==id);
            if (user == null) throw new Exception("Entity not found exception");
            return user;

        }

        public async Task<UserEntity> Update(UserEntity user)
        {
            var result = _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return result.Entity;
        }
    }
}
