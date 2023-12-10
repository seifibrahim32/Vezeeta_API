using Newtonsoft.Json; 

namespace Domain.Models.Response
{
    public class SuccessResponseModel
    {

        [JsonProperty(PropertyName = "status_code")]
        public int StatusCode { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "access_token")]
        public string Message { get; set; }
    }
}
