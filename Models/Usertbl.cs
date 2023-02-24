using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelPlaces.Models;

public partial class Usertbl
{
    public String UserName { get; set; }
    public int UserId { get; set; }
    public String Password { get; set; }

}
