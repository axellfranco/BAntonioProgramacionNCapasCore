using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class ProductoController : Controller
    {
        [HttpGet] //Listo 
        public ActionResult GetAll()
        {
            
            ML.Producto producto = new ML.Producto();
            ML.Result result = BL.Producto.GetAll();

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
