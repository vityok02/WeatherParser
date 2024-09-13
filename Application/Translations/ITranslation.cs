namespace Application.Translations;

public interface ITranslation
{
    string this[string key] { get; }
}
