using System.Text.Json.Serialization;

namespace SantoAndreOnBus.Models
{
    public class LineVehicle
    {
        public int Id { get; set; }
        public int LineId { get; set; }
        public int VehicleId { get; set; }

        [JsonIgnore]
        public virtual Line Line { get; set; }
        public virtual Vehicle Vehicle { get; set; }
    }
}