using System.ComponentModel.DataAnnotations.Schema;

namespace First_Proyect.Models
{
    [Table("Portafolios")]
    public class Portafolio
    {
        public string AppUserId { get; set; }
        public int StockId { get; set; }
        public AppUser AppUser { get; set; }
        public Stock Stock { get; set; }
    }
}
