using eTickets.Data.Base;
using eTickets.Data.ViewModels;
using eTickets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Data.Services
{
    public interface IMovieService : IEntityBaseRepository<Movie>
    {
        Task<Movie> GetMovieByIdAsync(int id);
        Task<DropDownListNewMovie> GetDropDownListNewMovieValues();
        Task AddNewMovieAsync(ViewModelNewMovies data);
        Task UpdateMovieAsync(ViewModelNewMovies data);
    }
}
