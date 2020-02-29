using Microsoft.AspNetCore.Mvc;
using WorkplannerCQRS.API.Domain;

namespace WorkplannerCQRS.API.Controllers
{
    /// <summary>
    /// Base API controller class
    /// </summary>
    [ApiController]
    [Route("/api/[controller]")]
    public class ApiBaseController : ControllerBase
    {
    }
}