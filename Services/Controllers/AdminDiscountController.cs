using Domain.Models.BookingsBusinessModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositories.IRepository;

namespace Services.Controllers
{
    [ApiController]
    [Route("admin/discounts")]
    public class AdminDiscountController: ControllerBase
    {
        private readonly IAdminRepository _AdminRepository;

        public AdminDiscountController(IAdminRepository adminRepository)
        {
            _AdminRepository = adminRepository;
        }

        [HttpPatch, Route("Deactivate")]
        [Authorize(Roles = "ADMIN")]
        public IActionResult Deactivate([FromBody] int DiscountId)
        {
            var response = _AdminRepository.Deactivate(DiscountId);
            return Ok(new
            {
                DiscountId = DiscountId,
                Message = response == true ? "The Deactivation is successful" :
                "Not successful"
            });
        }

        [HttpDelete, Route("Delete")]
        [Authorize(Roles = "ADMIN")]
        public IActionResult Delete([FromBody] int DiscountId)
        {
            var response = _AdminRepository.Delete(DiscountId);
            return Ok(new
            {
                DiscountId = DiscountId,
                Message = response == true ? "The deletion is successful" :
                "The deletion is Not successful"
            });
        }

        [HttpPost, Route("Add")]
        [Authorize(Roles = "ADMIN")]
        public IActionResult AddDiscount([FromBody] AddCouponDiscountModel discountModel)
        {
            var response = _AdminRepository.Add(discountModel);
            return Ok(new {
                DiscountCode = discountModel.DiscountCode, 
                Message = response == true ? "The addition is successful" :
                "The addition is Not successful"
            });
        }

        [HttpPatch, Route("Update")]
        [Authorize(Roles = "ADMIN")]
        public IActionResult UpdateDiscount([FromBody] AddCouponDiscountModel discountModel)
        {
            var response = _AdminRepository.Update(discountModel);
            return Ok(new
            {
                DiscountCode = discountModel.DiscountCode,
                Message = response == true ? "The addition is successful" :
                "The addition is Not successful"
            });
        }
    }
}
