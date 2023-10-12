using System;
using System.Collections.Generic;

namespace Sistema_Tec_Web_API.Models;

public partial class UserDepartment
{
    public int id { get; set; }

    public string departmentName { get; set; }

    public int campusId { get; set; }

    public string campusName { get; set; }

}

