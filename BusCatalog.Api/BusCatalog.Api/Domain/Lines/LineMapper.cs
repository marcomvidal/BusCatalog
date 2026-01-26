using BusCatalog.Api.Domain.Lines.Ports;

namespace BusCatalog.Api.Domain.Lines;

public static class LineMapper
{
    extension(Line line)
    {
        public LineResponse ToLineResponse() =>
            new()
            {
                Id = line.Id,
                Identification = line.Identification,
                Fromwards = line.Fromwards,
                Towards = line.Towards,
                DeparturesPerDay = line.DeparturesPerDay,
                Vehicles = line.Vehicles.Select(x => x.Identification)
            };
    }

    extension(LinePostRequest request)
    {
        public Line ToLine() =>
            new()
            {
                Identification = request.Identification!,
                Fromwards = request.Fromwards!,
                Towards = request.Towards!,
                DeparturesPerDay = request.DeparturesPerDay!.Value
            };
    }
    
    extension(LinePutRequest request)
    {
        public Line MergeWithSavedLine(Line line)
        {
            line.Identification = request.Identification ?? line.Identification;
            line.Fromwards = request.Fromwards ?? line.Fromwards;
            line.Towards = request.Towards ?? line.Towards;
            line.DeparturesPerDay = request.DeparturesPerDay ?? line.DeparturesPerDay;

            return line;
        }
    }
}
