using Application.Common.Interfaces;
using Telegram.Bot.Types;

namespace Bot.Services;

public class TelegramFileAdapter
{
    public InputFile ConvertToTelegramFile(IFile file)
    {
        return InputFile.FromStream(file.GetStream());
    }
}
