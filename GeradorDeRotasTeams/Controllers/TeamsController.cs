using GeradorDeRotasTeams.Service;
using Microsoft.AspNetCore.Mvc;
using ModelShare.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeradorDeRotasTeams.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly TeamsServices _teamsService;
        public TeamsController(TeamsServices teamsTeams)
        {
            _teamsService = teamsTeams;
        }

        [HttpGet]
        public async Task<IEnumerable<Teams>> GetAll()
        {
            return await _teamsService.GetPeople();
        }

        [HttpGet("{id}", Name = "GetPerson")]
        public async Task<Teams> Get(string id) =>
            await _teamsService.GetPerson(id);

        [HttpPost]
        public async Task<IActionResult> Post(Teams newPerson)
        {
            var result = await _teamsService.PostNewPerson(newPerson);
            if (result.Item1 == 400)
                return NotFound(result.Item2);
            return Ok(result.Item2);
        }

        [HttpPut]
        public async Task<IActionResult> Put(Teams person)
        {
            var result = await _teamsService.Update(person);
            if (result.Item1 == 400)
                return NotFound(result.Item2);
            return Ok(result.Item2);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            await _teamsService.Delete(id);
            return NoContent();
        }

    }
}
