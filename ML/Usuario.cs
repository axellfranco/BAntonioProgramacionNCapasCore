using System.ComponentModel.DataAnnotations;

namespace ML
{
    public class Usuario
    {
        ML.Result result= new ML.Result();
        public int? IdUsuario { get; set; }


        [Display(Name = "Usuario")]
        public string? UserName { get; set; }



        [Display(Name = "Nombre")]
        //[Required(ErrorMessage = "Ingrese el Nombre"), MaxLength(50)]
        [RegularExpression(@"^[A-Za-z]\w{1,50}$", ErrorMessage = "Nombre no valido")]
        public string? Nombre { get; set; }


        [Display(Name = "Apellido Paterno")]
        //[Required(ErrorMessage = "Ingrese el Apellido Paterno"), MaxLength(50)]
        [RegularExpression(@"^[A-Za-z]\w{1,50}$", ErrorMessage = "Apellido no valido")]
        public string? ApellidoPaterno { get; set; }




        [Display(Name = "Apellido Materno")]
        //[Required(ErrorMessage = "Ingrese el Apellido Materno"), MaxLength(50)]
        [RegularExpression(@"^[A-Za-z]\w{1,50}$", ErrorMessage = "Apellido no valido")]
        public string? ApellidoMaterno { get; set; }


        [Display(Name = "Correo electronico")]
        //[Required(ErrorMessage = "Ingrese el Correo "), MaxLength(50)]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Correo no valido")]
        public string? Email { get; set; }


        [Display(Name = "Contraseña")]
        //[Required(ErrorMessage = "Ingrese una contraseña "), MaxLength(50)]
        public string? Password { get; set; }



        [Display(Name = "Fecha de Nacimiento")]//PENDIENTE
        
        public string? FechaNacimiento { get; set; }


        [Display(Name = "Sexo")]
        public string? Sexo { get; set; }


        [Display(Name = "Telefono")]
        [StringLength(10, ErrorMessage = "Se ingresa solamente 10 numeros")]
        public string? Telefono { get; set; }


        [Display(Name = "Celular")]
        [StringLength(10, ErrorMessage = "Se ingresa solamente 10 numeros")]
        public string? Celuar { get; set; }


        [Display(Name = "CURP")]
        public string? Curp { get; set; }

        [Display(Name = "Imagen")]
        public string? Imagen { get; set; }


        [Display(Name = "Estatus")]
        public bool Estatus { get; set; }
        

        public ML.Rol? Rol { get; set; }

        public List<object>? UsuarioList { get; set; }

        public ML.Pais? Pais { get; set; }
        public ML.Direccion? Direccion { get; set; }

        
    }
}