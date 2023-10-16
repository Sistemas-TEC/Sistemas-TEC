using System;
using System.Collections.Generic;

namespace Sistema_Tec_Web_API.Models
{
    public class LoginBody
    {
        public string email { get; set; }

        public string? password { get; set; }

        public string? newPassword { get; set; }
        public string? oldPassword { get; set; }
    }
}
