using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cotizaciones.DBA.Models
{
    [Table("Categoria")]
    public class Category
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Column("Nombre")]
        [Required]
        public string Name { get; set; }

        [Column("Descripcion")]
        [Required]
        public string Description { get; set; }

        public ICollection<Products> Products { get; set; } = new List<Products>();
    }
}
