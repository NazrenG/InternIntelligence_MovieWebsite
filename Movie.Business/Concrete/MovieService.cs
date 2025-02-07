using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Movie.Business.Abstract;
using Movie.DataAccess.Abstract;
using Movie.Entities.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Movie.Business.Concrete
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
      
         
        public MovieService(IMovieRepository movieRepository, HttpClient httpClient, IConfiguration configuration)
        {
            _movieRepository = movieRepository;
            _httpClient = httpClient;
            _configuration = configuration;
        }
        //add movie to watchlist from api
        public async Task AddMovie(Film film)
        { 
            await _movieRepository.Add(film);
        }
        //delete movie from watchlist
        public async Task DeleteMovie(int id)
        {
            var item = await _movieRepository.GetById(p => p.Id == id);
            if (item != null)
                await _movieRepository.Delete(item);
        }
        //all movie when add watchlist
        public async Task<List<Film>> GetAllMovies()
        {
            return await _movieRepository.GetAll();
        }
        //all popular movie from api
        public async Task<List<Film>> GetTrendingMovies()
        {
            var response = await _httpClient.GetAsync($"https://api.themoviedb.org/3/movie/popular?api_key={_configuration["TMDB:ApiKey"]}&page=2");
            if (!response.IsSuccessStatusCode)
            {
                return new List<Film>();
            }

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<TmdbResponse>(json); 
            Console.WriteLine(json);

            return result.Results.ToList();
        }

         
        //movie detail
        public async Task<Film> GetMovieByIdForWatchList(int id)
        {
            return await _movieRepository.GetById(p => p.Id == id);
        }

        public async Task<Film> GetMovieByIdForApi(int id)
        {
            var response = await _httpClient.GetAsync($"https://api.themoviedb.org/3/movie/popular?api_key={_configuration["TMDB:ApiKey"]}&page=2");
            if (!response.IsSuccessStatusCode)
            {
                return new Film();
            }

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<TmdbResponse>(json);
           

            return result.Results.FirstOrDefault(mi=>mi.Id==id);
        }
         //update watchlist
        public async Task UpdateMovie(Film film)
        {
            await _movieRepository.Update(film);
        }
        //seacrh film
        public async Task<List<Film>> SearchFilmFromApi(string query)
        {
           

            var url = $"https://api.themoviedb.org/3/search/movie?api_key={_configuration["TMDB:ApiKey"]}&query={query}";

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetStringAsync(url);
                var data = JsonConvert.DeserializeObject<TmdbResponse>(response);
                return data.Results;
            }
        }

        public async Task<List<Film>> SearchFilmFromList(string query)
        {
            var items = await _movieRepository.GetAll();
            if (items.Count != 0)
            {
                var searchList = items.Where(i => i.Title.ToLower().Contains(query) || i.OriginalTitle.ToLower().Contains(query)).ToList();
                if (searchList.Any())
                {
                    return searchList;

                }
            }
            return null;
        }
        //filter movie in Watchlist
        public async Task<List<Film>> FilterFilmForList(string? language, string? year, int? vote)
        {
            var items = await _movieRepository.GetAll();
            var searchList=new List<Film>();
            if (items.Count != 0)
            {
                if (language != null)
                {
                   searchList=items.Where(i=>i.OriginalLanguage.ToLower().Contains(language)).ToList();
                }
                if (year!=null)
                {
                    searchList = items.Where(i => i.ReleaseDate.ToLower().Contains(year)).ToList();
                }
                if (vote.HasValue)
                {
                    searchList = items.Where(i => i.VoteAverage>=vote).ToList();
                }
              
            }
            return searchList;
        }
        public async Task<List<Film>> FilterFilmForApi(string? language, string? year, int? vote)
        {
           

            var url_ = $"https://api.themoviedb.org/3/search/movie?api_key={_configuration["TMDB:ApiKey"]}";
            if (language != null)
            {
                url_ += $"&original_language={language}";
            }
            if (year!=null)
            {
                url_ += $"&release_date={year}";
            }
            if (vote.HasValue)
            {
                url_ += $"&vote_average={vote}";
            }

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetStringAsync(url_);
                var data = JsonConvert.DeserializeObject<TmdbResponse>(response);
                return data.Results?? new List<Film>();
            }
        }
    }
}
