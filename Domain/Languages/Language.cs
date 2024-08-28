using Domain.Abstract;
using Domain.Users;

namespace Domain.Languages;

public class Language : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public ICollection<User> Users { get; private set; } = new HashSet<User>();

    public Language()
    { }

    public Language(string name)
    {
        Name = name;
    }
}
