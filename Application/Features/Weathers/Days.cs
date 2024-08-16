namespace Application.Features.Weathers;

public static class Days
{
    public readonly static Dictionary<string, DateTime> Value = new()
    {
        {"First", DateTime.Now },
        {"Second", DateTime.Now.AddDays(1) },
        {"Third", DateTime.Now.AddDays(2) },
        {"Fourth", DateTime.Now.AddDays(3) },
        {"Fifth", DateTime.Now.AddDays(4) },
        {"Sixth", DateTime.Now.AddDays(5) },
        {"Seventh", DateTime.Now.AddDays(6) },
    };

    //public readonly static KeyValuePair<string, DateTime> Today = new("Today", DateTime.Now);
    //public readonly static KeyValuePair<string, DateTime> Tomorrow = new("Tomorrow", DateTime.Now.AddDays(1));
}
