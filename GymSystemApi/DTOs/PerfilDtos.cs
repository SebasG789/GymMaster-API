using System.ComponentModel.DataAnnotations;

namespace GymSystemApi.DTOs
{
    public class PerfilDtos
    {
        [Required(ErrorMessage = "La edad es obligatoria")]
        [Range(1, 100, ErrorMessage = "La edad debe estar entre 1 y 100")]
        public int Edad { get; set; }

        [Required(ErrorMessage = "El peso es obligatorio")]
        [Range(1, 500, ErrorMessage = "El peso debe estar entre 1 y 500")]
        public double Peso { get; set; }

        [Required(ErrorMessage = "La altura es obligatoria")]
        [Range(1, 300, ErrorMessage = "La altura debe estar entre 1 y 300")]
        public double Altura { get; set; }

        [Required(ErrorMessage = "El objetivo es obligatorio")]
        public string Objetivo { get; set; } = string.Empty;

        public int UsuarioId { get; set; }
    }

    // DTO para retornar el perfil
    public class PerfilResponseDto
    {
        public int Id { get; set; }
        public int Edad { get; set; }
        public double Peso { get; set; }
        public double Altura { get; set; }
        public string Objetivo { get; set; } = string.Empty;
        public int UsuarioId { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;
    }
}
