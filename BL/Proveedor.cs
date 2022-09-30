using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Proveedor
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try

            {
                using (DL.BantonioProgramacionNcapasContext context = new DL.BantonioProgramacionNcapasContext())
                {
                    //var query = context.Usuarios.FromSqlRaw($"GetById {IdUser}");
                    var query = context.Proveedors.FromSqlRaw($"ProveedorGetAll").ToList();

                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach (var obj in query)
                        {

                            ML.Proveedor proveedor = new ML.Proveedor();

                            proveedor.IdProveedor = (byte)obj.IdProveedor;
                            proveedor.Nombre = obj.Nombre;
                            proveedor.Telefono = obj.Telefono;

                            result.Objects.Add(proveedor);
                            
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron registros";
                    }
                }

            }
            catch (Exception Ex)
            {
                result.Correct = false;
                result.ErrorMessage = Ex.Message;

            }

            return result;
        }

        public static ML.Result GetbyId(int IdProveedor)
        {
            ML.Result result = new ML.Result();

            try

            {
                using (DL.BantonioProgramacionNcapasContext context = new DL.BantonioProgramacionNcapasContext())
                {
                    var query = context.Proveedors.FromSqlRaw($"ProveedorGetById {IdProveedor}").ToList();

                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var obj in query)
                        {
                            ML.Proveedor proveedor = new ML.Proveedor();

                            proveedor.IdProveedor = (byte)obj.IdProveedor;
                            proveedor.Nombre = obj.Nombre;
                            proveedor.Telefono = obj.Telefono;

                            result.Objects.Add(proveedor);

                            result.Correct = true;

                        }

                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron registros";
                    }
                }

            }
            catch (Exception Ex)
            {
                result.Correct = false;
                result.ErrorMessage = Ex.Message;

            }

            return result;
        }
    }
}
