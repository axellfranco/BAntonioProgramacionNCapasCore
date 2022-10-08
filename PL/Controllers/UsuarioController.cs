using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;

namespace PL.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;


        public UsuarioController(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        //Consumir servicios en MVC

        [HttpGet]//ID=01GETALL
        public ActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();
            ML.Result resultusuario = new ML.Result();

            ML.Result resultList = BL.Usuario.GetAll(usuario);

            resultusuario.Objects = new List<Object>();

            using (var client = new HttpClient())
            {

                client.BaseAddress =new Uri (_configuration["WebAPI"]);

                var responseTask = client.GetAsync("GetAll");//Devuelve una respuesta 200
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    foreach (var resultItem in readTask.Result.Objects)
                    {
                        ML.Usuario resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(resultItem.ToString());
                        resultusuario.Objects.Add(resultItemList);
                    }
                }
                usuario.UsuarioList = resultList.Objects;
            }
            return View(usuario);
        }


        [HttpGet] //ID=02FORM-Add/Update
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
                //return View("GetAll");
            }
            else 
            {

                ML.Result result = BL.Usuario.GetById(IdUsuario.Value); 

           

                if (result.Correct)
                {

                    usuario = ((ML.Usuario)result.Object);


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


        [HttpPost] //ID=03 ADD-UPDATE
        public ActionResult Form(ML.Usuario usuario)
        {
            
            IFormFile image = Request.Form.Files["IFImage"];
            //if (ModelState.IsValid)
            //{

            if (image != null)
            {
                //llamar al metodo que convierte a bytes la imagen
                byte[] ImagenBytes = ConvertToBytes(image);
                //convierto a base 64 la imagen y la guardo en mi objeto materia
                usuario.Imagen = Convert.ToBase64String(ImagenBytes);
            }

            if (usuario.IdUsuario == null)
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(_configuration["WebAPI"]);

                    var responseTask = client.PostAsJsonAsync<ML.Usuario>("Add",usuario);//Devuelve una respuesta 200
                    responseTask.Wait();

                    var resultTask = responseTask.Result;

                    if (resultTask.IsSuccessStatusCode)
                    {
                        return RedirectToAction("GetAll");
                    }

                }

                
            }
            else
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(_configuration["WebAPI"]);

                    var responseTask = client.PostAsJsonAsync<ML.Usuario>("Update", usuario);//Devuelve una respuesta 200
                    responseTask.Wait();

                    var resultTask = responseTask.Result;

                    if (resultTask.IsSuccessStatusCode)
                    {
                        return RedirectToAction("GetAll");
                    }

                }

                //ML.Result result = new ML.Result();
                //result = BL.Usuario.Update(usuario);

            }
           
            return View("GetAll");
        }


        [HttpGet] //ID=04Delete 
        public ActionResult Delete(ML.Usuario usuario)
        {
            ML.Result resultusuario = new ML.Result();
            int IdUsuario = usuario.IdUsuario.Value;

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(_configuration["WebAPI"]);

                var postTask = client.DeleteAsync("Delete/"+IdUsuario);//Devuelve una respuesta 200
                postTask.Wait();

                var resultTask = postTask.Result;

                if (resultTask.IsSuccessStatusCode)
                {
                    resultusuario = BL.Usuario.GetAll(usuario);
                    return RedirectToAction("GetAll",resultusuario);
                }
               

            }
            //resultusuario = BL.Usuario.GetAll(usuario);
            return View("GetAll",usuario);
        }

        //Consumir servicios en MVC



        //METODOS 
        [HttpGet] //ID=01GETALL 
        public ActionResult GetAll1() //Si se desea utilizar cambiar el nombre
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



        [HttpGet]//ID=02FORM-Add/Update
        public ActionResult Form1(int? IdUsuario)

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

                    usuario = (ML.Usuario)result.Object;//unboxing 


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


        [HttpGet] //ID=04Delete 
        public ActionResult Delete1(ML.Usuario usuario)
        {
            ML.Result result = BL.Usuario.Delete(usuario);

            return View();
        }

        [HttpPost] //ID=03 ADD-UPDATE
        public ActionResult Form1(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            IFormFile image = Request.Form.Files["IFImage"];
            //if (ModelState.IsValid)
            //{

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
            //}
            //falta else
            return View("Modal");
        }


        public JsonResult GetEstado(int IdPais)
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

        //Estatus Toggle

        public ActionResult Estatus(int? IdUsuario)
        {

            ML.Result result = BL.Usuario.GetById(IdUsuario.Value);

            if (result.Correct)
            {
                ML.Usuario usuario = new ML.Usuario();

                usuario = ((ML.Usuario)result.Object);
                usuario.Estatus = (usuario.Estatus) ? false : true;

                result = BL.Usuario.Update(usuario);
            }
            else
            {

            }

            return PartialView("Modal");
        }
    }
}
