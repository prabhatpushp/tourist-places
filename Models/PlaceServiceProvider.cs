using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelPlaces.Models;

public partial class PlaceServiceProvider
{
    public int ProviderId { get; set; }
    public int PlaceId { get; set; }

}
