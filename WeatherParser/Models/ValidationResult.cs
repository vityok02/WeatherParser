using System.Text;

namespace WeatherParser.Models;

public class ValidationResult
{
    public List<string> Errors { get; set; } = [];
    public bool IsValid => Errors.Count == 0;

    public override string ToString()
    {
        var builder = new StringBuilder();

        foreach (var error in Errors)
        {
            builder.AppendLine(error);
        }

        return builder.ToString();
    }
}
