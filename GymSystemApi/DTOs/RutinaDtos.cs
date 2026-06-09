using System.ComponentModel.DataAnnotations;

namespace GymSystemApi.DTOs
{
    // DTO para crear o editar una rutina
    public class RutinaDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "La descripción es obligatoria")]
        public string Descripcion { get; set; } = string.Empty;

        [Range(1, 7, ErrorMessage = "Los días deben estar entre 1 y 7")]
        public int DiasSemana { get; set; }

        [Required(ErrorMessage = "El cliente es obligatorio")]
        public int ClienteId { get; set; }
    }

    // DTO para retornar una rutina con sus ejercicios
    public class RutinaResponseDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public int DiasSemana { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int ClienteId { get; set; }
        public string NombreCliente { get; set; } = string.Empty;
        public List<EjercicioResponseDto> Ejercicios { get; set; } = new();
    }

    // DTO para crear o editar un ejercicio
    public class EjercicioDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; } = string.Empty;

        [Range(1, 20, ErrorMessage = "Las series deben estar entre 1 y 20")]
        public int Series { get; set; }

        
        public string Repeticiones { get; set; } = string.Empty;

        public double PesoKg { get; set; }

        [Required(ErrorMessage = "El día es obligatorio")]
        public string Dia { get; set; } = string.Empty;

        [Required(ErrorMessage = "La rutina es obligatoria")]
        public int RutinaId { get; set; }
    }

    // DTO para retornar un ejercicio
    public class EjercicioResponseDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int Series { get; set; }
        public string Repeticiones { get; set; } = string.Empty;
        public double PesoKg { get; set; }
        public string Dia { get; set; } = string.Empty;
    }
}
