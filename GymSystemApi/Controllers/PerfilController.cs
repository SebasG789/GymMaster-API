using GymSystemApi.DTOs;
using GymSystemApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymSystemApi.Controllers
{
    [ApiController]
    [Route("api/perfil")]
    [Authorize]
    public class PerfilController : ControllerBase
    {
        private readonly PerfilService _perfilService;

        // Inyectamos el servicio de perfil
        public PerfilController(PerfilService perfilService)
        {
            _perfilService = perfilService;
        }

        // GET api/perfil/{usuarioId} — cualquier usuario autenticado
        [HttpGet("{usuarioId}")]
        public async Task<IActionResult> GetByUsuarioId(int usuarioId)
        {
            var perfil = await _perfilService.GetByUsuarioIdAsync(usuarioId);
            if (perfil == null)
                return NotFound(new { mensaje = "Perfil no encontrado" });

            return Ok(perfil);
        }

        // POST api/perfil — cualquier usuario autenticado
        [HttpPost]
        public async Task<IActionResult> Create(PerfilDtos dto)
        {
            var perfil = await _perfilService.CreateAsync(dto);
            if (perfil == null)
                return BadRequest(new { mensaje = "El usuario no existe o ya tiene un perfil" });

            return Ok(perfil);
        }

        // PUT api/perfil/{usuarioId} — cualquier usuario autenticado
        [HttpPut("{usuarioId}")]
        public async Task<IActionResult> Update(int usuarioId, PerfilDtos dto)
        {
            var resultado = await _perfilService.UpdateAsync(usuarioId, dto);
            if (!resultado)
                return NotFound(new { mensaje = "Perfil no encontrado" });

            return Ok(new { mensaje = "Perfil actualizado correctamente" });
        }
    }
}
