using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OMDMovie.Entities;
using System.Xml.Linq;

namespace OMDMovie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly string apikey = "d43831a1";
       
        private readonly HttpClient _httpClient;

        public MovieController(HttpClient httpClient)
        {
            _httpClient = httpClient;
           
        }

        [HttpGet]
        [Route("SearchByTitle")]

        public async Task<IActionResult> SearchMovies([FromQuery]string title)
        {
            var MovieInformation = await GetMovieInfomationFromApi(title);
            if (MovieInformation == null)
            {
                return NotFound();
            }
           
            return Ok(MovieInformation);
        }


        private async Task<MovieInformation> GetMovieInfomationFromApi(string title)
        {
            var apiUrl = $"http://www.omdbapi.com/?i=tt3896198&apikey=d43831a1&t={title}";
            var response = await _httpClient.GetStringAsync(apiUrl);
            return JsonConvert.DeserializeObject<MovieInformation>(response);
        }


    }
}
