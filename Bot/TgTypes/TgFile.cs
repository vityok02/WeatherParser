using Application.Interfaces;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Bot.TgTypes;

public class TgFile : InputFile, IAppFile
{
    public InputFile File { get; private set; } = default!;

    public override FileType FileType => File.FileType;

    public TgFile()
    { }

    public TgFile(Stream stream)
    {
        File = new InputFileStream(stream);
    }

    public IAppFile FromStream(Stream stream)
    {
        return new TgFile(stream);
    }
}
