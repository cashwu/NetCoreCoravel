using Coravel.Queuing.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace NetCoreCoravel.Controllers;

public class HomeController : Controller
{
    private readonly IQueue _queue;

    public HomeController(IQueue queue)
    {
        _queue = queue;
    }
    
    [Route("/")]
    public IActionResult Index()
    {
        return Ok(123);
    }
    
    [Route("/Job")]
    public IActionResult Job()
    {
        _queue.QueueInvocableWithPayload<TestJob, TestJobPayload>(new TestJobPayload
        {
            CompanyId = Guid.Parse("7AF1CFB5-8E66-47A7-866C-925EA2C9804B"),
            Start = new DateTime(2022, 10, 1),
            End = new DateTime(2022, 10, 30),
        });
        
        return Ok(nameof(Job));
    }
}