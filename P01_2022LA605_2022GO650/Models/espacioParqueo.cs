using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace P01_2022LA605_2022GO650.Models
{
    public class espacioParqueo
    {
       
            [Key]
            public int EspacioId { get; set; }

            [Required]
            public int SucursalId { get; set; }

            [Required]
            [MaxLength(10)]
            public string NumeroEspacio { get; set; }

            [MaxLength(50)]
            public string Ubicacion { get; set; }

            [Required]
            public decimal CostoHora { get; set; }

            [Required]
            public string Estado { get; set; } = "disponible"; // Disponible por defecto

            // Relación con Sucursal
            [ForeignKey("SucursalId")]
            public Sucursal Sucursal { get; set; }
        
    }
}
