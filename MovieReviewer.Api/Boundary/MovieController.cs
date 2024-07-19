using Microsoft.AspNetCore.Mvc;
using MovieReviewer.Api.Control.Repository;

namespace MovieReviewer.Api.Boundary
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController(MovieR movieR) : Controller
    {
    }
}
