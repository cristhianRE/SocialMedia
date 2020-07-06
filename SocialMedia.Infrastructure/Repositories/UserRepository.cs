using Microsoft.EntityFrameworkCore;
using SocialMedia.Infrastructure.Data;
using SocialMedia.Model.Entities;
using SocialMedia.Model.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMedia.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SocialMediaContext _context;

        public UserRepository(SocialMediaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await _context.Users.ToArrayAsync();
            return users;
        }

        public async Task<User> GetUser(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(p => p.UserId == id);
            return user;
        }
    }
}
