using Common.Constants;

namespace Application;

public class UserSession
{
    public long Id { get; set; }
    public UserState State { get; private set; }
    public string MessageText { get; set; }
}
