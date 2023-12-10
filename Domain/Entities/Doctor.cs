using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Doctor
{
    public int DoctorId { get; set; }

    public string EmailDoctor { get; set; } = null!;

    public string? FullName { get; set; }

    public string Phone { get; set; } = null!;

    public string? Specialization { get; set; }

    public string? ImagePath { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>(); 

    public virtual Specialization? SpecializationNavigation { get; set; }
}
