using System.ComponentModel.DataAnnotations;

namespace OnlineEducation.Models
{
    public class managecourse
    {
        [Key]
        public int id { get; set; }
        public string? coursepic { get; set; }
        public string? title { get; set; }
        public string? coursedetail { get; set; }
        public string? price { get; set; }
    }
}
