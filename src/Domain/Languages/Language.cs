using Domain.Abstract;
using Domain.Users;

namespace Domain.Languages;

public class Language : BaseEntity
{
    public string Name { get; private set; }
    public string Code { get; private set; }
    public ICollection<User> Users { get; private set; } = [];

    public Language(long id, string name, string code)
    {
        Id = id;
        Name = name;
        Code = code;
    }
}
