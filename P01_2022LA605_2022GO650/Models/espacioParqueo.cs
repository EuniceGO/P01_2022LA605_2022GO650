using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace P01_2022LA605_2022GO650.Models
{
    public class espacioParqueo
    {
       
            [Key]
            public int espacio_id { get; set; }

            [Required]
            public int sucursal_id { get; set; }

            [Required]
            [MaxLength(10)]
            public string numero_espacio { get; set; }

            [MaxLength(50)]
            public string ubicacion { get; set; }

            [Required]
            public decimal costo_hora { get; set; }

            [Required]
            public string estado { get; set; } = "disponible"; // Disponible por defecto

            
          
        
    }
}
