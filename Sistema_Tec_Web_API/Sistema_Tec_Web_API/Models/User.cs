using System;
using System.Collections.Generic;

namespace Sistema_Tec_Web_API.Models;

public partial class User
{
    public required string email { get; set; }

    public required int id { get; set; }

    public required string personName { get; set; }

    public required string firstLastName { get; set; }

    public required string secondLastName { get; set; }

    public required int debt { get; set; }

    public virtual UserEmployee? employee { get; set; }

    public virtual UserStudent? student { get; set; }

    public virtual ICollection<UserDepartment> departments { get; set; } = new List<UserDepartment>();

    public virtual ICollection<UserSchool> schools { get; set; } = new List<UserSchool>();

    public virtual ICollection<UserApplicationRole> applicationRoles { get; set; } = new List<UserApplicationRole>();

}
