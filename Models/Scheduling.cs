using System;
using System.Collections.Generic;

namespace MedistockApp.Models;

public partial class Scheduling
{
    public int IdScheduling { get; set; }

    public string Reason { get; set; } = null!;

    public string State { get; set; } = null!;

    public int FkIdPatient { get; set; }

    public int FkIdDoctor { get; set; }

    public virtual ICollection<Appoinment> Appoinments { get; set; } = new List<Appoinment>();

    public virtual User FkIdDoctorNavigation { get; set; } = null!;

    public virtual User FkIdPatientNavigation { get; set; } = null!;
}
