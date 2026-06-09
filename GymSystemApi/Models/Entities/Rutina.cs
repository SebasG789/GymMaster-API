namespace GymSystemApi.Models.Entities
{
    public class Rutina
    {
        // Clave primaria
        public int Id { get; set; }

        // Nombre de la rutina
        public string Nombre { get; set; } = string.Empty;

        // Descripción general
        public string Descripcion { get; set; } = string.Empty;

        // Días a la semana
        public int DiasSemana { get; set; }

        // Fecha de creación
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        // Cliente al que pertenece
        public int ClienteId { get; set; }
        public Usuario? Cliente { get; set; }

        // Relación con ejercicios
        public ICollection<Ejercicio> Ejercicios { get; set; } = new List<Ejercicio>();
    }
}
