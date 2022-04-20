using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GeradorDeRotasMVC.Models;
using GeradorDeRotasMVC.Services;
using System.Collections.Generic;
using System.Linq;

namespace GeradorDeRotasMVC.Controllers
{
    public class TeamsController : Controller
    {
        // GET: People
        public async Task<IActionResult> Index()
        {
            return View(await TeamsServices.GetAll());
        }

        // GET: People/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teams = await TeamsServices.Details(id);
            if (teams == null)
            {
                return NotFound();
            }

            return View(await TeamsServices.Details(id));
        }

        // GET: People/Create
        public async Task<IActionResult> Create()
        {
            List<Person> result = await PersonServices.GetAll();
            ViewBag.People = result;
            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Teams teams)
        {
            List<Person> selectPeople = new List<Person>();
            if (ModelState.IsValid)
            {
                if(Request.Form["checkPeopleTeam"].ToList().Count == 0)
                    return View(nameof(Create), teams);
                
                foreach(var item in Request.Form["checkPeopleTeam"].ToList())
                {
                    selectPeople.Add(new Person(item));
                }

                teams.People = selectPeople;
                await TeamsServices.Create(teams);
                return RedirectToAction(nameof(Index));
            }
            return View(teams);
        }

        // GET: People/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teams = await TeamsServices.Details(id);
            if (teams == null)
            {
                return NotFound();
            }
            return View(teams);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,Name")] Teams teams)
        {
            string id = teams.Id;
            if (id != teams.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await TeamsServices.Update(teams);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamsExists(teams.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(teams);
        }

        // GET: People/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await TeamsServices.Details(id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var person = await TeamsServices.Details(id);
            await PersonServices.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool TeamsExists(string id)
        {
            var result = TeamsServices.Details(id);
            return result != null ? true : false;
        }
    }
}
