using eTickets.Data.Context;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
    public class ProducersController : Controller
    {
        private readonly IProducerService _service;

        public ProducersController(IProducerService service) => _service = service;

        public async Task<IActionResult> Index() => View(await _service.GetAllAsync());

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create([Bind("profilPctureUrl", "FulName", "Bio")] Producer producer)
        {
            if (!ModelState.IsValid)
            {
                return View(producer);
            }

            await _service.AddAsync(producer);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var producerDetailById = await _service.GetByIdAsync(id);

            if (producerDetailById == null) RedirectToAction("Index");
            return View(producerDetailById);

        }

        public async Task<IActionResult> Edit(int id)
        {
            var getProducerForEdit = await _service.GetByIdAsync(id);

            if (getProducerForEdit == null) return View("NotFound");
            return View(getProducerForEdit);
        }

        [HttpPut]
        public async Task<IActionResult> Edit(int id, [Bind("profilPctureUrl, FulName, Bio")] Producer producer)
        {
            if (!ModelState.IsValid)
            {
                return View(producer);
            }

            await _service.UpdateAsync(id, producer);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
