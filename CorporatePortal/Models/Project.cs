using System.Text.Json.Serialization;

namespace CorporatePortal.Models
{
    public class Project
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Code { get; set; } = null!;

        public bool IsActive { get; set; }
        [JsonIgnore]
        public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
    }
}
