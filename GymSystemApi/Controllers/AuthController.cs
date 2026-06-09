using GymSystemApi.DTOs;
using GymSystemApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace GymSystemApi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        // Inyectamos el servicio de autenticación
        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        // POST api/auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            // Llama al servicio y retorna el resultado
            var resultado = await _authService.RegisterAsync(dto);

            if (resultado == "El email ya está registrado")
                return BadRequest(new { mensaje = resultado });

            return Ok(new { mensaje = resultado });
        }

        // POST api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var token = await _authService.LoginAsync(dto);

            // Si el token es null las credenciales son incorrectas
            if (token == null)
                return Unauthorized(new { mensaje = "Credenciales incorrectas" });

            return Ok(new { token });
        }
    }
}
