using eTickets.Data.Context;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
    public class ActorsController : Controller
    {
        private readonly IActorService _service;

        public ActorsController(IActorService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAllAsync());
        }

        public IActionResult Create() =>  View();

        [HttpPost]
        public async Task<IActionResult> Create([Bind("profilPctureUrl", "FulName", "Bio")] Actor actor)
        {
            if (!ModelState.IsValid) return View(actor);

            await _service.AddAsync( actor);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var actorDetailById =await _service.GetByIdAsync(id);

            if (actorDetailById == null) RedirectToAction("Index");
            return View(actorDetailById);

        }

        public async Task<IActionResult> Edit(int id)
        {
            var getActorForEdit = await _service.GetByIdAsync(id);

            if (getActorForEdit == null) return View("NotFound");
            return View(getActorForEdit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id,[Bind("profilPctureUrl, FulName, Bio")] Actor actor)
        {
            if (!ModelState.IsValid)
                return View(actor);

            await _service.UpdateAsync(id,actor);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
