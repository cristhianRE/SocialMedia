using SocialMedia.Model.CustomEnities;
using SocialMedia.Model.Entities;
using SocialMedia.Model.ModelFilters;
using System.Threading.Tasks;

namespace SocialMedia.Model.Interfaces
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