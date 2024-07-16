namespace IsTakipSistemi.Models
{
    public class TimelineEvent
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? EventDate { get; set; }
        public int? TimelineId { get; set; }
        public Timeline? Timeline { get; set; }
        public TimelineEvent()
        {
                
        }
    }

}
