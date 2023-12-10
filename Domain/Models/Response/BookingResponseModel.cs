using Newtonsoft.Json; 

namespace Domain.Models.Response
{
    public class BookingResponseModel
    {

        [JsonProperty(PropertyName = "status_code")]
        public int StatusCode { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string? Message { get; set; }

        [JsonProperty(PropertyName = "value")]
        public string? Value { get; set; }
    }
}
