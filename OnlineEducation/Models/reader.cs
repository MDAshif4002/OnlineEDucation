using System.ComponentModel.DataAnnotations;

namespace OnlineEducation.Models
{
    public class reader
    {
        [Key]
        public int id { get; set; }
        public string? readerpic { get; set; }
    }
}
