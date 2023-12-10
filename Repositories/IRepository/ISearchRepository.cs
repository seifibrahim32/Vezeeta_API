using Domain.Models;
using Domain.Models.Response;

namespace Repositories.IRepository
{
    public interface ISearchRepository
    {
        public IQueryable GetAllAppointments(SearchModel searchModel);

        public bool GetDoctorsBookings(PatientBookingModel patientBookingModel);

        public IQueryable GetAllBookings();


    }
}
