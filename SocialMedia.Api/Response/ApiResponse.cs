using SocialMedia.Domain.CustomEnities;

namespace SocialMedia.Api.Response
{
    public class ApiResponse<T>
    {
        public T Data { get; set; }
        public Metadata Meta { get; set; }

        public ApiResponse(T data)
        {
            Data = data;
        }
        // Standard response to http requests
    }
}
