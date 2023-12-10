using Domain.Data;
using Domain.Entities;
using Domain.Models;
using Domain.Models.BookingsBusinessModel;
using Microsoft.EntityFrameworkCore;
using Repositories.IRepository;

namespace Repositories.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly VezeetaDbContext _Context;

        public BookingRepository(VezeetaDbContext context)
        {
            _Context = context;
        }
        public bool CancelBooking(int BookingId)
        {
            var appointmentToCancel = _Context.Appointments
                .FirstOrDefault(a => a.BookingId == BookingId);

            if (appointmentToCancel != null)
            {
                appointmentToCancel.StatusAppointment = -1;
                _Context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool ConfirmCheckup(int BookingId)
        {
            var appointmentToConfirm = _Context.Appointments
                .FirstOrDefault(a => a.BookingId == BookingId);

            if (appointmentToConfirm != null)
            {

                appointmentToConfirm.StatusAppointment = 1;

                _Context.SaveChanges();

                return true;
            }

            return false;
        }

        public IEnumerable<object> GetAllBookings(BookingDoctorModel model)
        {
             
            var doctor = _Context.Doctors.FirstOrDefault(d => d.DoctorId == model.DoctorId);
            if (doctor == null)
            {
                return new List<string>(0);
            }
             
            IQueryable<Appointment> query = _Context.Appointments
                .Include(a=> a.patient)
                .Where(a => a.EmailDoctor == doctor.EmailDoctor);
             
            if (model.Date.HasValue)
            {
                query = query.Where(a => a.AppointmentDate == model.Date.Value.Date);
            }
             
            query = query.OrderBy(a => a.AppointmentDay)
                .Skip((model.PageNumber - 1) * model.PageSize)
                .Take(model.PageSize);
             
            var result = query.Select(a => new
            {
                PatientEmail = $"{a.EmailPatient}",
                Image = a.EmailPatient,
                Age = CalculateAge(a.patient.DateOfBirth),
                Gender = a.patient.Gender,
                Phone = a.patient.PatientPhone,
                Email = a.patient.EmailPatient, 
                Appointment = a
            });

            return result.ToList();
             
        }

        private int CalculateAge(DateTime birthDate)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - birthDate.Year;
            if (birthDate.Date > today.AddYears(-age))
            {
                age--;
            }
            return age;
        }

        public bool Add(AddBookingModel model)
        { 

            var doctor = _Context.Doctors.FirstOrDefault(d => d.DoctorId == model.DoctorID);

            if (doctor == null)
            { 
                return false;
            }
              

            foreach (var day in model.Days)
            {
                var newAppointment = new Appointment
                {
                    EmailPatient = doctor.EmailDoctor,
                    Price = model.Price,
                    AppointmentDate = day.Date,
                    EmailDoctor = doctor.EmailDoctor,
                    Phone = doctor.Phone,
                    Specialization = doctor.Specialization,
                    AppointmentDay = day.Day
                };
                _Context.Appointments.Add(newAppointment);

                foreach(var time in day.TimeSlots)
                {
                    var newTime = new Time
                    {
                        TimesId = newAppointment.BookingId, 
                        Price = model.Price,
                        AppointmentTime = time.appointmentTime
                    };
                    _Context.Times.Add(newTime);

                } 

            }  
             
            _Context.SaveChanges();

            return true;

        }
        public bool Update(EditBookingModel model)
        { 
            var existingTimeSlot = _Context.Appointments
                .FirstOrDefault(a => a.BookingId == model.BookingId);

            if (existingTimeSlot == null)
            { 
                return false;
            }
             
            if (!_Context.Appointments.Any(t => t.StatusAppointment == 1))
            { 
                existingTimeSlot.AppointmentDate = model.NewDate; 
                 
                _Context.SaveChanges();
                 
                return true;
            }
            else
            {
                 return false;
            }
        }

        public bool Delete(DeleteBookingModel model)
        {
            var existingTimeSlot = _Context.Appointments
                .FirstOrDefault(a => a.BookingId == model.AppointmentId);

            if (existingTimeSlot == null)
            {
                return false;
            }
             
            if (!_Context.Appointments.Any(t => t.StatusAppointment == 1))
            {
                existingTimeSlot.AppointmentDate = model.DeletedTime;

                _Context.SaveChanges();
                 
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
