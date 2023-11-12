namespace ContosoCrafts.WebSite.Models
{
    public class MeetupModel
    {
        public string Id { get; set; } = System.Guid.NewGuid().ToString();
        public string PawToMeet { get; set; }
        public string DateOfMeetup { get; set; }
        public string LocationOfMeetup { get; set; }
        public string SpecialMessage { get; set; }
    }
}
