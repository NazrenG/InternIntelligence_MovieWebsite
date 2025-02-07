using Movie.Core.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Entities.Models
{
    public class Film : IEntity
    {


            [JsonProperty("id")]
            public int Id { get; set; }

            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("original_title")]
            public string OriginalTitle { get; set; }

            [JsonProperty("overview")]
            public string Overview { get; set; }

            [JsonProperty("poster_path")]
            public string PosterPath { get; set; }

            [JsonProperty("backdrop_path")]
            public string BackdropPath { get; set; }

            [JsonProperty("original_language")]
            public string OriginalLanguage { get; set; }

            [JsonProperty("genre_ids")]
            public List<int> GenreIds { get; set; }

            [JsonProperty("popularity")]
            public double Popularity { get; set; }

            [JsonProperty("release_date")]
            public string ReleaseDate { get; set; }

            [JsonProperty("adult")]
            public bool Adult { get; set; }

            [JsonProperty("vote_average")]
            public double VoteAverage { get; set; }

            [JsonProperty("vote_count")]
            public int VoteCount { get; set; }
         
            public string FullPosterUrl => string.IsNullOrEmpty(PosterPath) ? null : $"https://image.tmdb.org/t/p/w500{PosterPath}";
        }

 
}
