﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Sistema_Tec_Web_API.Models;

public partial class Person
{
    public string email { get; set; }

    public string personPassword { get; set; }

    public int id { get; set; }

    public string personName { get; set; }

    public string firstLastName { get; set; }

    public string secondLastName { get; set; }

    public int debt { get; set; }
    
    public virtual Employee Employee { get; set; }
    
    public virtual Student Student { get; set; }

    public virtual ICollection<ApplicationRole> applicationRoles { get; set; } = new List<ApplicationRole>();

    public virtual ICollection<Department> departments { get; set; } = new List<Department>();

    public virtual ICollection<School> schools { get; set; } = new List<School>();
}