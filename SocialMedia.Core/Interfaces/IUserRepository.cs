using SocialMedia.Model.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMedia.Model.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUser(int id);
    }
}
