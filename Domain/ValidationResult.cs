using System.Text;

namespace Domain;

public class ValidationResult
{
    public List<string> Errors => [];
    public bool IsValid => Errors.Count == 0;
    public bool IsError => !IsValid;

    public override string ToString()
    {
        var builder = new StringBuilder();

        foreach (var error in Errors)
        {
            builder.AppendLine(error);
        }

        return builder.ToString();
    }
}
