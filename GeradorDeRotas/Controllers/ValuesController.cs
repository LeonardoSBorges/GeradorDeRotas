using GeradorDeRotasPerson.Service;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("GetById/{id}", Name = "GetById")]
        public async Task<Person> GetById(string id) =>
            await _personService.GetById(id);

        [HttpGet("{name}", Name = "GetByName")]
        public async Task<Person> Get(string name) =>
            await _personService.GetPerson(name);

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

        [HttpDelete("{id}", Name = "GetPerson")]
        public async Task<IActionResult> Delete(string id)
        {
            await _personService.Delete(id);
            return NoContent();
        }
        
    }
}
