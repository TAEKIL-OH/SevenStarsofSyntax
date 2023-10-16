using System.Text.Json;
using System.Text.Json.Serialization;


namespace ContosoCrafts.WebSite.Models
{
    public class Paw
    {
        public string Name { get; set; }
        public string Breed { get; set; }
        public string Gender { get; set; }
        public string Age { get; set; }
        public string Size { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }

    public class Owner
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }
        public string Email { get; set; }
    }
    public class PawModel
    {
        public string Id { get; set; }
        public Paw Paw { get; set; }
        public Owner Owner { get; set; }

        public override string ToString() => JsonSerializer.Serialize<PawModel>(this);


    }
}
