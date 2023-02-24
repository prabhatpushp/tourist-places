using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelPlaces.Models;

public partial class TouristPlace
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PlaceId { get; set; }

    [Required(ErrorMessage = "Please Enter the Name of Tourist Spot."), MaxLength(50), Display(Name = "Place")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Please Enter the Description of Tourist Spot."), MaxLength(500), Display(Name = "Description")]
    public string Description { get; set; } = null!;

    [Required(ErrorMessage = "Please Enter the City of Tourist Spot."), MaxLength(50)]
    public string City { get; set; } = null!;

    [Required(ErrorMessage = "Please Enter the State of Tourist Spot."), MaxLength(50)]
    public string State { get; set; } = null!;

    [Required(ErrorMessage = "Please Enter the Country of Tourist Spot."), MaxLength(50)]
    public string Country { get; set; } = null!;

    [Display(Name = "Rating")]
    public decimal PlaceRating { get; set; }

    public virtual ICollection<PlaceImage> PlaceImages { get; } = new List<PlaceImage>();

    public virtual ICollection<ServiceProvider> Providers { get; } = new List<ServiceProvider>();
}
