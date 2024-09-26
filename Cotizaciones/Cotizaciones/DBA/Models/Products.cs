using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cotizaciones.DBA.Models
{
    [Table("Productos")]
    public class Products
    {

        public Guid Id { get; set; } = Guid.NewGuid();

        [Column("Nombre")]
        public string Name { get; set; }

        [Column("Descripcion")]
        public String Description { get; set; }

        [Column("Precio")]
        [Required]
        public Decimal Price { get; set; }

        [Column("Categoria_id")]
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
