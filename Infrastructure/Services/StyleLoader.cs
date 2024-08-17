using Application.Common.Interfaces;
using System.Text;

namespace Infrastructure.Services;

public class StyleLoader : IStyleLoader
{
    public string LoadStyles(string path)
    {
        return File.ReadAllText(path, Encoding.UTF8);
    }
}