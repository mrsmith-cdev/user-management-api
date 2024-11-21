using Microsoft.AspNetCore.Mvc;

namespace HomeServiceApplication_Api.Controllers.Shared
{
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("/api/[controller]")]
    public class BaseApiController : ControllerBase
    {
    }
}
