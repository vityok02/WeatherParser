using Common.Constants;
using Infrastructure.Translations.Interfaces;

namespace Infrastructure.Translations;

public class PathProvider : IPathProvider
{
    private string _path = "Localization/";
    public string GetFileName(string language)
    {
        return language switch
        {
            Languages.English => _path += "en.json",
            Languages.Ukrainian => _path += "ua.json",
            _ => _path += "en.json"
        };
    }

}