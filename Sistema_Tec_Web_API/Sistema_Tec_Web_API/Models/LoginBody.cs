﻿using System;
using System.Collections.Generic;

namespace Sistema_Tec_Web_API.Models
{
    public class LoginBody
    {
        public string email { get; set; }

        public string? password { get; set; }

        public string? name { get; set; }
        public string? firstLastName { get; set; }
        public string? secondLastName { get; set; }
        public string? id { get; set; }
        public string? departmentId { get; set; }
        public string? schoolId { get; set; }
        public string? degreeId { get; set; }
        public string? studentId { get; set; }
        public string? isExemptFromPrintingCosts { get; set; }

        public string? newPassword { get; set; }
        public string? oldPassword { get; set; }
    }
}
