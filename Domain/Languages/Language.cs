using Domain.Abstract;
using Domain.Users;

namespace Domain.Languages;

public class Language : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public string Code { get; private set; } = string.Empty;
    public ICollection<User> Users { get; private set; } = new List<User>();

    public Language(long id, string name, string code)
    {
        Id = id;
        Name = name;
        Code = code;
    }
}
