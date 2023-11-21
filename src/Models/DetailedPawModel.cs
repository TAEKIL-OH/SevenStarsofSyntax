using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ContosoCrafts.WebSite.Models
{
    /// <summary>
    /// Meetup Model Class
    /// </summary>
    public class DetailedPawModel
    {
        //Getter and Setter of Paw Id
        public string Id { get; set; }

        //Getter and Setter of Paw Class
        public PawModel Paw { get; set; }

        //Getter and Setter of Owner Class
        public Owner Owner { get; set; }

        //Getter and Setter of MeetupModel
        public List<MeetupModel> BookingLists { get; set; } = new List<MeetupModel>();

        //Serializes PawModel Model to a String
        public override string ToString() => JsonSerializer.Serialize<DetailedPawModel>(this);
    }
}
