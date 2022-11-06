namespace NetCoreCoravel;

public interface IUser
{
    Guid UserId();
}

public class CurrentUser : IUser
{
    public Guid UserId()
    {
        return Guid.NewGuid();
    }
}