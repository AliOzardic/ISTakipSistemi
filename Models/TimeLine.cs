namespace IsTakipSistemi.Models
{
    public class Timeline
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public ICollection<TimelineEvent>? Events { get; set; }
        public Timeline()
        {
                
        }
    }

}
