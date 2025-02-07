using Newtonsoft.Json;

namespace Movie.Webapi.Dtos
{
    public class MovieDto
    { 
        public string Title { get; set; }
         
        public string OriginalTitle { get; set; } 
        public string Overview { get; set; } 
        public string PosterPath { get; set; }
         
        public string BackdropPath { get; set; } 
        public string OriginalLanguage { get; set; } 
        public List<int> GenreIds { get; set; } 
        public double Popularity { get; set; } 
        public string ReleaseDate { get; set; }
         
        public bool Adult { get; set; }
         
        public double VoteAverage { get; set; } 
        public int VoteCount { get; set; }

        public string FullPosterUrl => string.IsNullOrEmpty(PosterPath) ? null : $"https://image.tmdb.org/t/p/w500{PosterPath}";

    }
}
