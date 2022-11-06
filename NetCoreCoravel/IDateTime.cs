namespace NetCoreCoravel;

public interface IDateTime
{
    DateTime Now();
}

public class CurrentDateTime : IDateTime
{
    public DateTime Now()
    {
        return DateTime.Now;
    }
}