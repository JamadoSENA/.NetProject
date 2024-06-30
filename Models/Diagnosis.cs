using System;
using System.Collections.Generic;

namespace MedistockApp.Models;

public partial class Diagnosis
{
    public int IdDiagnosis { get; set; }

    public string Description { get; set; } = null!;

    public int FkIdAppointment { get; set; }

    public virtual Appoinment FkIdAppointmentNavigation { get; set; } = null!;

    public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
}
