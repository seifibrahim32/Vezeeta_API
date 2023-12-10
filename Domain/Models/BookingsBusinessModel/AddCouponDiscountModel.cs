using Domain.Enums; 

namespace Domain.Models.BookingsBusinessModel
{
    public class AddCouponDiscountModel
    {
        public int DiscountCode { get; set; }
        public int DiscountType { get; set; }
        public int Value { get; set; }
    }

}
