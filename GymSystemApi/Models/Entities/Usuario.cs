namespace GymSystemApi.Models.Entities
{
    public class Usuario
    {
        // Clave primaria
        public int Id { get; set; }

        // Nombre completo del usuario
        public string Nombre { get; set; } = string.Empty;

        // Email único para login
        public string Email { get; set; } = string.Empty;

        // Contraseña encriptada con BCrypt
        public string PasswordHash { get; set; } = string.Empty;

        // Rol: Admin, Entrenador o Cliente
        public string Rol { get; set; } = string.Empty;

        // Fecha de registro
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;

        // Relación: un usuario puede tener un perfil
        public PerfilCliente? Perfil { get; set; }
    }

}
