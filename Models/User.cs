using System;
using System.Collections.Generic;

namespace MedistockApp.Models;

public partial class User
{
    public int IdUser { get; set; }

    public string DocumentType { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string Birthdate { get; set; } = null!;

    public int Age { get; set; }

    public string Gender { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Profession { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int FkIdRole { get; set; }

    public virtual Role FkIdRoleNavigation { get; set; } = null!;

    public virtual ICollection<Scheduling> SchedulingFkIdDoctorNavigations { get; set; } = new List<Scheduling>();

    public virtual ICollection<Scheduling> SchedulingFkIdPatientNavigations { get; set; } = new List<Scheduling>();
}

