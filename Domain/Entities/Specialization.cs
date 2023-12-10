using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Specialization
{
    public DateOnly? CreatedBy { get; set; }

    public string SpecializationName { get; set; } = null!;

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
}
