using System.ComponentModel.DataAnnotations;


namespace ContosoCrafts.WebSite.Models
{
    /// <summary>
    /// Meetup Model Class
    /// </summary>
    public class MeetupModel
    {
        //Getter and Setter of Id 
        public string Id { get; set; } = System.Guid.NewGuid().ToString();

        //Getter and Setter of Paw To Meet with a validation
        [Required] 
        public string PawToMeet { get; set; }

        //Getter and Setter of Date of meetup with a validation
        [Required]
        public string DateOfMeetup { get; set; }

        //Getter and Setter of Location of meetup with a validation
        [Required]
        public string LocationOfMeetup { get; set; }

        //Getter and Setter of Special Message with a validation
        [MaxLength(255)]
        public string SpecialMessage { get; set; }
    }
}
