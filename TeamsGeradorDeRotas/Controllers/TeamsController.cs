using Microsoft.AspNetCore.Mvc;
using ModelShare.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeamsGeradorDeRotas.Services;

namespace TeamsGeradorDeRotas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly TeamsServices _teamsServices;
        public TeamsController(TeamsServices teamsServices)
        {

            _teamsServices = teamsServices;
        }

        [HttpGet]
        public async Task<ICollection<Teams>> GetAll()
        {

            await _teamsServices.UpdateTeamsData();
            var result = await _teamsServices.GetAll();
            return result;
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _teamsServices.GetById(id);

            if (result == null)
                return NotFound("Nao foi encontrado nenhum registro com este id, por favor verifique e tente novamente!");

            return Ok(result);
        }

        [HttpGet("GetByCity/{id}")]
        public async Task<List<Teams>> GetTeamsByCity(string id)
        {
            return await _teamsServices.GetTeamsByCity(id);
        }

        [HttpPost("Post")]
        public async Task<IActionResult> Post(Teams newTeams)
        {
            var result = await _teamsServices.Create(newTeams);

            if (result == 200)
                return Ok(newTeams);
            else if (result == 204)
                return NoContent();
            return NotFound("Nao foi possivel registrar um novo time, por favor verifique se todos os dados foram inseridos!");
        }


        [HttpPut]
        public async Task<IActionResult> Put(Teams replaceTeams)
        {
            var result = await _teamsServices.Replace(replaceTeams);
            if (result == 404)
                return NotFound("Nao foi encontrado nenhum registro com este id, por favor verifique e tente novamente!");

            return Ok(replaceTeams);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _teamsServices.Delete(id);
            if (result == 404)
                return NotFound("Nao foi encontrado nenhum registro com este id, por favor verifique e tente novamente!");

            return Ok(result);
        }
    }
}
