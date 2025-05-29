using System.ComponentModel.DataAnnotations;

namespace StockControl.Models
{
    public class Machine
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public List<Part> Parts { get; set; } = new List<Part>();
    }
}