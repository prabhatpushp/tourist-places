using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelPlaces.Models;

public partial class ServiceProvider
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ProviderId { get; set; }

    [Required(ErrorMessage = "Please Enter the name of the service provider."), MaxLength(50), Display(Name = "Name")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Please Enter the Email of the service provider."), MaxLength(50), Display(Name = "Email ID")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Please Enter the Phone Number of the service provider."), Display(Name = "Phone Number")]
    public int Phone { get; set; }

    [Required(ErrorMessage = "Please Enter the Type of the service."), MaxLength(15), Display(Name = "Type of Service")]
    public string ProviderType { get; set; } = null!;

    public decimal ProviderRating { get; set; }

    public virtual ICollection<TouristPlace> Places { get; } = new List<TouristPlace>();
}
