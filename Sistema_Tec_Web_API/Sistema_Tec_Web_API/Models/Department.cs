﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Sistema_Tec_Web_API.Models;

public partial class Department
{
    public int id { get; set; }

    public string departmentName { get; set; }

    public int campusId { get; set; }

    public virtual Campus campus { get; set; }

    public virtual ICollection<Person> emails { get; set; } = new List<Person>();
}