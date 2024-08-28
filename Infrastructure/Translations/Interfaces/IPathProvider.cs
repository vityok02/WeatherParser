namespace Infrastructure.Translations.Interfaces;

public interface IPathProvider
{
    string GetFileName(string language);
}