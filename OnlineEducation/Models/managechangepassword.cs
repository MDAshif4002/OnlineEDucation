using System.ComponentModel.DataAnnotations;

namespace OnlineEducation.Models
{
    public class managechangepassword
    {
        [Key]
        public int id { get; set; }
        public string? name { get; set; }
        public string? email { get; set; }
        public string? oldpassword { get; set; }
        public string? newpassword { get; set; }
        public string? confirmpassword { get; set; }
    }
}
