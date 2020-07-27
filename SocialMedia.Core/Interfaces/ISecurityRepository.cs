using SocialMedia.Domain.Entities;
using System.Threading.Tasks;

namespace SocialMedia.Domain.Interfaces
{
    public interface ISecurityRepository : IRepository<Security>
    {
        Task<Security> GetLoginByCredential(UserLogin login);
    }
}