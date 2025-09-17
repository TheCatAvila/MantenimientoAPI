namespace MantenimientoAPI.Models
{
    public class Mantenimiento
    {
        public int Id { get; set; }
        public string Equipo { get; set; } = string.Empty;
        public string NumeroSerie { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string Usuario { get; set; } = string.Empty;
    }
}