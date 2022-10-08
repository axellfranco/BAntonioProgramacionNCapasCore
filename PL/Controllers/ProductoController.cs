using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class ProductoController : Controller
    {

        private readonly IConfiguration _configuration;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;


        public ProductoController(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        //Metodos con servicios API

        [HttpGet] //ID01GETALL 
        public ActionResult GetAll()
        {

            ML.Producto producto = new ML.Producto();
            ML.Result resultProducto = BL.Producto.GetAll(producto);




            using (var client = new HttpClient())
            {
                ML.Result resultObj = new ML.Result();
                resultObj.Objects = new List<Object>();

                client.BaseAddress = new Uri(_configuration["WebAPI"]);

                var responseTask = client.GetAsync("GetAll");//Devuelve una respuesta 200
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    foreach (var resultItem in readTask.Result.Objects)
                    {
                        ML.Producto resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Producto>(resultItem.ToString());
                        resultObj.Objects.Add(resultItemList);
                    }
                }
                producto.ProductoList = resultProducto.Objects;
            }
            return View(producto);
        }


        [HttpGet]//ID02
        public ActionResult Form(int? IdProducto)
        {
            ML.Producto producto = new ML.Producto();

            ML.Result Area = BL.Area.AreaGetAll();
            ML.Result Proveedor = BL.Proveedor.GetAll();


            producto.Proveedor = new ML.Proveedor();
            producto.Departamento = new ML.Departamento();
            producto.Departamento.Area = new ML.Area();



            if (IdProducto == null)
            {

                producto.Proveedor.Proveedores = Proveedor.Objects;//Lista de Proveedores
                producto.Departamento.Area.AreaList = Area.Objects;



                return View(producto);
            }
            else //UPDATE
            {

                ML.Result result = BL.Producto.GetById(IdProducto.Value);



                if (result.Correct)
                {

                    producto = (ML.Producto)result.Object;//unboxing 


                    ML.Result proveedores = BL.Proveedor.GetbyId(producto.Proveedor.IdProveedor);
                    ML.Result departamentos = BL.Departamento.GetbyId(producto.Departamento.IdDepartamento);

                    producto.Departamento.Departamentos = departamentos.Objects;
                    producto.Departamento.Area.AreaList = Area.Objects;

                    producto.Proveedor.Proveedores = proveedores.Objects; //REVISAR A DETALLE


                    //usuario.Rol.RolList = Roles.Objects;
                    //usuario.Direccion.Colonia.Colonias = resultColonias.Objects;
                    //usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipio.Objects;
                    //usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;
                    //usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = Paises.Objects;

                    return View(producto);
                }
                else
                {
                    ViewBag.Mensaje = "Ocurrio un error " + result.ErrorMessage;
                }
            }
            return PartialView("Modal");
        }


        [HttpPost]//ID03 "GetById"
        public ActionResult Form(ML.Producto producto)
        {
            
            IFormFile image = Request.Form.Files["IFImage"];
            if (image != null)
            {
                //llamar al metodo que convierte a bytes la imagen
                byte[] ImagenBytes = ConvertToBytes(image);
                //convierto a base 64 la imagen y la guardo en mi objeto materia
                producto.Imagen = Convert.ToBase64String(ImagenBytes);
            }

            if (producto.IdProducto == 0)
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(_configuration["WebAPI"]);

                    var responseTask = client.PostAsJsonAsync<ML.Producto>("Add",producto);//Devuelve una respuesta 200
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
                

            }
            return View(producto);
        }




        //METODOS SIN SERVICIO
        [HttpGet] //ID01GetALL 
        public ActionResult GetAll1()
        {
            
            ML.Producto producto = new ML.Producto();
            ML.Result result = BL.Producto.GetAll(producto);


            if (result.Correct)
            {

                producto.ProductoList = result.Objects;
                return View(producto);
            }
            else
            {
                return View(producto);
            }
        }


        [HttpGet]//ID02
        public ActionResult Form1(int? IdProducto)
        {
            ML.Producto producto = new ML.Producto();

            ML.Result Area = BL.Area.AreaGetAll();
            ML.Result Proveedor = BL.Proveedor.GetAll();


            producto.Proveedor= new ML.Proveedor();
            producto.Departamento = new ML.Departamento();
            producto.Departamento.Area = new ML.Area();
            


            if (IdProducto == null)
            {

                producto.Proveedor.Proveedores = Proveedor.Objects;//Lista de Proveedores
                producto.Departamento.Area.AreaList = Area.Objects;
               
               

                return View(producto);
            }
            else //UPDATE
            {

                ML.Result result = BL.Producto.GetById(IdProducto.Value); 

                

                if (result.Correct)
                {

                    producto = (ML.Producto)result.Object;//unboxing 
                   

                    ML.Result proveedores = BL.Proveedor.GetbyId(producto.Proveedor.IdProveedor);
                    ML.Result departamentos = BL.Departamento.GetbyId(producto.Departamento.IdDepartamento);

                    producto.Departamento.Departamentos = departamentos.Objects;
                    producto.Departamento.Area.AreaList = Area.Objects;
                    
                    producto.Proveedor.Proveedores = proveedores.Objects; //REVISAR A DETALLE


                    //usuario.Rol.RolList = Roles.Objects;
                    //usuario.Direccion.Colonia.Colonias = resultColonias.Objects;
                    //usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipio.Objects;
                    //usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;
                    //usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = Paises.Objects;

                    return View(producto);
                }
                else
                {
                    ViewBag.Mensaje = "Ocurrio un error " + result.ErrorMessage;
                }
            }
            return PartialView("Modal");
        }

        [HttpPost]//ID03 "GetById"
        public ActionResult Form1(ML.Producto producto)
        {
            ML.Result result = new ML.Result();
            IFormFile image = Request.Form.Files["IFImage"];
            if (image != null)
            {
                //llamar al metodo que convierte a bytes la imagen
                byte[] ImagenBytes = ConvertToBytes(image);
                //convierto a base 64 la imagen y la guardo en mi objeto materia
                producto.Imagen = Convert.ToBase64String(ImagenBytes);
            }

            if (producto.IdProducto == 0)
            {

                result = BL.Producto.Add(producto);
            }
            else
            {
                result = BL.Producto.Update(producto);

            }
            return View(producto);
        }

        [HttpGet] // DELETE
        public ActionResult Delete(ML.Producto producto)
        {
            ML.Result result = BL.Producto.Delete(producto);

            return View();
        }

        public static byte[] ConvertToBytes(IFormFile imagen)
        {

            using var fileStream = imagen.OpenReadStream();

            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, (int)fileStream.Length);

            return bytes;
        }

        public JsonResult GetDepartamento(int IdArea)
        {
            var result = BL.Departamento.DepartamentoGetByArea(IdArea);
            return Json(result.Objects);
        }

    }
}
