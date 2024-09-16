using Domain.Languages;
using Infrastructure.Translations.Interfaces;

namespace Infrastructure.Translations;

public class PathProvider : IPathProvider
{
    public string GetFileName(string language)
    {
        string path = "Localization/";
        return language switch
        {
            Languages.English => path += "en.json",
            Languages.Ukrainian => path += "ua.json",
            _ => path += "en.json"
        };
    }

}