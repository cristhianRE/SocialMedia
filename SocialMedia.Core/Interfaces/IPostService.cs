using SocialMedia.Domain.CustomEnities;
using SocialMedia.Domain.Entities;
using SocialMedia.Domain.ModelFilters;
using System.Threading.Tasks;

namespace SocialMedia.Domain.Interfaces
{
    public interface IPostService
    {
        PagedList<Post> GetPosts(PostQueryFilter filter);
        Task<Post> GetPost(int id);
        Task InsertPost(Post post);
        Task<bool> UpdatePost(Post post);
        Task<bool> DeletePost(int id);
    }
}