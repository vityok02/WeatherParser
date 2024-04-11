namespace Infrastructure.Weathers;

public static class PathBuilder
{
    public static string BuildPath(
        string basePath,
        string parameter,
        string token)
    {
        return $"{basePath}?q={parameter}&key={token}";
    }
}
