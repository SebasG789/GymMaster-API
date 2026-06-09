namespace GymSystemApi.Models.Entities
{
    public class PerfilCliente
    {
        // Clave primaria
        public int Id { get; set; }

        // Edad del cliente
        public int Edad { get; set; }

        // Peso en kg
        public double Peso { get; set; }

        // Altura en cm
        public double Altura { get; set; }

        // Objetivo: perder peso, ganar músculo, etc.
        public string Objetivo { get; set; } = string.Empty;

        // Clave foránea hacia Usuario
        public int UsuarioId { get; set; }

        // Navegación hacia Usuario
        public Usuario? Usuario { get; set; }
    }
}
