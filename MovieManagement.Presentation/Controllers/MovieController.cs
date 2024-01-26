using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieManagement.Application.Abstraction;
using MovieManagement.Domain.Movie;
using MovieManagement.Infrastructure.Dtos;

namespace MovieManagement.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;
        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }


        [HttpPost("Add")]        
        public async Task<IActionResult> AddMovie([FromBody] MovieDto data)
        {
            
            var result = await _movieService.Add(data);

            return Ok(result);
        }

        [HttpPut("{Id}")]        
        public async Task<IActionResult> Update([FromBody] MovieDto data, int Id)
        {
            
            var result = await _movieService.Update(data, Id);

            return Ok(result);
        }

        [HttpGet("All")]
        public async Task<IActionResult> AllMovie()
        {

            var result = await _movieService.GetAll();

            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> MovieById(int Id)
        {

            var result = await _movieService.GetById(Id);

            return Ok(result);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Remove(int Id)
        {

            var result = await _movieService.Delete(Id);

            return Ok(result);
        }

    }
}
