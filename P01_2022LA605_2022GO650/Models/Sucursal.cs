using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace P01_2022LA605_2022GO650.Models
{
    public class Sucursal
    {
        [Key]
        public int sucursal_id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [Required]
        [MaxLength(255)]
        public string Direccion { get; set; }

        [MaxLength(20)]
        public string Telefono { get; set; }

 
        public int? administrador_id { get; set; } // Puede ser null si aún no tiene admin

        public int num_espacios { get; set; }

    }
}
