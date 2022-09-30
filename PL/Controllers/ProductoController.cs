using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class ProductoController : Controller
    {
        [HttpGet] // 
        public ActionResult GetAll()
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


        [HttpGet]
        public ActionResult Form(int? IdProducto)
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

                    producto = ((ML.Producto)result.Object);//unboxing 

                    ML.Result proveedores = BL.Proveedor.GetbyId(producto.Proveedor.IdProveedor);
                    //ML.Result resultEstado = BL.Estado.EstadoGetByIdPais(usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais);
                    ML.Result departamento = BL.Departamento.GetbyId(producto.Departamento.IdDepartamento);
                    

                    return View(producto);
                }
                else
                {
                    ViewBag.Mensaje = "Ocurrio un error " + result.ErrorMessage;
                }
            }
            return PartialView("Modal");
        }

        [HttpPost]
        public ActionResult Form(ML.Producto producto)
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
            return View("Modal");
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
