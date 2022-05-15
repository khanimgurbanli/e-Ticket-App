using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
    public class CinemasController : Controller
    {
        private readonly ICinemaService _service;

        public CinemasController(ICinemaService service)=> _service = service;

        public async Task<IActionResult> Index() => View(await _service.GetAllAsync());

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Logo, Name, Description")] Cinema cinema)
        {
            if (!ModelState.IsValid)
            {
                return View(cinema);
            }

            await _service.AddAsync(cinema);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var cinemaDetailById = await _service.GetByIdAsync(id);

            if (cinemaDetailById == null) return View("NotFound");
            return View(cinemaDetailById);

        }

        public async Task<IActionResult> Edit(int id)
        {
            var getCinemaForEdit = await _service.GetByIdAsync(id);

            if (getCinemaForEdit == null) return View("NotFound");
            return View(getCinemaForEdit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Logo, Name, Description")] Cinema cinema)
        {
            if (!ModelState.IsValid) return View(cinema);

            await _service.UpdateAsync(id, cinema);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
