using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class UsuarioController : Controller
    {
        [HttpGet] //Listo 
        public ActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();

            ML.Result result = BL.Usuario.GetAll(usuario);

            if (result.Correct)
            {
                usuario.UsuarioList = result.Objects;

                return View(usuario);
            }
            else
            {
                return View(usuario);
            }
        }


        [HttpPost] //Listo 
        public ActionResult GetAll(ML.Usuario usuario)
        {
            //ML.Usuario usuario = new ML.Usuario();

            ML.Result result = BL.Usuario.GetAll(usuario);

            if (result.Correct)
            {
                usuario.UsuarioList = result.Objects;

                return View(usuario);
            }
            else
            {
                return View(usuario);
            }
        }





        [HttpGet]
        public ActionResult Form(int? IdUsuario)

        {// ADD

            ML.Usuario usuario = new ML.Usuario();

            ML.Result Roles = BL.Rol.GetAllEF(); //Lista de roles 
            ML.Result Paises = BL.Pais.GetAllEF();// Lista Pais
            
            usuario.Rol = new ML.Rol();

           

            usuario.Direccion = new ML.Direccion();
            usuario.Direccion.Colonia = new ML.Colonia();
            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
            usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais(); 

            

            if (IdUsuario == null) 

            {

                usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = Paises.Objects;
                usuario.Rol.RolList = Roles.Objects;

                return View(usuario);

            }
            else //UPDATE
            {

                ML.Result result = BL.Usuario.GetById(IdUsuario.Value); //PASAR EL VALOR DE IdUsuario

                 //Roles = BL.Rol.GetbyId(usuario.Rol.IdRol);

                if (result.Correct)
                {
                    
                    usuario = ((ML.Usuario)result.Object);//unboxing 

                    
                    ML.Result resultEstado = BL.Estado.EstadoGetByIdPais(usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais);
                    ML.Result resultMunicipio = BL.Municipio.MunicipioGetByIdEstado(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
                    ML.Result resultColonias = BL.Colonia.ColoniaGetByIdMunicipio(usuario.Direccion.Colonia.Municipio.IdMunicipio);
                  

                    usuario.Rol.RolList = Roles.Objects;
                    usuario.Direccion.Colonia.Colonias = resultColonias.Objects;
                    usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipio.Objects;
                    usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;
                    usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = Paises.Objects;

                    return View(usuario);
                }
                else
                {
                    ViewBag.Mensaje = "Ocurrio un error " + result.ErrorMessage;
                }
                return View(usuario);

            }
        }


        [HttpGet] // 
        public ActionResult Delete(ML.Usuario usuario)
        {
            ML.Result result = BL.Usuario.Delete(usuario);

            return View();
        }

        [HttpPost]
        public ActionResult Form(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            IFormFile image = Request.Form.Files["IFImage"];

            if (image != null)
            {
                //llamar al metodo que convierte a bytes la imagen
                byte[] ImagenBytes = ConvertToBytes(image);
                //convierto a base 64 la imagen y la guardo en mi objeto materia
                usuario.Imagen = Convert.ToBase64String(ImagenBytes);
            }

            if (usuario.IdUsuario == null)
            {
                
                result = BL.Usuario.Add(usuario);
            }
            else
            {
                result = BL.Usuario.Update(usuario);
                
            }
            return View("Modal");
        }
        

        public JsonResult GetEstado(int IdPais) //ENTRA
        {
            var result = BL.Estado.EstadoGetByIdPais(IdPais);
            return Json(result.Objects);
        }
        public JsonResult GetMunicipio(int IdEstado)
        {
            var result = BL.Municipio.MunicipioGetByIdEstado(IdEstado);
            return Json(result.Objects);
        }
        public JsonResult GetColonia(int IdMunicipio)
        {
            var result = BL.Colonia.ColoniaGetByIdMunicipio(IdMunicipio);
            return Json(result.Objects);
        }


        //Input Imagen
        


        public static byte[] ConvertToBytes(IFormFile imagen)
        {

            using var fileStream = imagen.OpenReadStream();

            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, (int)fileStream.Length);

            return bytes;
        }
    }
}
