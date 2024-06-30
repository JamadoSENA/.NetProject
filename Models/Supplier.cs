using System;
using System.Collections.Generic;

namespace MedistockApp.Models;

public partial class Supplier
{
    public int IdSupplier { get; set; }

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;
}
