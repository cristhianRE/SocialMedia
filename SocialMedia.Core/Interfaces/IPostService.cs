using SocialMedia.Model.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMedia.Model.Interfaces
{
    public interface IPostService
    {
        Task<IEnumerable<Post>> GetPosts();
        Task<Post> GetPost(int id);
        Task InsertPost(Post post);
        Task<bool> UpdatePost(Post post);
        Task<bool> DeletePost(int id);
    }
}