using System;
using System.Collections.Generic;

namespace Sistema_Tec_Web_API.Models;

public partial class UserStudent
{
    public required int id { get; set; }

    public required bool isExemptFromPrintingCosts { get; set; }

    public required int degreeId { get; set; }

    public required string degreeName { get; set; }

}