﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Sistema_Tec_Web_API.Models
{
    public partial class Employee
    {
        public int id { get; set; }
        public string email { get; set; }
        public bool isProfessor { get; set; }

        public virtual Person emailNavigation { get; set; }
    }
}