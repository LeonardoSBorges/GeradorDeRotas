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

            ViewBag.Error = null;
            if (teams.People.Count == 0)
            {
                ViewBag.Error = "Error";
                return RedirectToAction(nameof(Create));
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: People/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            ViewBag.PeopleAvaliable = null;
            ViewBag.Address = null;
            ViewBag.PeopleTeam = null;
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

            if (teams != null)
            {
                foreach (var peopleTeam in teams.People)
                {
                    hasTeam.Add(peopleTeam);
                }
            }

            ViewBag.PeopleTeam = hasTeam;
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
            var participatingOfTeam = Request.Form["checkPeopleAvailableToAdd"].ToList();
            var removePeopleOfTeam = Request.Form["checkPeopleTeamToRemove"].ToList();
            var addressId = Request.Form["addressRegisterTeams"].ToString();
            var address = await AddressServices.Details(addressId);

            if (participatingOfTeam.Count > 0 || removePeopleOfTeam.Count > 0)
            {
                List<Person> aux = new();
                foreach (var newPersonInTeam in participatingOfTeam)
                {
                    var person = await PersonServices.Details(newPersonInTeam);
                    await TeamsServices.AddPersonInTeams(id, person);
                }
                foreach (var personForRemoveOfList in removePeopleOfTeam)
                {
                    var person = await PersonServices.Details(personForRemoveOfList);
                    await TeamsServices.UpdatePersonInTeams(id, person);
                }
            }
            if (!TeamsExists(teams.Id))
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));

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
