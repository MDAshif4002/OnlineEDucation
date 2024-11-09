using System.ComponentModel.DataAnnotations;

namespace OnlineEducation.Models
{
    public class purchase
    {
        [Key]
        public int id { get; set; }
        public string? name { get; set; }
        public string? email { get; set; }
        public string? mobile { get; set; }
        public string? city { get; set; }
        public string? pincode { get; set; }
        public string? address { get; set; }
        public string? paymentmode { get; set; }
        public string? userid { get; set; }
    }
}
