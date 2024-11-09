using System.ComponentModel.DataAnnotations;

namespace OnlineEducation.Models
{
    public class manageuser
    {
        [Key]
        public int sr { get; set; }
        public string? name { get; set; }
        public string? email { get; set; }
        public string? mobile { get; set; }
        public string? city { get; set; }
        public string? password { get; set; }
    }
}
