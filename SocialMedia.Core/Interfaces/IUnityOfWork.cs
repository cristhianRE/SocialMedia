using SocialMedia.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace SocialMedia.Domain.Interfaces
{
    public interface IUnityOfWork : IDisposable
    {
        IPostRepository PostRepository { get; }
        IRepository<User> UserRepository { get; }
        IRepository<Comment> CommentRepository { get; }
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
