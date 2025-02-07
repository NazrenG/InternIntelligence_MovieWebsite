using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Movie.Business.Abstract;
using Movie.Entities.Models;
using Movie.Webapi.Dtos;

namespace Movie.Webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private static List<MovieDto> GetMovieDto(List<Film> films)
        {
            return films.Select(item => new MovieDto
            {
                Title = item.Title,
                ReleaseDate = item.ReleaseDate,
                GenreIds = item.GenreIds,
                OriginalLanguage = item.OriginalLanguage,
                Popularity = item.Popularity,
                PosterPath = item.PosterPath,
                OriginalTitle = item.OriginalTitle,
                VoteAverage = item.VoteAverage,
                VoteCount = item.VoteCount,
                Overview = item.Overview,
            }).ToList();
        }
        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }
        [Authorize(Roles ="User")]
        [HttpPost("NewMovieInWatchlist/{id}")]
        public async Task<IActionResult> AddMovieTWatchlist(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { Message = "Invalid movie ID!" });
            }

            var movie = await _movieService.GetMovieByIdForApi(id);
            if (movie == null)
            {
                return BadRequest(new { Message = "Invalid Movie data!" });
            }

            var item = new Film
            {
                Title = movie.Title,
                ReleaseDate = movie.ReleaseDate,
                GenreIds = movie.GenreIds,
                OriginalLanguage = movie.OriginalLanguage,
                Popularity = movie.Popularity,
                PosterPath = movie.PosterPath,
                OriginalTitle = movie.OriginalTitle,
                VoteAverage = movie.VoteAverage,
                VoteCount = movie.VoteCount,
                Overview = movie.Overview,
                Adult = movie.Adult,
                BackdropPath = movie.BackdropPath,
            };

            await _movieService.AddMovie(item);
            return Ok(item);
        }
        //see everyone
        [HttpGet("TrendMovies")]
        public async Task<IActionResult> GetTrendMovies()
        {
            var items = await _movieService.GetTrendingMovies();
            if (!items.Any())
            {
                return NotFound(new { Message = "No trending movies found." });
            }

            var list = GetMovieDto(items);

            return Ok(list);
        }

        [Authorize(Roles = "User,Admin")]
        [HttpGet("WatchlistMovies")]
        public async Task<IActionResult> GetWatchListMovies()
        {
            var items = await _movieService.GetAllMovies();
            if (!items.Any())
            {
                return NotFound(new { Message = "Watchlist is empty." });
            }

            var list = GetMovieDto(items);
            return Ok(list);
        }

        [Authorize(Roles = "User,Admin")]
        [HttpGet("MovieDetailByIdFromList/{id}")]
        public async Task<IActionResult> GetMovie([FromRoute] int id)
        {
            var item = await _movieService.GetMovieByIdForWatchList(id);
            if (item == null)
            {
                return NotFound(new { Message = "Movie not found!" });
            }
            var movie = new MovieDto
            {
                Title = item.Title,
                ReleaseDate = item.ReleaseDate,
                GenreIds = item.GenreIds,
                OriginalLanguage = item.OriginalLanguage,
                Popularity = item.Popularity,
                PosterPath = item.PosterPath,
                OriginalTitle = item.OriginalTitle,
                VoteAverage = item.VoteAverage,
                VoteCount = item.VoteCount,
                Overview = item.Overview,
            };
            return Ok(movie);
        }


        [HttpGet("MovieDetailByIdFromApi/{id}")]
        public async Task<IActionResult> GetMovieFromApi([FromRoute] int id)
        {
            var item = await _movieService.GetMovieByIdForApi(id);
            if (item == null)
            {
                return NotFound(new { Message = "Movie not found!" });
            }
            var movie = new MovieDto
            {
                Title = item.Title,
                ReleaseDate = item.ReleaseDate,
                GenreIds = item.GenreIds,
                OriginalLanguage = item.OriginalLanguage,
                Popularity = item.Popularity,
                PosterPath = item.PosterPath,
                OriginalTitle = item.OriginalTitle,
                VoteAverage = item.VoteAverage,
                VoteCount = item.VoteCount,
                Overview = item.Overview,
            };
            return Ok(movie);
        }
        [Authorize(Roles = "User,Admin")]
        [HttpDelete("DeletedMovieFromWatchList/{id}")]
        public async Task<IActionResult> DeleteMovieFromWatchList(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { Message = "Invalid movie ID!" });
            }

            var item = await _movieService.GetMovieByIdForWatchList(id);
            if (item == null)
            {
                return NotFound(new { Message = "Movie not found!" });
            }

            await _movieService.DeleteMovie(id);
            return Ok(new { Message = "Movie deleted successfully" });
        }
        [Authorize(Roles = "User,Admin")]
        [HttpPut("UpdatedMovieFromWatchList/{id}")]
        public async Task<IActionResult> UpdatedMovie(int id, [FromBody] MovieDto dto)
        {
            if (id <= 0)
            {
                return BadRequest(new { Message = "Invalid movie ID!" });
            }

            if (dto == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
             

            var movie = await _movieService.GetMovieByIdForWatchList(id);
            if (movie == null)
            {
                return NotFound(new { Message = "Movie not found!" });
            }

            movie.Adult = dto.Adult;
            movie.Title = dto.Title;
            movie.OriginalTitle = dto.OriginalTitle;
            movie.VoteAverage = dto.VoteAverage;
            movie.VoteCount = dto.VoteCount;
            movie.Overview = dto.Overview;
            movie.Popularity = dto.Popularity;
            movie.BackdropPath = dto.BackdropPath;
            movie.GenreIds = dto.GenreIds;
            movie.OriginalLanguage = dto.OriginalLanguage;
            movie.PosterPath = dto.PosterPath;
            movie.ReleaseDate = dto.ReleaseDate;
            await _movieService.UpdateMovie(movie);
            return Ok(new { Message = "Updated successfully Movie" });
        }

        //search movie for name when added to watchlist
        [Authorize(Roles = "User")]
        [HttpGet("MovieByTitleFromList")]
        public async Task<IActionResult> GetMovieByTitleFromList(string query)
        {
            var items = await _movieService.SearchFilmFromList(query);
            if (items == null)
            {
                return NotFound(new { Message = "Movie not found!" });
            }
            var movies = GetMovieDto(items);
            return Ok(movies);
        }

        //search movie for name from api
        [HttpGet("MovieByTitleFromApi")]
        public async Task<IActionResult> GetMovieByTitleFromApi(string query)
        {
            var items = await _movieService.SearchFilmFromApi(query);
            if (items == null)
            {
                return NotFound(new { Message = "Movie not found!" });
            }
            var movies = GetMovieDto(items);
            return Ok(movies);
        }


        //filter movie when added to watchlist
        [Authorize(Roles = "User,Admin")]
        [HttpGet("FilteredMovieFromList")]
        public async Task<IActionResult> GetFilterMovieFromList(string? language,string? year,int? vote)
        {
            var items = await _movieService.FilterFilmForList(language,year,vote);
            if (items == null)
            {
                return NotFound(new { Message = "Movie not found!" });
            }
            var movies = GetMovieDto(items);
            return Ok(movies);
        }

        //filter movie  from api
        [HttpGet("FilteredMovieFromApi")]
        public async Task<IActionResult> GetFilterMoviFromApi(string? language, int? year, int? vote)
        {
            var items = await _movieService.FilterFilmForApi(language,year,vote);
            if (items == null)
            {
                return NotFound(new { Message = "Movie not found!" });
            }
            var movies = GetMovieDto(items);
            return Ok(movies);
        }

    }
}
