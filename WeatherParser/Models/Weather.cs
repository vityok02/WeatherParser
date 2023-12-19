using System.Text;

namespace WeatherParser.Models;

public class Weather
{
    public int? CurrentTemperature { get; set; }
    public int? MinTemperature { get; set; }
    public int? MaxTemperature { get; set; }
    public DateTime? ObservationTime { get; set; } = default!;
    public string? Location { get; set; } = default!;

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"Current temperature: {CurrentTemperature.ToString()}");
        sb.AppendLine($"Minimal temperature: {MinTemperature.ToString()}");
        sb.AppendLine($"Maximum temperature: {MaxTemperature.ToString()}");
        sb.AppendLine($"Time: {ObservationTime.ToString()}");
        sb.AppendLine($"Location: {Location}");

        return sb.ToString();
    }
}
