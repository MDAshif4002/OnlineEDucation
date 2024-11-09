using System.ComponentModel.DataAnnotations;

namespace OnlineEducation.Models
{
    public class slider
    {
        [Key]
        public int id {  get; set; }
        public string? image { get; set; }
    }
}
