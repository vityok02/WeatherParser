﻿using Domain.Translations;

namespace Infrastructure.Translations;

public record LanguagePack(string Language, Translation Translations);
