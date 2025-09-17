using Microsoft.AspNetCore.Mvc;
using MantenimientoAPI.Models;

namespace MantenimientoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MantenimientosController : ControllerBase
    {
        // Acá se guardan los mantenimientos en memoria mientras no hay base de datos
        private static List<Mantenimiento> mantenimientos = new List<Mantenimiento>();
        private static int nextId = 1;

        // Simulación de usuarios válidos
        private static List<string> usuariosValidos = new List<string> { "Diego", "Pedro", "Maria" };

        [HttpPost]
        public IActionResult CrearMantenimiento([FromBody] Mantenimiento nuevo)
        {
            if (string.IsNullOrWhiteSpace(nuevo.Equipo) ||
                string.IsNullOrWhiteSpace(nuevo.NumeroSerie) ||
                string.IsNullOrWhiteSpace(nuevo.Tipo) ||
                string.IsNullOrWhiteSpace(nuevo.Descripcion) ||
                string.IsNullOrWhiteSpace(nuevo.Usuario))
            {
                return BadRequest(new { mensaje = "Debe completar todos los campos." });
            }

            // El nombre del equipo no puede superar los 30 caracteres
            if (nuevo.Equipo.Length > 30)
            {
                return BadRequest(new { mensaje = "El nombre del equipo no puede superar los 30 caracteres." });
            }
            // Número de serie no puede tener espacios
            if (nuevo.NumeroSerie.Contains(" "))
            {
                return BadRequest(new { mensaje = "El número de serie no puede tener espacios." });
            }
            // No permitir fechas futuras
            if (nuevo.Fecha > DateTime.Now)
            {
                return BadRequest(new { mensaje = "La fecha no puede ser en el futuro." });
            }
            // Descripción mínima de 10 caracteres
            if (nuevo.Descripcion.Length < 10)
            {
                return BadRequest(new { mensaje = "La descripción debe tener al menos 10 caracteres." });
            }
            // Usuario debe existir en la lista
            if (!usuariosValidos.Contains(nuevo.Usuario))
            {
                return BadRequest(new { mensaje = "El usuario no está autorizado en el sistema." });
            }

            // Parámetros que no se ingresan desde la solicitud
            nuevo.Id = nextId++;
            nuevo.Fecha = nuevo.Fecha == default ? DateTime.Now : nuevo.Fecha;

            mantenimientos.Add(nuevo);

            return Ok(nuevo);
        }

        // Mostrar los mantenimientos creados
        [HttpGet]
        public IActionResult ListarMantenimientos()
        {
            return Ok(mantenimientos);
        }
    }
}