using SantoAndreOnBus.Models.DTOs;

namespace SantoAndreOnBus.Models.Mappers
{
    public class LineDTOToModel
    {
        public static Line Map(LineDTO dto, Line line)
        {
            line.Letter = dto.Letter;
            line.Number = dto.Number;
            line.Fromwards = dto.Fromwards;
            line.Towards = dto.Towards;
            line.Departures = dto.Departures;
            line.PeakHeadway = dto.PeakHeadway;
            line.Places = dto.Places;
            line.InterestPoints = dto.InterestPoints;

            return line;
        }
    }
}