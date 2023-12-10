using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Patient
{
    public int PatientId { get; set; }

    public string EmailPatient { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? ImagePath { get; set; }

    public string? PatientPhone { get; set; }

    public DateTime DateOfBirth { get; set; }

    public int? Gender { get; set; }
}
