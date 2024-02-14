using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain;

public class Weather
{
    public int? CurrentTemperature { get; set; }
    public int? MinTemperature { get; set; }
    public int? MaxTemperature { get; set; }
    [DataType(DataType.Time)]
    public DateTime? ObservationTime { get; set; } = default!;
    public string? Location { get; set; } = default!;

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Temperature:");
        sb.AppendLine($"{GetEmoji(CurrentTemperature!.Value)}Current: {CurrentTemperature.ToString()}°");
        sb.AppendLine($"{GetEmoji(MinTemperature!.Value)}Minimal: {MinTemperature.ToString()}°");
        sb.AppendLine($"{GetEmoji(MaxTemperature!.Value)}Maximum: {MaxTemperature.ToString()}°");
        sb.AppendLine($"Time: {ObservationTime.ToString()}");
        sb.AppendLine($"Location: {Location}");

        return sb.ToString();
    }

    private string GetEmoji(int temperature)
    {
        return temperature switch
        {
            > 30 => "🟥",
            > 20 => "🟧",
            > 10 => "🟨",
            > 0 => "🟩",
            _ => "🟦",
        };
    }
}
