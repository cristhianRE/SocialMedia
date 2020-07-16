using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SocialMedia.Api.Response;
using SocialMedia.Domain.CustomEnities;
using SocialMedia.Domain.DTOs;
using SocialMedia.Domain.Entities;
using SocialMedia.Domain.Interfaces;
using SocialMedia.Domain.ModelFilters;
using SocialMedia.Infrastructure.Interfaces;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _service;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public PostController(IPostService service, IMapper mapper, IUriService uriService)
        {
            _service = service;
            _mapper = mapper;
            _uriService = uriService;
        }

        [HttpGet(Name = nameof(GetPost))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult GetPosts([FromQuery]PostQueryFilter filter)
        {
            var posts = _service.GetPosts(filter);
            var postsDto = _mapper.Map<IEnumerable<PostDto>>(posts);

            var metadata = new Metadata
            {
                TotalCount = posts.TotalCount,
                PageSize = posts.PageSize,
                CurrentPage = posts.CurrentPage,
                TotalPages = posts.TotalPages,
                HasNextPage = posts.HasNextPage,
                HasPreviousPage = posts.HasPreviousPage,
                NextPageUrl = _uriService.GetPostPaginationUri(filter, Url.RouteUrl(nameof(GetPost))).ToString(),
                PreviousPageUrl = _uriService.GetPostPaginationUri(filter, Url.RouteUrl(nameof(GetPost))).ToString()
            };

            var response = new ApiResponse<IEnumerable<PostDto>>(postsDto)
            {
                Meta = metadata
            };

            //optional
            Response.Headers.Add("x-pagination", JsonConvert.SerializeObject(metadata));
            //

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost([FromRoute] int id)
        {
            var post = await _service.GetPost(id);
            var postDto = _mapper.Map<PostDto>(post);
            var response = new ApiResponse<PostDto>(postDto);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostDto postDto)
        {
            var post = _mapper.Map<Post>(postDto);
            await _service.InsertPost(post);
            postDto = _mapper.Map<PostDto>(post);
            var response = new ApiResponse<PostDto>(postDto);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] PostDto postDto)
        {
            var post = _mapper.Map<Post>(postDto);
            post.Id = id;
            var result = await _service.UpdatePost(post);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await _service.DeletePost(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}
