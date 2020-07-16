using System;

namespace SocialMedia.Domain.ModelFilters
{
    public class PostQueryFilter
    {
        public int? UserId { get; set; }
        public DateTime? Date { get; set; }
        public string Description { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
