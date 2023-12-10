using Azure.Core;
using Domain.Data;
using Domain.Entities;
using Domain.Models;
using Domain.Models.BookingsBusinessModel;
using Microsoft.EntityFrameworkCore; 
using Repositories.IRepository;

namespace Repositories.Repository
{
    public class AdminstrationRepository : IAdminRepository
    {
        private readonly VezeetaDbContext _Context;

        public AdminstrationRepository(VezeetaDbContext context)
        {
            _Context = context;
        }

        public int GetNumberOfDoctors()
        {
            return _Context.Doctors.Count();
        }
        public int GetNumberOfPatients()
        {
            return _Context.Patients.Count();
        }
        public RequestsModel GetNumOfRequests()
        {
            var totalRequests = _Context.Appointments
                .Count();
            var pendingRequests = _Context.Appointments
                .Count(a => a.StatusAppointment == 1);
            var completedRequests = _Context.Appointments
                .Count(a => a.StatusAppointment == 2);
            var canceledRequests = _Context.Appointments
                .Count(a => a.StatusAppointment == -1);

            var numberOfRequests = new RequestsModel
            {
                TotalRequests = totalRequests,
                PendingRequests = pendingRequests,
                CompletedRequests = completedRequests,
                CanceledRequests = canceledRequests
            };
            return numberOfRequests;
        }

        public IEnumerable<object> GetTop10Doctors()
        {
            var topDoctors = _Context.Doctors
                .OrderByDescending(doctor => doctor.Appointments.Count())
                .Take(10)
                .Select(doctor => new
                {
                    Image = doctor.ImagePath,
                    FullName = doctor.FullName,
                    Specialization = doctor.Specialization,
                    NumberOfRequests = doctor.Appointments.Count()
                })
                .ToList<object>();

            return topDoctors;
        }

        public IEnumerable<object> GetTop5Specializations()
        {
            var topSpecializations = _Context.Appointments
                .GroupBy(a => a.Specialization)
                .OrderByDescending(group => group.Count())
                .Take(5)
                .Select(group => new
                {
                    Specialization = group.Key,
                    AppointmentCount = group.Count()
                })
                .ToList();

            return topSpecializations;

        }

        public bool Delete(int discountID)
        {
            var discount = _Context.Discounts.FirstOrDefault(d => d.DiscountCode == discountID);

            if (discount != null)
            {
                _Context.Discounts.Remove(discount);
                _Context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool Deactivate(int discountID)
        {
            var discount = _Context.Discounts.FirstOrDefault(d => d.DiscountCode == discountID);

            if (discount != null)
            {
                discount.Valid = 0;
                _Context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool Add(AddCouponDiscountModel discountModel)
        {
            var newDiscount = new Discount
            {
                DiscountCode = discountModel.DiscountCode,
                DiscountType = discountModel.DiscountType,
                Price = discountModel.Value
            };

            _Context.Discounts.Add(newDiscount);
            _Context.SaveChanges();

            return true;
        }

        public bool Update(AddCouponDiscountModel discountModel)
        {
            var existingDiscount = _Context.Discounts
                .FirstOrDefault(d => d.DiscountCode == discountModel.DiscountCode);

            if (existingDiscount != null && !
                _Context.Times.Any(t => t.DiscountDoctorCode == existingDiscount.DiscountCode))
            {
                existingDiscount.DiscountCode = discountModel.DiscountCode;
                existingDiscount.DiscountType = discountModel.DiscountType;
                existingDiscount.DiscountType = discountModel.DiscountType;

                _Context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public IEnumerable<object> GetAllPatients(SearchModel searchModel)
        {

            var query = _Context.Patients.AsQueryable();

            if (!string.IsNullOrEmpty(searchModel.Search))
            {
                query = query.Where(d => d.EmailPatient.Contains(searchModel.Search) ||
                                         d.PatientPhone.Contains(searchModel.Search));
            }

            var totalCount = query.Count();

            var patients = query
                .Skip((searchModel.Page - 1) * searchModel.PageSize)
                .Take(searchModel.PageSize)
                .Select(d => new Patient
                {
                    ImagePath = d.ImagePath,
                    FirstName = d.FirstName + d.LastName,
                    PatientPhone = d.PatientPhone,
                    Gender = d.Gender,
                    DateOfBirth = d.DateOfBirth
                })
                .ToList();

            return patients;
        }


        public IEnumerable<object> GetPatientByID(int patientID)
        {
            var patient = _Context.Patients
              .Where(p => p.PatientId == patientID)
              .Select(p => new Patient
              {
                  ImagePath = p.ImagePath,
                  FirstName = $"{p.FirstName} {p.LastName}",
                  EmailPatient = p.EmailPatient,
                  PatientPhone = p.PatientPhone,
                  Gender = p.Gender,
                  DateOfBirth = p.DateOfBirth
              })
              .FirstOrDefault();

            if (patient != null)
            {
                var patientDetails = _Context.Patients.Where(p => p.PatientId == patientID)
                    .Join(
                        _Context.Appointments.Include(a => a.Times),
                        patient => patient.EmailPatient,
                        appointment => appointment.EmailPatient,
                        (patient, appointment) => new
                        {
                            Patient = patient,
                            Appointment = appointment
                        }
                    ).Select(result => new Appointment
                    {
                        patient = result.Patient,
                        Times = result.Appointment.Times
                    }).ToList();

                return patientDetails;
            }
            else return null;
        } 
    }
}
