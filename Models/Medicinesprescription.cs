using System;
using System.Collections.Generic;

namespace MedistockApp.Models;

public partial class Medicinesprescription
{
    public int Id { get; set; }

    public int Amount { get; set; }

    public int FkIdMedicine { get; set; }

    public int FkIdPrescription { get; set; }

    public virtual Medicine FkIdMedicineNavigation { get; set; } = null!;

    public virtual Prescription FkIdPrescriptionNavigation { get; set; } = null!;
}
