using System.ComponentModel.DataAnnotations;

namespace StockControl.Models
{
    public class Part
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Parça adı gereklidir.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Stok miktarı gereklidir.")]
        public int StockQuantity { get; set; }
        public int MachineId { get; set; }
        public Machine Machine { get; set; }
    }
}
