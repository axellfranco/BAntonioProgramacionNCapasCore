namespace ML
{
    public class Usuario
    {
        public int? IdUsuario { get; set; }

        public string? UserName { get; set; }
        public string? Nombre { get; set; }

        public string? ApellidoPaterno { get; set; }

        public string? ApellidoMaterno { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public string? FechaNacimiento { get; set; }
        public string? Sexo { get; set; }
        public string? Telefono { get; set; }
        public string? Celuar { get; set; }
        public string? Curp { get; set; }

        public string? Imagen { get; set; }

        public ML.Rol? Rol { get; set; }

        public List<object>? UsuarioList { get; set; }

        public ML.Pais? Pais { get; set; }
        public ML.Direccion? Direccion { get; set; }
    }
}