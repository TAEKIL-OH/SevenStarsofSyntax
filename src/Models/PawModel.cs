using ContosoCrafts.WebSite.Enums;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ContosoCrafts.WebSite.Models
{
    /// <summary>
    /// ProductModel Class
    /// </summary>
    //Paw Class
    public class PawModel
    {
        //Getter and Setter of Name with a validation
        [Required]
        public string Name { get; set; }

        //Getter and Setter of Breed with a validation
        [Required]
        public string Breed { get; set; }

        //Getter and Setter of Gender with a enum validation
        [EnumDataType(typeof(GenderTypeEnum))]
        public GenderTypeEnum Gender { get; set; }

        //Getter and Setter of Age with a validation
        [Required]
        public double Age { get; set; }

        //Getter and Setter of Size with a validation
        [Required]
        public string Size { get; set; }

        //Getter and Setter of Description with a validation
        [MaxLength(255)]
        public string Description { get; set; }

        //Getter and Setter of Image with a validation
        [Required]
        public string Image { get; set; }

        //Getter and Setter of Feedback with a validation
        [MaxLength(255)]
        public string[] Feedback { get; set; }
    }

    //Owner Class
    public class Owner
    {
        //Getter and Setter of Owner Name with a validation
        [Required]
        public string Name { get; set; }

        //Getter and Setter of Owner Address with a validation
        [MaxLength(255)]
        public string Address { get; set; }

        //Getter and Setter of Owner Phone with a validation
        [Required]
        public string Phone { get; set; }

        //Getter and Setter of Owner City with a validation
        [Required]
        public string City { get; set; }

        //Getter and Setter of Owner Zipcode with a validation
        [Required]
        public string Zipcode { get; set; }

        //Getter and Setter of Owner Email with a validation
        [Required]
        public string Email { get; set; }
    }

}