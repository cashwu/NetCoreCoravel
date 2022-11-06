using Coravel;
using Coravel.Queuing.Interfaces;
using NetCoreCoravel;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<IUser, CurrentUser>();
builder.Services.AddScoped<IDateTime, CurrentDateTime>();

builder.Services.AddScoped<TestJob>();
builder.Services.AddQueue();
builder.Services.AddScheduler();

var app = builder.Build();

app.MapControllers();

app.Services.ConfigureQueue()
   .OnError(exception =>
   {
       Console.WriteLine("queue error !!");
       Console.WriteLine(exception);
   })
   .LogQueuedTaskProgress(app.Services.GetService<ILogger<IQueue>>());

app.Services.UseScheduler(scheduler =>
{
    scheduler.ScheduleWithParams<TestJob>()
             .EveryMinute()
             .RunOnceAtStart()
             .PreventOverlapping(nameof(TestJob));
    
}).OnError(exception =>
{
    Console.WriteLine("scheduler error !!");
    Console.WriteLine(exception);
});

app.Run();