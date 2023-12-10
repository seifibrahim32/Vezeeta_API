using Domain.Models;
using Domain.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositories.IRepository;
using Repositories.Repository;

namespace Services.Controllers
{
    [Route("search")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private ISearchRepository _SearchRepository;

        public SearchController(ISearchRepository searchRespository)
        {
            _SearchRepository = searchRespository;
        }

        [HttpGet, Route("GetAll")]

        public IActionResult GetAllAppointments([FromQuery] SearchModel searchModel)
        {
            var response = _SearchRepository.GetAllAppointments(searchModel);

            return Ok(response);
        }


        [HttpGet, Route("Booking")]

        public IActionResult SearchTimeBooking([FromQuery] PatientBookingModel bookingModel)
        {
            var response = _SearchRepository.GetDoctorsBookings(bookingModel);

            return Ok(response);
        }

        [HttpGet, Route("GetAllBookings")]

        public IActionResult GetAllBookings()
        {
            var response = _SearchRepository.GetAllBookings();

            return Ok(response);
        }

    }
}
