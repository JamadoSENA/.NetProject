using System;
using System.Collections.Generic;

namespace MedistockApp.Models;

public partial class Prescription
{
    public int IdPrescription { get; set; }

    public string Medicines { get; set; } = null!;

    public int FkIdDiagnosis { get; set; }

    public virtual Diagnosis FkIdDiagnosisNavigation { get; set; } = null!;

    public virtual ICollection<Medicinesprescription> Medicinesprescriptions { get; set; } = new List<Medicinesprescription>();
}
