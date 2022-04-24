using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC.Models;
using MVC.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Controllers
{
    [Authorize]
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
            List<Address> address = await AddressServices.GetAll();
            List<Person> hasantTeam = new List<Person>();
            foreach (var person in result)
            {
                if (person.HaveTeam == false)
                    hasantTeam.Add(person);
            }
            ViewBag.Address = address;
            ViewBag.People = hasantTeam;
            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,City,State, Person")] Teams teams)
        {
            List<Person> selectPeople = new List<Person>();

            if (Request.Form["checkPeopleTeam"].ToList().Count == 0)
                return View(nameof(Create), teams);

            foreach (var item in Request.Form["checkPeopleTeam"].ToList())
            {
                selectPeople.Add(new Person(item, null));
            }

            var addressId = Request.Form["addressRegisterTeams"].ToString();
            var address = await AddressServices.Details(addressId);

            teams.Address = address;
            teams.People = selectPeople;

            await TeamsServices.Create(teams);

            return RedirectToAction(nameof(Index));
        }

        // GET: People/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            List<Person> result = await PersonServices.GetAll();
            List<Person> hasantTeam = new List<Person>();
            List<Address> address = await AddressServices.GetAll();
            var hasTeam = new List<Person>();
            foreach (var person in result)
            {
                if (person.HaveTeam == false)
                    hasantTeam.Add(person);
            }
            ViewBag.PeopleAvaliable = hasantTeam;
            ViewBag.Address = address;
            var teams = await TeamsServices.Details(id);

            foreach (var peopleTeam in teams.People)
            {
                hasTeam.Add(peopleTeam);
            }

            ViewBag.PeopleTeam = hasTeam;
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
            teams.People = new List<Person>();
            var team = await TeamsServices.Details(teams.Id);
            var people = team.People;
            
            var participatingOfTeam = Request.Form["checkPeopleAvailableToAdd"].ToList();
            var peopleHasntTeam = Request.Form["checkPeopleTeamToRemove"].ToList();
            var addressId = Request.Form["addressRegisterTeams"].ToString();
            var address = await AddressServices.Details(addressId);

            if (participatingOfTeam.Count > 0 || peopleHasntTeam.Count > 0)
            {
                var test = new List<Person>();  
                foreach (var participant in participatingOfTeam)
                {
                    var person = await PersonServices.Details(participant);
                    teams.People.Add(person);
                }
                foreach(var removePerson in peopleHasntTeam)
                {
                    var person = await PersonServices.Details(removePerson);
                    test.Add(person);
                }
                foreach (var item in people)
                {
                    int count = 0;
                    foreach (var personTeam in test)
                        if (item.Id == personTeam.Id)
                        {
                            count++;
                            personTeam.HaveTeam = false;
                            await PersonServices.Update(personTeam);
                        }

                    if (count == 0)
                        teams.People.Add(item);
                }
            }

            teams.Address = address;

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
            await TeamsServices.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool TeamsExists(string id)
        {
            var result = TeamsServices.Details(id);
            return result != null ? true : false;
        }
    }
}
