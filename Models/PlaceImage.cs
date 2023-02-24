using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelPlaces.Models;

public partial class PlaceImage
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ImageId { get; set; }

    [Required(ErrorMessage = "Place is a required field.")]
    public int PlaceId { get; set; }

    [Required(ErrorMessage = "Please enter the URL of image. This is a Required Field."), MaxLength(450)]
    public string ImageUrl { get; set; } = null!;

    public virtual TouristPlace Place { get; set; } = null!;
}
