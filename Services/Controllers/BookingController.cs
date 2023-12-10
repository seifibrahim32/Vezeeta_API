using Domain.Models;
using Domain.Models.BookingsBusinessModel;
using Domain.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Repositories.IRepository;

namespace Services.Controllers
{
    [Route("booking")]
    [ApiController]
    public class BookingController : ControllerBase
    { 
        private readonly IBookingRepository _BookingRepository;

        public BookingController(IBookingRepository bookingRespository)
        {
            _BookingRepository = bookingRespository;
        }
          
        [HttpPost, Route("Cancel")]

        public IActionResult CancelBooking([FromQuery] int bookingID)
        {
            var data = _BookingRepository.CancelBooking(bookingID);

            var response = new BookingResponseModel
            {
                StatusCode = 200,
                Message = data == true ? $"${bookingID} is cancelled" :
                        $"{bookingID} isn't available"
            };
            return Ok(response);
        }

        [HttpPost, Route("ConfirmCheckUp")]
        public IActionResult ConfirmCheckUp([FromQuery] int bookingId)
        {
            bool data = _BookingRepository.ConfirmCheckup(bookingId);

            var response = new BookingResponseModel
            {
                StatusCode = 200,
                Message = data == true ? $"${bookingId} is confirmed" :
                        $"{bookingId} isn't available"
            };

            return Ok(response);
        }

        [HttpGet, Route("GetAll")]
        public IActionResult GetAllDoctorById([FromBody] BookingDoctorModel dto)
        {
            var response = _BookingRepository.GetAllBookings(dto);


            return Ok(response);
        }

        [HttpPost, Route("Add")]
        public IActionResult AddBooking([FromBody] AddBookingModel dto)
        {
            var response = _BookingRepository.Add(dto);


            return Ok(response);
        }

        [HttpPatch, Route("Edit")]
        public IActionResult EditBooking([FromBody] EditBookingModel dto)
        {
            var response = _BookingRepository.Update(dto);


            return Ok(response);
        }

        [HttpDelete, Route("Delete")]
        public IActionResult DeleteBooking([FromBody] DeleteBookingModel dto)
        {
            var response = _BookingRepository.Delete(dto);


            return Ok(response);
        }
    }
}
