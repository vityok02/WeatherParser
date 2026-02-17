using Application.Common.Interfaces;
using Telegram.Bot.Types;

namespace Bot.Services;

public static class TelegramFileAdapter
{
    public static InputFile ConvertToTelegramFile(IFile file)
    {
        return InputFile.FromStream(file.GetStream());
    }
}
