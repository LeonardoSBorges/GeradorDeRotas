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
            var people = await PersonServices.GetAll();
            return View(people);
        }
        public async Task<IActionResult> Create(Person person)
        {

            if (string.IsNullOrEmpty(person.Name))
                return View(person);

            await PersonServices.Create(person);

            return View(nameof(Index));
        }
    }
}
