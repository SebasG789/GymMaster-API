using System.ComponentModel.DataAnnotations;

namespace GymSystemApi.DTOs
{
    // DTO para crear o editar un registro de progreso
    public class ProgresoDto
    {
        [Range(1, 500, ErrorMessage = "El peso debe estar entre 1 y 500 kg")]
        public double Peso { get; set; }

        public string Notas { get; set; } = string.Empty;

        [Required(ErrorMessage = "El cliente es obligatorio")]
        public int ClienteId { get; set; }
    }

    // DTO para retornar un registro de progreso
    public class ProgresoResponseDto
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public double Peso { get; set; }
        public string Notas { get; set; } = string.Empty;
        public int ClienteId { get; set; }
    }
}
