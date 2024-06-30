using System;
using System.Collections.Generic;

namespace MedistockApp.Models;

public partial class Appoinment
{
    public int IdAppointment { get; set; }

    public string DateHour { get; set; } = null!;

    public int FkIdScheduling { get; set; }

    public virtual ICollection<Diagnosis> Diagnoses { get; set; } = new List<Diagnosis>();

    public virtual Scheduling FkIdSchedulingNavigation { get; set; } = null!;
}
