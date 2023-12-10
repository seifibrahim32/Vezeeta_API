using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.IRepository;

namespace Services.Controllers
{
    [Route("admin/patients")]
    [ApiController]
    public class PatientAdmistrationController : ControllerBase
    {
        private readonly IAdminRepository _AdminRepository;
        public PatientAdmistrationController(IAdminRepository adminRepository)
        {
            _AdminRepository = adminRepository;
        }

        [HttpGet, Route("GetAll")]

        public IActionResult GetAll(SearchModel searchModel)
        {
            if (searchModel == null)
            {
                return BadRequest("Invalid search parameters");
            }

            var doctors = _AdminRepository.GetAllPatients(searchModel);

            if (doctors != null)
            {
                return Ok(doctors);
            }

            return NotFound();
        }
        [HttpGet, Route("GetByID")]

        public IActionResult GetById(int patientID)
        {
            if (patientID == null)
            {
                return BadRequest("Invalid search parameters");
            }

            var doctors = _AdminRepository.GetPatientByID(patientID);

            if (doctors != null)
            {
                return Ok(doctors);
            }

            return NotFound();
        }

    }
}
