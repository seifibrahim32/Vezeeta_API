using Domain.Models;
using Domain.Models.BookingsBusinessModel;

namespace Repositories.IRepository
{
    public interface IBookingRepository
    {
        public bool CancelBooking(int BookingId);

        public bool ConfirmCheckup(int BookingId);

        public IEnumerable<object> GetAllBookings(BookingDoctorModel model);

        public bool Add(AddBookingModel model);

        public bool Update(EditBookingModel model);

        public bool Delete(DeleteBookingModel model);
    }
}
