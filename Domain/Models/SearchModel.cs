namespace Domain.Models
{
    public class SearchModel
    {
        public int Page { get; set; }

        public int PageSize { get; set; }

        public string? Search { get; set; }
    }
}
