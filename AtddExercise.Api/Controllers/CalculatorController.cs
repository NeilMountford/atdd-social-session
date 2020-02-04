using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace AtddExercise.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        [HttpGet("add/{numbers}")]
        public IActionResult Add(string numbers)
        {
            return Ok(numbers.Split(",").Select(int.Parse).Sum());
        }
    }
}