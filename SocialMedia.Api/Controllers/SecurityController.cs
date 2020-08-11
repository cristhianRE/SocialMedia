using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Api.Response;
using SocialMedia.Domain.DTOs;
using SocialMedia.Domain.Entities;
using SocialMedia.Domain.Enums;
using SocialMedia.Domain.Interfaces;
using SocialMedia.Infrastructure.Interfaces;
using System.Threading.Tasks;

namespace SocialMedia.Api.Controllers
{
    [Authorize(Roles = nameof(RoleType.Administrator))]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private readonly ISecurityService _service;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;

        public SecurityController(ISecurityService service,
                                  IMapper mapper, 
                                  IPasswordService passwordService)
        {
            _service = service;
            _mapper = mapper;
            _passwordService = passwordService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SecurityDto securityDto)
        {
            var security = _mapper.Map<Security>(securityDto);
            security.Password = _passwordService.Hash(security.Password);
            await _service.RegisterUser(security);
            securityDto = _mapper.Map<SecurityDto>(security);
            var response = new ApiResponse<SecurityDto>(securityDto);
            return Ok(response);
        }
    }
}
