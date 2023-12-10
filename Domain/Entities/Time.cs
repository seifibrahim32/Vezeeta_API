using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Time
{
    public int? TimesId { get; set; }

    public int DiscountId { get; set; }

    public int? DiscountDoctorCode { get; set; }

    public int? Price { get; set; }

    public DateTime AppointmentTime { get; set; }

    public virtual Discount? DiscountDoctorCodeNavigation { get; set; }
}
