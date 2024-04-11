namespace Domain.Abstract;

public sealed record Error(string Description)
{
    public static readonly Error None = new(string.Empty);
    public override string ToString()
    {
        return $"Description: {Description}";
    }
}
