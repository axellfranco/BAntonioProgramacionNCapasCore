using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class VentaController : Controller
    {
        [HttpGet]  
        public ActionResult GetAll()
        {
            ML.Producto producto = new ML.Producto();

            //
            ML.Result Area = BL.Area.AreaGetAll();
            ML.Result Proveedor = BL.Proveedor.GetAll();
            producto.Proveedor = new ML.Proveedor();
            producto.Departamento = new ML.Departamento();
            producto.Departamento.Area = new ML.Area();
            ///

            ML.Result result = BL.Producto.GetAll(producto);

            if (result.Correct)
            {
                producto.ProductoList = result.Objects;
                //
                producto.Proveedor.Proveedores = Proveedor.Objects;
                producto.Departamento.Area.AreaList = Area.Objects;
                //
                return View(producto);
            }
            else
            {
                return View(producto);
            }
        }

        [HttpPost] //Listo 
        public ActionResult GetAll(ML.Producto producto)
        {
            //ML.Usuario usuario = new ML.Usuario();

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
    }
}
