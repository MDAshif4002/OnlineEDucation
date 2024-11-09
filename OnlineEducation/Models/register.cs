using System.ComponentModel.DataAnnotations;

namespace OnlineEducation.Models
{
    public class register
    {
        [Key]
        public int id { get; set; }
        public string? name { get; set; }
        public string? email { get; set; }
        public string? mobile { get; set; }
        public string? city { get; set; }
        public string? password { get; set; }
        public bool? deleteduser { get; set; }
    }
}
