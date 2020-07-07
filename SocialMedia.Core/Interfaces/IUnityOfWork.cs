using SocialMedia.Model.Entities;
using System;
using System.Threading.Tasks;

namespace SocialMedia.Model.Interfaces
{
    public interface IUnityOfWork : IDisposable
    {
        IRepository<Post> PostRepository { get; }
        IRepository<User> UserRepository { get; }
        IRepository<Comment> CommentRepository { get; }
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
