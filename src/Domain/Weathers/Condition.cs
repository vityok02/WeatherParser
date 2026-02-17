namespace Domain.Weathers;

public class Condition
{
    public string Text { get; private set; }
    public string IconLink { get; private set; }

    public Condition(string text, string iconLink)
    {
        Text = text;
        IconLink = iconLink;
    }
}
