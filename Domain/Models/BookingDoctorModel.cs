using Newtonsoft.Json;

namespace Domain.Models
{
    public class BookingDoctorModel
    {
        public int DoctorId { get; set; }
        public DateTime? Date { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
