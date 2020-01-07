using System.Text.Json.Serialization;

namespace SantoAndreOnBus.Models
{
    public class InterestPoint
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LineId { get; set; }

        [JsonIgnore]
        public virtual Line Line { get; set; }
    }
}