namespace WeatherParser.Models.Interfaces;

public interface IHtmlPathBuilder
{
    string GeneratePath(double latitude, double longitude);
}
