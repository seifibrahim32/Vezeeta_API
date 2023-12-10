
namespace Domain.Models.BookingsBusinessModel
{
    public class AddBookingModel
    {
        public int DoctorID { get; set; }
        public int Price { get; set; } 
        public List<DayScheduleModel> Days { get; set; }
    }

    public class DayScheduleModel
    {
        public int Day { get; set; }
        public DateTime Date { get; set; }
        public List<TimeSlotModel> TimeSlots { get; set; }
    }

    public class TimeSlotModel
    { 
        public DateTime appointmentTime { get; set; }
    }
}
