using System.Text;

namespace Domain.Abstract;

public static class StringBuilderExtensions
{
    public static StringBuilder AppendLineIfNotNull<T>(this StringBuilder sb, T? value, string message)
    {
        if (value is not null)
        {
            sb.AppendLine(message);
        }

        return sb;
    }
}
