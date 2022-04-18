using GeradorDeRotasMVC.Models;
using GeradorDeRotasMVC.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GeradorDeRotasMVC.Controllers
{
    public class PersonController : Controller
    {
       public async Task<IActionResult> Index()
        {
            var result = View(await PersonServices.GetAll());
            return result;
        }

        public async Task<IActionResult> Create(Person person)
        {

            if (string.IsNullOrEmpty(person.Name))
                return View(person);

            await PersonServices.Create(person);
            
            return View(Index());
        }

        public async Task<IActionResult> Details(string id)
        {
            var result =  await PersonServices.Details(id);

            return View(result);
        }

        public async Task<IActionResult> Edit(Person person)
        {
             if(person.Name == null)
                return View(person);
            
            await PersonServices.Update(person);
            var result = person.Id;
            return View(Index());
        }
    }
}
