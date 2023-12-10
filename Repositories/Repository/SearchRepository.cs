using Domain.Data;
using Domain.Models;
using Domain.Models.Response;
using Microsoft.EntityFrameworkCore;
using Repositories.IRepository;
using System.Linq;
using System.Numerics;

namespace Repositories.Repository
{
    public class SearchRepository : ISearchRepository
    {
        private readonly VezeetaDbContext _Context;
        public SearchRepository(VezeetaDbContext context)
        {
            _Context = context;
        }
         
        public IQueryable GetAllAppointments(SearchModel searchModel)
        { 
            var query = _Context.Doctors
                .Where(doctor => doctor.EmailDoctor == searchModel.Search)
                .Include(d => d.Appointments)
                .ThenInclude(a => a.Times)
                .Skip(searchModel.PageSize * (searchModel.Page - 1))
                .Take(searchModel.PageSize);
              
            return query;
        }

        public bool GetDoctorsBookings(PatientBookingModel patientBookingModel)
        {
            int number;
            if (patientBookingModel.DiscountCode != null)
            {
                number = _Context.Times.Where(time => time.TimesId
                            == patientBookingModel.TimeID &&
                            time.DiscountDoctorCode == patientBookingModel.DiscountCode).Count();
            }
            else
            {
                number = _Context.Times.Where(time => time.TimesId
                        == patientBookingModel.TimeID).Count();
            }

            return number >= 1 ? true : false;
        }
        
        public IQueryable GetAllBookings()
        {
            var query = from appointment in _Context.Appointments
                         join doctor in _Context.Doctors on appointment.EmailDoctor equals doctor.EmailDoctor
                         join specialization in _Context.Specializations on appointment.Specialization equals specialization.SpecializationName
                         join time in _Context.Times on appointment.BookingId equals time.TimesId
                         join discount in _Context.Discounts on time.DiscountDoctorCode equals discount.DiscountCode into discountGroup
                         from discount in discountGroup.DefaultIfEmpty()
                         select new
                         {
                             image = doctor.ImagePath,
                             doctorName = doctor.FullName,
                             specialize = specialization.SpecializationName,
                             day = appointment.AppointmentDay,
                             time = time.AppointmentTime,
                             price = appointment.Price,
                             discountCode = discount.DiscountCode,
                             finalPrice = appointment.Price - (discount.Price ?? 0), 
                             status = appointment.StatusAppointment
                         }; 
            return query;
        }

        public async Task SaveChanges()
        {
            await _Context.SaveChangesAsync();
        }
    }
}
