namespace Domain.Translations;

public record Translation(
    TranslationSection Messages,
    TranslationSection Buttons,
    TranslationSection Units,
    TranslationSection Weather);