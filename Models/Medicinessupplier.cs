using System;
using System.Collections.Generic;

namespace MedistockApp.Models;

public partial class Medicinessupplier
{
    public int FkIdMedicine { get; set; }

    public int FkIdSupplier { get; set; }

    public virtual Medicine FkIdMedicineNavigation { get; set; } = null!;

    public virtual Supplier FkIdSupplierNavigation { get; set; } = null!;
}
