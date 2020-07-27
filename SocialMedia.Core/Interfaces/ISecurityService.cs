using SocialMedia.Domain.Entities;
using System.Threading.Tasks;

namespace SocialMedia.Domain.Interfaces
{
    public interface ISecurityService
    {
        Task<Security> GetLoginByCredentials(UserLogin userLogin);
        Task RegisterUser(Security security);
    }
}