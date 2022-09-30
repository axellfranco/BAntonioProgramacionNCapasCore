using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Producto
    {
        public static ML.Result Add(ML.Producto producto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.BantonioProgramacionNcapasContext context = new DL.BantonioProgramacionNcapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"ProductoAdd '{producto.Nombre}',{producto.PrecioUnitario}, {producto.Stock}, {producto.Proveedor.IdProveedor}, '{producto.Departamento.IdDepartamento}','{producto.Descripcion}','{producto.Imagen}'");

                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }

        public static ML.Result Update(ML.Producto producto)
        {
            ML.Result result = new ML.Result();

            try

            {
                using (DL.BantonioProgramacionNcapasContext context = new DL.BantonioProgramacionNcapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"ProductoUpdate {producto.IdProducto},'{producto.Nombre}', {producto.PrecioUnitario}, {producto.Stock}, {producto.Proveedor.IdProveedor},{producto.Departamento.IdDepartamento},'{producto.Descripcion}'");
                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }

            }
            catch (Exception Ex)
            {
                result.Ex = Ex;
                throw;
            }

            return result;
        }

        public static ML.Result Delete(ML.Producto producto)
        {
            ML.Result result = new ML.Result();

            try

            {
                using (DL.BantonioProgramacionNcapasContext context = new DL.BantonioProgramacionNcapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"ProductoDelete {producto.IdProducto}");

                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }

            }
            catch (Exception Ex)
            {
                result.Ex = Ex;
                throw;
            }

            return result;
        }

        public static ML.Result GetAll(ML.Producto producto)
        {
            ML.Result result = new ML.Result();

            try

            {
                using (DL.BantonioProgramacionNcapasContext context = new DL.BantonioProgramacionNcapasContext())
                {
                    var query = context.Productos.FromSqlRaw($"ProductoGetAll '{producto.Nombre}',{producto.IdProducto} ").ToList();

                    
                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            producto = new ML.Producto();

                            producto.IdProducto = (byte)obj.IdProducto;
                            producto.Nombre = obj.Nombre;
                            producto.PrecioUnitario = obj.PrecioUnitario;
                            producto.Stock = obj.Stock;

                            producto.Proveedor = new ML.Proveedor();
                            producto.Proveedor.IdProveedor = (byte)obj.IdProveedor;
                            producto.Proveedor.Nombre = obj.NombreProveedor;
                            //producto.IdProveedor = obj.IdProveedor.Value;

                            producto.Departamento = new ML.Departamento();
                            producto.Departamento.IdDepartamento = (byte)obj.IdDepartamento;
                            producto.Departamento.Nombre = obj.NombreDepartamento;
                            //producto.IdDepartamento = obj.IdDepartamento.Value;

                            producto.Descripcion = obj.Descripcion;
                            producto.Imagen = obj.Imagen;

                            result.Objects.Add(producto);


                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }

            }
            catch (Exception Ex)
            {
                result.Ex = Ex;
                throw;
            }

            return result;
        }

        public static ML.Result GetById(int Idproduct)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.BantonioProgramacionNcapasContext context = new DL.BantonioProgramacionNcapasContext())
                {
                    var query = context.Productos.FromSqlRaw($"ProductoGetById {Idproduct}").AsEnumerable().FirstOrDefault();

                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        ML.Producto producto = new ML.Producto();

                        producto.IdProducto = (byte)query.IdProducto;
                        producto.Nombre = query.Nombre;
                        producto.PrecioUnitario = query.PrecioUnitario;
                        producto.Stock = query.Stock;

                        producto.Proveedor = new ML.Proveedor();
                        producto.Proveedor.IdProveedor = (byte)query.IdProveedor;
                        producto.Proveedor.Nombre = query.NombreProveedor;
                        //producto.IdProveedor = obj.IdProveedor.Value;

                        producto.Departamento = new ML.Departamento();
                        producto.Departamento.IdDepartamento = (byte)query.IdDepartamento;
                        producto.Departamento.Nombre = query.NombreDepartamento;
                        //producto.IdDepartamento = obj.IdDepartamento.Value;

                        producto.Descripcion = query.Descripcion;
                        producto.Imagen = query.Imagen;

                        result.Object = producto;

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }

                }

            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

    }
}
