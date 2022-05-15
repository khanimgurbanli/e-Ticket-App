using eTickets.Data.Base;
using eTickets.Data.Context;
using eTickets.Data.ViewModels;
using eTickets.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Data.Services
{
    public class MovieManager : EntityBaseRepository<Movie>, IMovieService
    {
        private readonly AppDBContext _context;
        public MovieManager(AppDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddNewMovieAsync(ViewModelNewMovies data)
        {
            var newMovie = new Movie()
            {
                Name = data.Name,
                Description = data.Description,
                Price = data.Price,
                ImageUrl = data.ImageUrl,
                ProducerId = data.ProducerId,
                CinemaId = data.CinemaId,
                movieCategory = data.movieCategory,
                BeginDate = data.BeginDate,
                EndDate = data.EndDate
            };

            await _context.Movies.AddAsync(newMovie);
            await _context.SaveChangesAsync();

            //many to many
            foreach (var actorId in data.ActorIds)
            {
                var newActorMovie = new ActorMovie()
                {
                    MovieId = newMovie.Id,
                    ActorId = actorId
                };
                await _context.ActorsMovies.AddAsync(newActorMovie);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<DropDownListNewMovie> GetDropDownListNewMovieValues()
        {
            var result = new DropDownListNewMovie()
            {
                Producers = await _context.Producers.OrderBy(x => x.FulName).ToListAsync(),
                Actors = await _context.Actors.OrderBy(x => x.FulName).ToListAsync(),
                Cinemas = await _context.Cinemas.OrderBy(x => x.Name).ToListAsync()
            };

            return result;
        }

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            var movieDetails = await _context.Movies
                .Include(x => x.Producer)
                .Include(x => x.Cinema)
                .Include(x => x.ActorMovies).ThenInclude(x => x.Actor)
                .FirstOrDefaultAsync(x => x.Id == id);

            return movieDetails;
        }

        public async Task UpdateMovieAsync(ViewModelNewMovies editMovie)
        {
            var inDbMovie = await _context.Movies.FirstOrDefaultAsync(x => x.Id == editMovie.Id);

            if (inDbMovie != null)
            {
                inDbMovie.Name = editMovie.Name;
                inDbMovie.Description = editMovie.Description;
                inDbMovie.Price = editMovie.Price;
                inDbMovie.ImageUrl = editMovie.ImageUrl;
                inDbMovie.ProducerId = editMovie.ProducerId;
                inDbMovie.CinemaId = editMovie.CinemaId;
                inDbMovie.movieCategory = editMovie.movieCategory;
                inDbMovie.BeginDate = editMovie.BeginDate;
                inDbMovie.EndDate = editMovie.EndDate;

                await _context.SaveChangesAsync();
            }

            var dbActors = _context.ActorsMovies.Where(x => x.MovieId == editMovie.Id).ToList();
            _context.ActorsMovies.RemoveRange(dbActors);
            await _context.SaveChangesAsync();


            //many to many add Actor
            foreach (var actorId in editMovie.ActorIds)
            {
                var newActorMovie = new ActorMovie()
                {
                    MovieId = editMovie.Id,
                    ActorId = actorId
                };
                await _context.ActorsMovies.AddAsync(newActorMovie);
            }
            await _context.SaveChangesAsync();
        }
    }
}
