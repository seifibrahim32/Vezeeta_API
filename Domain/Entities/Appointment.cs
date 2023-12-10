
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Appointment
{ 
    public int BookingId { get; set; }

    public string EmailDoctor { get; set; } = null!;

    public int? Price { get; set; }

    public int? StatusAppointment { get; set; }

    public DateTime? AppointmentDate { get; set; }

    public string? EmailPatient { get; set; }

    public string Phone { get; set; } = null!;

    public string? Specialization { get; set; } 

    public int? AppointmentDay { get; set; }

    public Patient? patient { get; set; }

    public virtual ICollection<Time> Times { get; set; } = new List<Time>();
     

}
