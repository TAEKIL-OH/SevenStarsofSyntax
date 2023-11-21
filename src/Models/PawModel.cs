using ContosoCrafts.WebSite.Enums;
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
        //Getter and Setter of Name
        public string Name { get; set; }

        //Getter and Setter of Breed
        public string Breed { get; set; }

        //Getter and Setter of Gender
        public GenderTypeEnum Gender { get; set; }

        ////Getter and Setter of Age
        public double Age { get; set; }

        //Getter and Setter of Size
        public string Size { get; set; }

        //Getter and Setter of Description
        public string Description { get; set; }

        //Getter and Setter of Image
        public string Image { get; set; }

        //Getter and Setter of Feedback
        public string[] Feedback { get; set; }
    }

    //Owner Class
    public class Owner
    {
        //Getter and Setter of Owner Name
        public string Name { get; set; }

        //Getter and Setter of Owner Address
        public string Address { get; set; }

        //Getter and Setter of Owner Phone
        public string Phone { get; set; }

        //Getter and Setter of Owner City
        public string City { get; set; }

        //Getter and Setter of Owner Zipcode
        public string Zipcode { get; set; }

        //Getter and Setter of Owner Email
        public string Email { get; set; }
    }

}