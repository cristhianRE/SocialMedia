using Microsoft.EntityFrameworkCore;
using SocialMedia.Domain.Entities;
using SocialMedia.Domain.Interfaces;
using SocialMedia.Infrastructure.Data;
using System.Threading.Tasks;

namespace SocialMedia.Infrastructure.Repositories
{
    public class SecurityRepository : Repository<Security>, ISecurityRepository
    {
        public SecurityRepository(SocialMediaContext context) : base(context)
        {
        }

        public async Task<Security> GetLoginByCredential(UserLogin login)
        {
            return await _entities.FirstOrDefaultAsync(x => x.User == login.User);
        }
    }
}
