using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SL_WebAPI.Controllers
{
    [Route("api/Producto/")]
    [ApiController]
    public class ProductoController : ControllerBase
    {

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            ML.Producto producto = new ML.Producto();

            ML.Result result = BL.Producto.GetAll(producto);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }


        [HttpGet]
        [Route("GetById/IdProducto")]
        public IActionResult GetById(int IdProducto)
        {

            ML.Result result = BL.Producto.GetById(IdProducto);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }


        [HttpPost]
        [Route("Add")]
        public IActionResult Add([FromBody] ML.Producto producto)
        {
            ML.Result result = BL.Producto.Add(producto);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }



        [HttpPost]
        [Route("Update/{IdProducto}")]
        public IActionResult Update(ML.Producto producto)
        {
            
            ML.Result result = BL.Producto.Update(producto);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete]
        [Route("Delete/IdUsuario")]
        public IActionResult Delete(ML.Producto producto)
        {

            ML.Result result = BL.Producto.Delete(producto);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

    }
}
