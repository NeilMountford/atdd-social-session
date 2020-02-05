using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace AtddExercise.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        private readonly IDataAccess _dataAccess;

        public CalculatorController(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }
        
        [HttpGet("add/{numbers}")]
        public IActionResult Add(string numbers)
        {
            var result = numbers.Split(",").Select(int.Parse).Sum();
            _dataAccess.SaveInputsAndResult(numbers, result.ToString());
            return Ok(result);
        }
        
        [HttpGet("add")]
        public IActionResult Add()
        {
            return BadRequest();
        }
    }
}