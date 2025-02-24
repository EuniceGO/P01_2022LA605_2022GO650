using System.ComponentModel.DataAnnotations;

namespace P01_2022LA605_2022GO650.Models
{
    public class reserva
    {
        [Key]
        public int reserva_id {  get; set; }
        public int usuario_id {  get; set; }
    }
}
