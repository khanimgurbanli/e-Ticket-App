using eTickets.Data.Context;
using eTickets.Data.Services;
using eTickets.Data.ViewModels;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieService _service;

        public MoviesController(IMovieService service) => _service = service;

        public async Task<IActionResult> Index() => View(await _service.GetAllAsync(c => c.Cinema));

        public async Task<IActionResult> Filter(string searchString)
        {
            var GetAllMovies = await _service.GetAllAsync(c => c.Cinema);

            if (!string.IsNullOrEmpty(searchString))
            {
                var filterResult = GetAllMovies.Where(x => x.Name.Contains(searchString) || x.Description.Contains(searchString)).ToList();
                return View("Index", filterResult);
            }

            return View("Index", GetAllMovies);
        }


        public async Task<IActionResult> Create()
        {
            var movieFromDropdownList = await _service.GetDropDownListNewMovieValues();

            ViewBag.Actors = new SelectList(movieFromDropdownList.Actors, "Id", "FulName");
            ViewBag.Cinemas = new SelectList(movieFromDropdownList.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(movieFromDropdownList.Producers, "Id", "FulName");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ViewModelNewMovies newMovies)
        {
            if (!ModelState.IsValid)
            {
                var movieFromDropdownList = await _service.GetDropDownListNewMovieValues();

                ViewBag.Actors = new SelectList(movieFromDropdownList.Actors, "Id", "FulName");
                ViewBag.Cinemas = new SelectList(movieFromDropdownList.Cinemas, "Id", "Name");
                ViewBag.Producers = new SelectList(movieFromDropdownList.Producers, "Id", "FulName");

                return View("Create", newMovies);
            }
            await _service.AddNewMovieAsync(newMovies);
            return  RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var movieDetail = await _service.GetMovieByIdAsync(id);
            if (movieDetail == null) return View("NotFound");

            var getMovieData = new ViewModelNewMovies()
            {
                Id=movieDetail.Id,
                Name = movieDetail.Name,
                Description = movieDetail.Description,
                Price = movieDetail.Price,
                ImageUrl = movieDetail.ImageUrl,
                ProducerId = movieDetail.ProducerId,
                CinemaId = movieDetail.CinemaId,
                movieCategory = movieDetail.movieCategory,
                BeginDate = movieDetail.BeginDate,
                EndDate = movieDetail.EndDate,
                ActorIds = movieDetail.ActorMovies.Select(x => x.ActorId).ToList()
            };

            var movieFromDropdownList = await _service.GetDropDownListNewMovieValues();

                ViewBag.Actors = new SelectList(movieFromDropdownList.Actors, "Id", "FulName");
                ViewBag.Cinemas = new SelectList(movieFromDropdownList.Cinemas, "Id", "Name");
                ViewBag.Producers = new SelectList(movieFromDropdownList.Producers, "Id", "FulName");

            return View(getMovieData);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ViewModelNewMovies movie)
        {
            if (id != movie.Id) return View(nameof(NotFound));

            if (!ModelState.IsValid)
            {
                var movieFromDropdownList = await _service.GetDropDownListNewMovieValues();

                ViewBag.Actors = new SelectList(movieFromDropdownList.Actors, "Id", "FulName");
                ViewBag.Cinemas = new SelectList(movieFromDropdownList.Cinemas, "Id", "Name");
                ViewBag.Producers = new SelectList(movieFromDropdownList.Producers, "Id", "FulName");

                return View("Edit", movie);
            }
            await _service.UpdateMovieAsync(movie); 

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var movieDetailById = await _service.GetMovieByIdAsync(id);

            if (movieDetailById == null) RedirectToAction("NotFound");
            return View(movieDetailById);
        }


        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
