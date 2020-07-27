using Microsoft.Extensions.Options;
using SocialMedia.Domain.CustomEnities;
using SocialMedia.Domain.Entities;
using SocialMedia.Domain.Interfaces;
using System.Threading.Tasks;

namespace SocialMedia.Domain.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly IUnityOfWork _unitOfWork;

        public SecurityService(IUnityOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Security> GetLoginByCredentials(UserLogin userLogin)
        {
            return await _unitOfWork.SecurityRepository.GetLoginByCredential(userLogin);
        }

        public async Task RegisterUser(Security security)
        {
            await _unitOfWork.SecurityRepository.Insert(security);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
