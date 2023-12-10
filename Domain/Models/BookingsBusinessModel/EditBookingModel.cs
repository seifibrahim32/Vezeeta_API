
namespace Domain.Models.BookingsBusinessModel
{
    public class EditBookingModel
    {
        public int DoctorId { get; set; }

        public int BookingId { get; set; }

        public DateTime NewDate { get; set; }
    } 
}
