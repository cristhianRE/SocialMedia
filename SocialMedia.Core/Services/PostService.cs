using Microsoft.Extensions.Options;
using SocialMedia.Domain.CustomEnities;
using SocialMedia.Domain.Entities;
using SocialMedia.Domain.Exceptions;
using SocialMedia.Domain.Interfaces;
using SocialMedia.Domain.ModelFilters;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Domain.Services
{
    public class PostService : IPostService
    {
        private readonly IUnityOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public PostService(IUnityOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
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
            var exisitngPost = await _unitOfWork.PostRepository.GetById(post.Id);
            exisitngPost.Image = post.Image;
            exisitngPost.Description = post.Description;
            _unitOfWork.PostRepository.Update(exisitngPost);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public PagedList<Post> GetPosts(PostQueryFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

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

            var pagedList = PagedList<Post>.Create(posts, filter.PageNumber, filter.PageSize);

            return pagedList;
        }

        public async Task<Post> GetPost(int id)
        {
            return await _unitOfWork.PostRepository.GetById(id);
        }

        public async Task<bool> DeletePost(int id)
        {
            await _unitOfWork.PostRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
