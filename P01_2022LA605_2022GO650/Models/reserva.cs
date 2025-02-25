using System.ComponentModel.DataAnnotations;

namespace P01_2022LA605_2022GO650.Models
{
    public class reserva
    {
        [Key]
        public int reserva_id { get; set; }
        public int usuario_id { get; set; }
        public int espacio_id { get; set; }
        public DateTime fecha_reserva {  get; set; }
        public int hora_reserva { get; set; }
        public int cantidad_horas { get; set; }
        
        public string estado {  get; set; }

    }
}
