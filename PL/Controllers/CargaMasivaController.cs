using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class CargaMasivaController : Controller
    {

        private readonly IConfiguration _configuration;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;


        public CargaMasivaController(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }



        [HttpGet]
        public ActionResult CargaMasiva()
        {
            ML.Result result = new ML.Result();
            //ML.Usuario usuario = new ML.Usuario();

            return View(result);
        }


        [HttpPost]
        public ActionResult CargaMasiva(ML.Usuario usuario)
        {
            IFormFile excelCargaMasiva = Request.Form.Files["FileExcel"];

            if (HttpContext.Session.GetString("PathArchivo") == null)
            {
                if (excelCargaMasiva.Length > 0)
                {
                    string fileName = Path.GetFileName(excelCargaMasiva.FileName);
                    string folderPath = _configuration["PathFolder:value"];
                    string extensionArchivo = Path.GetExtension(excelCargaMasiva.FileName).ToLower();
                    string extensionModulo = _configuration["TipoExcel"];

                    if (extensionArchivo == extensionModulo)
                    {
                        string filePath = Path.Combine(_hostingEnvironment.ContentRootPath, folderPath, Path.GetFileName(fileName)) + '-' + DateTime.Now.ToString("yyyymmddhhmmss") + ".xlsx";

                        if (!System.IO.File.Exists(filePath))
                        {
                            using (FileStream stream = new FileStream(filePath, FileMode.Create))
                            {
                                excelCargaMasiva.CopyTo(stream);
                            }

                            string connectionString = _configuration["ConnectionStringExcel:value"] + filePath;

                            ML.Result resultUsuario = BL.Usuario.ConvertirExceltoDataTable(connectionString);

                            if (resultUsuario.Correct)
                            {
                                ML.Result resultValidacion = BL.Usuario.ValidarExcel(resultUsuario.Objects);

                                if (resultValidacion.Objects.Count==0)
                                {
                                    resultValidacion.Correct = true;
                                    HttpContext.Session.SetString("PathArchivo", filePath);

                                }
                                return View(resultValidacion);
                            }

                        }
                    }
                }
            }
            else
            {
                string rutaArchivoExcel = HttpContext.Session.GetString("PathArchivo");
                string connectionString = _configuration["ConnectionStringExcell:value"] + rutaArchivoExcel;

                ML.Result resultData = BL.Usuario.ConvertirExceltoDataTable(connectionString);

                if (resultData.Correct)
                {
                    ML.Result resultErrores = new ML.Result();
                    resultErrores.Objects = new List<object>();

                    foreach (ML.Usuario usuarioItem in resultData.Objects)
                    {
                        ML.Result resultAdd = BL.Usuario.Add(usuarioItem);

                        if (!resultAdd.Correct)
                        {
                            resultErrores.Objects.Add("No se inserto el Usuario con nombre:" + usuarioItem.Nombre +resultAdd.ErrorMessage);
                        }
                    }

                    if (resultErrores.Objects.Count >0)
                    {
                        string fileError = Path.Combine(_hostingEnvironment.WebRootPath, @"C:\Users\Alien8\Documents\Brandon Axell Antonio Franco\BAntonioProgramacionNCapasCore\Errores.txt");
                        using (StreamWriter writer = new StreamWriter(fileError))
                        {
                            foreach (string ln in resultErrores.Objects)
                            {
                                writer.WriteLine(ln);
                            }
                        }
                    }
                }
                
            }

            return PartialView("Modal");
        }
    }
}
