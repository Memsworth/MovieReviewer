using Microsoft.AspNetCore.Mvc;
using MovieReviewer.Api.Control.Repository;

namespace MovieReviewer.Api.Boundary
{
    [ApiController]
    [Route("[controller]")]
    public class ReviewController(ReviewR reviewR) : Controller
    {
    }
}
