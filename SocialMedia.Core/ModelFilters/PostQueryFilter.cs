using System;

namespace SocialMedia.Model.ModelFilters
{
    public class PostQueryFilter
    {
        //refer to the params to filter a post(3 or +, recomended to create an object)
        public int? UserId { get; set; }
        public DateTime? Date { get; set; }
        public string Description { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
