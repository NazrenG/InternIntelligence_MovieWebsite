using Movie.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Entity.Models
{
    public class Film:IEntity
    {
     public int Id {  get; set; }
     public string? Title {  get; set; }
     public string? Description {  get; set; }
     public string? Genre {  get; set; }
     public string? Director {  get; set; }
     public decimal Rating {  get; set; }
     public int Duration {  get; set; }
     public string? PosterUrl {  get; set; }
         
 
    }
}
