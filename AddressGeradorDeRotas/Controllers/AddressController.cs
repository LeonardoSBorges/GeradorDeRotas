using AddressGeradorDeRotas.Service;
using Microsoft.AspNetCore.Mvc;
using ModelShare.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AddressGeradorDeRotas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly AddressService _addressService;
        public AddressController(AddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpGet]
        public async Task<IEnumerable<Address>> GetAll()
        {
            return await _addressService.GetAllAddress();
        }

        [HttpGet("{id}", Name = "GetAddress")]
        public async Task<Address> Get(string id)
        {
            return await _addressService.GetAddress(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Address newPerson)
        {
            var result = await _addressService.PostNewPerson(newPerson);
            if (result.Item1 == 400)
                return NotFound(result.Item2);
            return Ok(result.Item2);
        }

        [HttpPut]
        public async Task<IActionResult> Put(Address person)
        {
            var result = await _addressService.Update(person);
            if (result.Item1 == 400)
                return NotFound(result.Item2);
            return Ok(result.Item2);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _addressService.Delete(id);
            return NoContent();
        }
        
    }
}
