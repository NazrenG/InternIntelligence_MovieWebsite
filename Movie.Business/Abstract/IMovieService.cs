using Movie.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Business.Abstract
{
    public interface IMovieService
    {
        public Task AddMovie(Film film);
        public Task<List<Film>> GetAllMovies();
        public Task<List<Film>> GetTrendingMovies();
        public Task<Film> GetMovieByIdForWatchList(int id);
        public Task<Film> GetMovieByIdForApi(int id);
        public Task UpdateMovie(Film film);
        public Task DeleteMovie(int id);
        public Task<List<Film>> SearchFilmFromApi(string query);
        public Task<List<Film>> SearchFilmFromList(string query);
        public Task<List<Film>> FilterFilmForApi(string? language, int? year, double? vote);
        public Task<List<Film>> FilterFilmForList(string? language, string? year, int? vote);
    }
}
