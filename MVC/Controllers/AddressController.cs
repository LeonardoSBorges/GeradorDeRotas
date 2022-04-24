using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC.Models;
using MVC.Services;
using System.Threading.Tasks;

namespace MVC.Controllers
{
    public class AddressController : Controller
    {
        // GET: Address
        public async Task<IActionResult> Index()
        {
            return View(await AddressServices.GetAll());
        }

        // GET: Address/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var address = await AddressServices.Details(id);
            if (address == null)
            {
                return NotFound();
            }

            return View(address);
        }

        // GET: Address/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Address/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("State,City")] Address address)
        {
            if (ModelState.IsValid)
            {
                await AddressServices.Create(address);
                return RedirectToAction(nameof(Index));
            }
            return View(address);
        }

        // GET: Address/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var address = await AddressServices.Details(id);
            if (address == null)
            {
                return NotFound();
            }
            return View(address);
        }

        // POST: Address/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,State,City")] Address address)
        {
            if (id != address.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await AddressServices.Update(address);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AddressExists(address.Id))
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
            return View(address);
        }

        // GET: Address/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var address = await AddressServices.Details(id);
            if (address == null)
            {
                return NotFound();
            }

            return View(address);
        }

        // POST: Address/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var address = await AddressServices.Details(id);
            await AddressServices.Delete(id);            
            return RedirectToAction(nameof(Index));
        }

        private bool AddressExists(string id)
        {
            var result = TeamsServices.Details(id);
            return result != null ? true : false;
        }
    }
}
