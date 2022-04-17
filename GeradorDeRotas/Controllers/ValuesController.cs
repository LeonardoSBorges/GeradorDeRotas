using GeradorDeRotasPerson.Service;
using Microsoft.AspNetCore.Mvc;
using ModelShare.DTO;
using ModelShare.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeradorDeRotasPerson.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly PersonService _personService;
        public ValuesController(PersonService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        public async Task<IEnumerable<Person>> GetAll()
        {
            return await _personService.GetPeople();
        }

        [HttpGet("{id}", Name = "GetPerson")]
        public async Task<Person> Get(string id) =>
            await _personService.GetPerson(id);

        [HttpPost]
        public async Task<IActionResult> Post(Person newPerson)
        {
            var result = await _personService.PostNewPerson(newPerson);
            if (result.Item1 == 400)
                return NotFound(result.Item2);
            return Ok(result.Item2);
        }

        [HttpPut]
        public async Task<IActionResult> Put(Person person)
        {
            var result = await _personService.Update(person);
            if (result.Item1 == 400)
                return NotFound(result.Item2);
            return Ok(result.Item2);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            await _personService.Delete(id);
            return NoContent();
        }
        
    }
}
