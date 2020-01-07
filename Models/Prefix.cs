using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace SantoAndreOnBus.Models
{
    public class Prefix
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public int CompanyId { get; set; }

        [JsonIgnore]
        public virtual Company Company { get; set; }

        [JsonIgnore]
        public virtual ICollection<Line> Lines { get; set; }
    }
}