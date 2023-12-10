using Domain.Models;
using Domain.Models.BookingsBusinessModel;

namespace Repositories.IRepository
{
    public interface IAdminRepository
    {
        public int GetNumberOfDoctors();

        public int GetNumberOfPatients();  

        public RequestsModel GetNumOfRequests();

        public IEnumerable<object> GetTop5Specializations();

        public IEnumerable<object> GetTop10Doctors();



        public IEnumerable<object> GetAllPatients(SearchModel searchModel);

        public IEnumerable<object> GetPatientByID(int patientID);



        public bool Add(AddCouponDiscountModel discountModel);

        public bool Update(AddCouponDiscountModel discountModel);
                  
        public bool Deactivate(int discountID);

        public bool Delete(int discountID);
    }
}
