namespace Domain.Weathers;

public class Precipitation
{
    public bool WillItRain { get; private set; }
    public int ChanceOfRain { get; private set; }
    public bool WillItSnow { get; private set; }
    public int ChanceOfSnow { get; private set; }

    public Precipitation(bool willItRain, int chanceOfRain, bool willItSnow, int chanceOfSnow)
    {
        WillItRain = willItRain;
        ChanceOfRain = chanceOfRain;
        WillItSnow = willItSnow;
        ChanceOfSnow = chanceOfSnow;
    }
}
