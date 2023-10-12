using System;
using System.Collections.Generic;

namespace Sistema_Tec_Web_API.Models;

public partial class UserSchool
{
    public int id { get; set; }

    public string schoolName { get; set; }

    public int campusId { get; set; }

    public string campusName { get; set; }

}
