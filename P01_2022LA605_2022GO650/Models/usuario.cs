using System.ComponentModel.DataAnnotations;

namespace P01_2022LA605_2022GO650.Models
{
    public class usuario
    {
        [Key]
        public int usuario_id {  get; set; }
        public string nombre { get; set; }
        public string correo { get; set; }
        public string telefono {  get; set; }
        public string contraseña {  get; set; }
        public string rol {  get; set; }

    }
}
