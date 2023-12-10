using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositories.IRepository; 

namespace Services.Controllers
{
    [Route("admin/dashboard")]
    [ApiController]
    public class AdminDashboardController : ControllerBase
    {
        private readonly IAdminRepository _AdminRepository;

        public AdminDashboardController(IAdminRepository adminRepository)
        {
            _AdminRepository = adminRepository;
        }

        [HttpGet, Route("NumOfDoctors")]
        [Authorize(Roles = "ADMIN")]
        public IActionResult GetNumberOfDoctors()
        {
            return Ok(_AdminRepository.GetNumberOfDoctors());
        }

        [HttpGet, Route("NumOfPatients")]
        [Authorize(Roles = "ADMIN")]
        public IActionResult GetNumberOfPatients()
        {
            return Ok(_AdminRepository.GetNumberOfDoctors());
        }

        [HttpGet, Route("NumOfRequests")]
        [Authorize(Roles = "ADMIN")]
        public IActionResult GetNumOfRequests()
        {
            return Ok(_AdminRepository.GetNumOfRequests());
        }

        [HttpGet, Route("Top10Specializations")]
        [Authorize(Roles = "ADMIN")]
        public IActionResult GetTop10Specializations()
        {
            return Ok(_AdminRepository.GetTop5Specializations());
        }

        [HttpGet, Route("Top10Doctors")]
        [Authorize(Roles = "ADMIN")]
        public IActionResult GetTop10Doctors()
        {
            return Ok(_AdminRepository.GetTop10Doctors());
        }
    }
}
