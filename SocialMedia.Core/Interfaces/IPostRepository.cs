using SocialMedia.Model.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMedia.Model.Interfaces
{
    public interface IPostRepository : IRepository<Post>
    {
        Task<IEnumerable<Post>> GetPostsByUser(int UserId);
    }
}
