using Coravel.Invocable;

namespace NetCoreCoravel;

public class TestJob  : IInvocable, ICancellableInvocable, IInvocableWithPayload<TestJobPayload>
{
    private readonly IUser _user;
    private readonly IDateTime _dateTime;

    public TestJob(IUser user, IDateTime dateTime)
    {
        _user = user;
        _dateTime = dateTime;

        Payload ??= new TestJobPayload
        {
            CompanyId = Guid.NewGuid(),
            Start = new DateTime(2022, 1, 1),
            End = new DateTime(2022, 1, 31),
        };
    }

    public async Task Invoke()
    {
        if (Payload.CompanyId == Guid.Parse("7AF1CFB5-8E66-47A7-866C-925EA2C9804B") )
        {
            Console.WriteLine("--- queue start ---");
            await Task.Delay(TimeSpan.FromSeconds(5), CancellationToken);
            Console.WriteLine($"payload - {Payload}");
            Console.WriteLine("--- queue end ---");

            return;
        }
        
        Console.WriteLine($"-- begin job ({DateTime.Now:O}) --");

        Console.WriteLine($"start delay ({DateTime.Now:O})");
        await Task.Delay(TimeSpan.FromSeconds(10), CancellationToken);
        Console.WriteLine($"user - {_user.UserId()}, datetime - {_dateTime.Now()}");
        Console.WriteLine($"payload - {Payload}");
        Console.WriteLine($"end delay ({DateTime.Now:O})");

        Console.WriteLine($"-- end job ({DateTime.Now:O}) --");
    }

    public CancellationToken CancellationToken { get; set; }

    public TestJobPayload Payload { get; set; }
}

public class TestJobPayload
{
    public Guid CompanyId { get; set; }

    public DateTime Start { get; set; }
    
    public DateTime End { get; set; }

    public override string ToString()
    {
        return $"{nameof(CompanyId)}: {CompanyId}, {nameof(Start)}: {Start:d}, {nameof(End)}: {End:d}";
    }
}