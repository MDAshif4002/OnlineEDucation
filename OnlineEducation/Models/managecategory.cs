using System.ComponentModel.DataAnnotations;

namespace OnlineEducation.Models
{
    public class managecategory
    {
        [Key]
        public int id { get; set; }
        public string? name { get; set; }
        public string? price { get; set; }
        public string? description { get; set; }
        public string? image { get; set; }
        public string? categoryid { get; set; }
    }
}
