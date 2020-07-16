using SocialMedia.Domain.ModelFilters;
using SocialMedia.Infrastructure.Interfaces;
using System;

namespace SocialMedia.Infrastructure.Services
{
    public class UriService : IUriService
    {
        // Serviço Responsavel por gerar Urls

        private readonly string _baseUri;

        public UriService(string baseuri)
        {
            _baseUri = baseuri;
        }

        public Uri GetPostPaginationUri(PostQueryFilter filter, string actionUrl)
        {
            string baseUrl = $"{_baseUri}{actionUrl}";
            return new Uri(baseUrl);
        }
    }
}
