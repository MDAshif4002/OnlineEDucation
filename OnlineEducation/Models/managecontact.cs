using System.ComponentModel.DataAnnotations;

namespace OnlineEducation.Models
{
    public class managecontact
    {
        [Key]
        public int id { get; set; }
        public string? name { get; set; }
        public string? email { get; set; }
        public string? message { get; set; }
    }
}
