﻿using SocialMedia.Infrastructure.Data;
using SocialMedia.Model.Entities;
using SocialMedia.Model.Interfaces;
using System.Threading.Tasks;

namespace SocialMedia.Infrastructure.Repositories
{
    public class UnityOfWork : IUnityOfWork
    {
        private readonly SocialMediaContext _context;
        private readonly IRepository<Post> _postRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Comment> _commentRepository;

        public UnityOfWork(SocialMediaContext context)
        {
            _context = context;
        }

        public IRepository<Post> PostRepository => _postRepository ?? new Repository<Post>(_context);

        public IRepository<User> UserRepository => _userRepository ?? new Repository<User>(_context);

        public IRepository<Comment> CommentRepository => _commentRepository ?? new Repository<Comment>(_context);

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }
    }
}
