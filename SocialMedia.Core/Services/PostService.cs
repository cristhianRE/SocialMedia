using SocialMedia.Model.Entities;
using SocialMedia.Model.Exceptions;
using SocialMedia.Model.Interfaces;
using SocialMedia.Model.ModelFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Model.Services
{
    public class PostService : IPostService
    {
        private readonly IUnityOfWork _unitOfWork;

        public PostService(IUnityOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task InsertPost(Post post)
        {
            var user = await _unitOfWork.UserRepository.GetById(post.UserId);
            if (user == null)
            {
                throw new DomainException("User does't exist.");
            }

            var userPost = await _unitOfWork.PostRepository.GetPostsByUser(post.UserId);
            if (userPost.Count() < 10)
            {
                var lastPost = userPost.OrderByDescending(x => x.Date).FirstOrDefault();
                if ((DateTime.Now - lastPost.Date).TotalDays < 7)
                {
                    throw new DomainException("You are not able to publish any post.");
                }
            }

            if (post.Description.Contains("Sex"))
            {
                throw new DomainException("Content not allowed.");
            }

            await _unitOfWork.PostRepository.Insert(post);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdatePost(Post post)
        {
            _unitOfWork.PostRepository.Update(post);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public IEnumerable<Post> GetPosts(PostQueryFilter filter)
        {
            var posts = _unitOfWork.PostRepository.GetAll();

            if (filter.UserId != null)
            {
                posts = posts.Where(x => x.UserId == filter.UserId);
            }
            if (filter.Date != null)
            {
                posts = posts.Where(x => x.Date.ToShortDateString() == filter.Date?.ToShortDateString());
            }
            if (filter.Description != null)
            {
                posts = posts.Where(x => x.Description.ToLower().Contains(filter.Description.ToLower()));
            }

            return posts;
        }

        public async Task<Post> GetPost(int id)
        {
            return await _unitOfWork.PostRepository.GetById(id);
        }

        public async Task<bool> DeletePost(int id)
        {
            await _unitOfWork.PostRepository.Delete(id);
            return true;
        }
    }
}
