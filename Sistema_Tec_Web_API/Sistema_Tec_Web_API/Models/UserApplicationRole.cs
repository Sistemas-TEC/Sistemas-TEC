using System;
using System.Collections.Generic;

namespace Sistema_Tec_Web_API.Models;

public partial class UserApplicationRole
{
    public required int id { get; set; }

    public required int applicationId { get; set; }

    public required string applicationRoleName { get; set; }

    public required string applicationName { get; set; }

}
