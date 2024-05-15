using System.Text.Json.Serialization;

namespace CorporatePortal.Models
{
    public class Entry
    {
        public int Id { get; set; }

        public DateOnly Date { get; set; }

        public int Hours { get; set; }

        public string? Description { get; set; }

        public int TaskId { get; set; }
        [JsonIgnore]
        public virtual Task Task { get; set; } = null!;
    }
}
