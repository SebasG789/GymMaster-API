namespace GymSystemApi.Models.Entities
{
    public class Ejercicio
    {
        // Clave primaria
        public int Id { get; set; }

        // Nombre del ejercicio
        public string Nombre { get; set; } = string.Empty;

        // Series y repeticiones
        public int Series { get; set; }
        public string Repeticiones { get; set; } = string.Empty;

        // Peso usado en kg
        public double PesoKg { get; set; }

        // Día de la semana que se realiza
        public string Dia { get; set; } = string.Empty;

        // Rutina a la que pertenece
        public int RutinaId { get; set; }
        public Rutina? Rutina { get; set; }
    }
}
