using System;
using System.Collections.Generic;

namespace DL
{
    public partial class Usuario
    {
        public Usuario()
        {
            Direccions = new HashSet<Direccion>();
        }

        public int IdUsuario { get; set; }
        public string? Nombre { get; set; }
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string? Sexo { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Celular { get; set; }
        public string? Curp { get; set; }
        public string? Imagen { get; set; }
        public string? Telefono { get; set; }
        public int? IdRol { get; set; }

        public virtual Rol? IdRolNavigation { get; set; }
        public virtual ICollection<Direccion> Direccions { get; set; }

        //rol
        public string? NombreRol { get; set; }
        //DIRECCION
        public int? IdDireccion { get; set; }
        public string? DireccionCalle { get; set; }
        public string? NumeroInterior { get; set; }
        public string? NumeroExterior { get; set; }

        //Colonia
        public int IdColonia { get; set; }
        public string? NombreColonia { get; set; }

        public string? CodigoPostal { get; set; }

        //Municipio
        public int IdMunicipio { get; set; }

        public string? MunicipioNombre { get; set; }

        //Estado
        public int IdEstado { get; set; }
        public string? EstadoNombre { get; set; }

        //Pais
        public int IdPais { get; set; }
        public string? PaisNombre { get; set; }
    }
}
