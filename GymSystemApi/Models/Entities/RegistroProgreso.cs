namespace GymSystemApi.Models.Entities
{
    public class RegistroProgreso
    {
        // Clave primaria
        public int Id { get; set; }

        // Fecha del registro
        public DateTime Fecha { get; set; } = DateTime.UtcNow;

        // Peso actual del cliente
        public double Peso { get; set; }

        // Notas del entrenador o cliente
        public string Notas { get; set; } = string.Empty;

        // Cliente al que pertenece el registro
        public int ClienteId { get; set; }
        public Usuario? Cliente { get; set; }
    }
}
