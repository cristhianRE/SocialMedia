using SocialMedia.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMedia.Domain.Interfaces
{
    public interface IPostRepository : IRepository<Post>
    {
        Task<IEnumerable<Post>> GetPostsByUser(int UserId);
    }
}
