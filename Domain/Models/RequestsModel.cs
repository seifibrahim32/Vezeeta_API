namespace Domain.Models
{
    public class RequestsModel
    {
        public int? TotalRequests { get; set; }
        public int? PendingRequests { get; set; }
        public int? CompletedRequests { get; set; }
        public int? CanceledRequests { get; set; }
    }
}
