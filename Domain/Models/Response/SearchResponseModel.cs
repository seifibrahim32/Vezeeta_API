
using Domain.Entities;

namespace Domain.Models.Response
{
    public class SearchResponseModel
    {
        public string Image { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Specialization { get; set; }

        List<Appointment> Appointments { get; set; } = new List<Appointment>();

    }
}
