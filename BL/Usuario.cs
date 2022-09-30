using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.OleDb;

namespace BL
{
    public class Usuario
    {
        public static ML.Result Add(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();


            try
            {
                using (DL.BantonioProgramacionNcapasContext context = new DL.BantonioProgramacionNcapasContext())
                {
                    usuario.Estatus = true;
                    var query = context.Database.ExecuteSqlRaw($"UsuarioAdd '{usuario.UserName}','{usuario.Nombre}', '{usuario.ApellidoPaterno}', '{usuario.ApellidoMaterno}', '{usuario.Email}','{usuario.Password}', '{usuario.FechaNacimiento}','{usuario.Sexo}','{usuario.Telefono}', '{usuario.Celuar}','{usuario.Curp}',{usuario.Rol.IdRol},'{usuario.Imagen}',{usuario.Estatus},'{usuario.Direccion.Calle}','{usuario.Direccion.NumeroInterior}','{usuario.Direccion.NumeroExterior}',{usuario.Direccion.Colonia.IdColonia}");


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

        public static ML.Result Update(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try

            {
                using (DL.BantonioProgramacionNcapasContext context = new DL.BantonioProgramacionNcapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"UsuarioUpdate {usuario.IdUsuario}'{usuario.UserName}','{usuario.Nombre}', '{usuario.ApellidoPaterno}', '{usuario.ApellidoMaterno}', '{usuario.Email}','{usuario.Password}', '{usuario.FechaNacimiento}','{usuario.Sexo}','{usuario.Telefono}', '{usuario.Celuar}','{usuario.Curp}',{usuario.Rol.IdRol},'{usuario.Imagen}',{usuario.Estatus},'{usuario.Direccion.Calle}','{usuario.Direccion.NumeroInterior}','{usuario.Direccion.NumeroExterior}',{usuario.Direccion.Colonia.IdColonia}");
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

        public static ML.Result Delete(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try

            {
                using (DL.BantonioProgramacionNcapasContext context = new DL.BantonioProgramacionNcapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"UsuarioDelete {usuario.IdUsuario}");
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

        public static ML.Result GetAll(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try

            {
                using (DL.BantonioProgramacionNcapasContext context = new DL.BantonioProgramacionNcapasContext())
                {
                    var query = context.Usuarios.FromSqlRaw($"UsuarioGetAll '{usuario.Nombre}','{usuario.ApellidoPaterno}','{usuario.ApellidoMaterno}'").ToList();

                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            usuario = new ML.Usuario();

                            usuario.IdUsuario = obj.IdUsuario;
                            usuario.UserName = obj.UserName;
                            usuario.Nombre = obj.Nombre;
                            usuario.ApellidoPaterno = obj.ApellidoPaterno;
                            usuario.ApellidoMaterno = obj.ApellidoMaterno;
                            usuario.Email = obj.Email;
                            usuario.Password = obj.Password;
                            usuario.FechaNacimiento = obj.FechaNacimiento.ToString();
                            usuario.Sexo = obj.Sexo;
                            usuario.Telefono = obj.Telefono;
                            usuario.Celuar = obj.Celular;
                            usuario.Curp = obj.Curp;
                            usuario.Imagen = obj.Imagen;
                            usuario.Estatus = obj.Estatus.Value;

                            usuario.Rol = new ML.Rol();
                            usuario.Rol.IdRol = obj.IdRol.Value;
                            usuario.Rol.Nombre = obj.NombreRol;


                            usuario.Direccion = new ML.Direccion();
                            usuario.Direccion.IdDireccion = obj.IdDireccion.Value;
                            usuario.Direccion.Calle = obj.DireccionCalle;
                            usuario.Direccion.NumeroInterior = obj.NumeroInterior;
                            usuario.Direccion.NumeroExterior = obj.NumeroExterior;


                            usuario.Direccion.Colonia = new ML.Colonia();
                            usuario.Direccion.Colonia.IdColonia = obj.IdColonia;
                            usuario.Direccion.Colonia.Nombre = obj.NombreColonia;
                            usuario.Direccion.Colonia.CodigoPostal = obj.CodigoPostal;

                            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                            usuario.Direccion.Colonia.Municipio.IdMunicipio = obj.IdMunicipio;
                            usuario.Direccion.Colonia.Municipio.Nombre = obj.MunicipioNombre;

                            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                            usuario.Direccion.Colonia.Municipio.Estado.IdEstado = obj.IdEstado;
                            usuario.Direccion.Colonia.Municipio.Estado.Nombre = obj.EstadoNombre;

                            usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
                            usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais = obj.IdPais;
                            usuario.Direccion.Colonia.Municipio.Estado.Pais.Nombre = obj.PaisNombre;



                            result.Objects.Add(usuario);
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
                result.Correct = false;
                result.Ex = Ex;
                result.ErrorMessage = Ex.Message;
            }

            return result;
        } //Listo

        public static ML.Result GetById(int IdUser)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.BantonioProgramacionNcapasContext context = new DL.BantonioProgramacionNcapasContext())
                {
                    var query = context.Usuarios.FromSqlRaw($"UsuarioGetById {IdUser}").AsEnumerable().FirstOrDefault();


                    if (query != null)
                    {

                        ML.Usuario usuario = new ML.Usuario();

                        usuario.IdUsuario = query.IdUsuario;
                        usuario.UserName = query.UserName;
                        usuario.Nombre = query.Nombre;
                        usuario.ApellidoPaterno = query.ApellidoPaterno;
                        usuario.ApellidoMaterno = query.ApellidoMaterno;
                        usuario.Email = query.Email;
                        usuario.Password = query.Password;
                        usuario.FechaNacimiento = query.FechaNacimiento.ToString();
                        usuario.Sexo = query.Sexo;
                        usuario.Telefono = query.Telefono;
                        usuario.Celuar = query.Celular;
                        usuario.Curp = query.Curp;
                        usuario.Imagen = query.Imagen;
                        usuario.Estatus = query.Estatus.Value;

                        usuario.Rol = new ML.Rol();
                        usuario.Rol.IdRol = query.IdRol.Value;
                        usuario.Rol.Nombre = query.NombreRol;

                        usuario.Direccion = new ML.Direccion();
                        usuario.Direccion.IdDireccion = query.IdDireccion.Value;
                        usuario.Direccion.Calle = query.DireccionCalle;
                        usuario.Direccion.NumeroInterior = query.NumeroInterior;
                        usuario.Direccion.NumeroExterior = query.NumeroExterior;

                        usuario.Direccion.Colonia = new ML.Colonia();
                        usuario.Direccion.Colonia.IdColonia = query.IdColonia;
                        usuario.Direccion.Colonia.Nombre = query.NombreColonia;
                        usuario.Direccion.Colonia.CodigoPostal = query.CodigoPostal;

                        usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                        usuario.Direccion.Colonia.Municipio.IdMunicipio = query.IdMunicipio;
                        usuario.Direccion.Colonia.Municipio.Nombre = query.MunicipioNombre;

                        usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                        usuario.Direccion.Colonia.Municipio.Estado.IdEstado = query.IdEstado;
                        usuario.Direccion.Colonia.Municipio.Estado.Nombre = query.EstadoNombre;

                        usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
                        usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais = query.IdPais;
                        usuario.Direccion.Colonia.Municipio.Estado.Pais.Nombre = query.PaisNombre;

                        result.Object = usuario;

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

        public static ML.Result ConvertirExceltoDataTable(string connString)
        {
            ML.Result result= new ML.Result();

            try
            {
                using (OleDbConnection context = new OleDbConnection(connString))
                {
                    string query = "SELECT * FROM [Sheet1$]";

                    using (OleDbCommand cmd= new OleDbCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Connection = context;

                        OleDbDataAdapter da = new OleDbDataAdapter();
                        da.SelectCommand = cmd;

                        DataTable tableusuario = new DataTable();

                        da.Fill(tableusuario);


                        if(tableusuario.Rows.Count > 0)
                        {
                            result.Objects = new List<object>();

                            foreach (DataRow row in tableusuario.Rows)
                            {
                                ML.Usuario usuario = new ML.Usuario();

                               
                                usuario.UserName = row[0].ToString();
                                usuario.Nombre = row[1].ToString();
                                usuario.ApellidoPaterno = row[2].ToString();
                                usuario.ApellidoMaterno = row[3].ToString();
                                usuario.Email = row[4].ToString();
                                usuario.Password = row[5].ToString();
                                usuario.FechaNacimiento = row[6].ToString();
                                usuario.Sexo = row[7].ToString();
                                usuario.Telefono = row[8].ToString();
                                usuario.Celuar = row[9].ToString();
                                usuario.Curp = row[10].ToString();
                                
                                usuario.Imagen = null;
                                usuario.Estatus = true;

                                usuario.Rol = new ML.Rol();
                                usuario.Rol.IdRol = byte.Parse(row[11].ToString());
                                //usuario.Rol.Nombre = row[12].ToString();

                                usuario.Direccion = new ML.Direccion();
                               
                                usuario.Direccion.Calle = row[12].ToString();
                                usuario.Direccion.NumeroInterior = row[13].ToString();
                                usuario.Direccion.NumeroExterior = row[14].ToString();

                                usuario.Direccion.Colonia = new ML.Colonia();
                                usuario.Direccion.Colonia.IdColonia = byte.Parse(row[15].ToString());
                                

                                result.Objects.Add(usuario);
                            }

                            result.Correct = true;
                        }

                        result.Object = tableusuario;

                        if (tableusuario.Rows.Count > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No existe registro en el excel";
                        }
                    }

                }
            }
            catch (Exception)
            {

                result.Correct = false;
                //result.ErrorMessage = ex.Message; 
            }
            return result;
        }

        public static ML.Result ValidarExcel(List<object> Object)
        {
            ML.Result result = new ML.Result();

            try
            {
                result.Objects = new List<object>();
                //DataTable  //Rows //Columns
                int i = 1;

                foreach (ML.Usuario usuario in Object)
                {
                    ML.ErrorExcel error = new ML.ErrorExcel();
                    error.IdRegistro = i++;
                    

                    usuario.UserName = (usuario.UserName == "") ? error.Mensaje += "Ingresa el Usuario: " : usuario.UserName;
                    usuario.Nombre = (usuario.Nombre == "") ? error.Mensaje += "Ingresa el Nombre: " : usuario.Nombre;
                    usuario.ApellidoPaterno = (usuario.ApellidoPaterno == "") ? error.Mensaje += "Ingresa el Apellido Paterno : " : usuario.ApellidoPaterno;
                    usuario.ApellidoMaterno = (usuario.ApellidoMaterno == "") ? error.Mensaje += "Ingresa el Apellido Materno: " : usuario.ApellidoMaterno;
                    usuario.Email = (usuario.Email == "") ? error.Mensaje += "Ingresa el Email: " : usuario.Email;
                    usuario.Password = (usuario.Password == "") ? error.Mensaje += "Ingresa la contraseña: " : usuario.Password;
                    usuario.FechaNacimiento = (usuario.FechaNacimiento == "") ? error.Mensaje += "Ingresa la Fecha de Nacimiento: " : usuario.FechaNacimiento;
                    usuario.Sexo = (usuario.Sexo == "") ? error.Mensaje += "Ingresa el Sexo de la persona: " : usuario.Sexo;
                    usuario.Telefono = (usuario.Telefono == "") ? error.Mensaje += "Ingresa el Telefono: " : usuario.Telefono;
                    usuario.Celuar = (usuario.Celuar == "") ? error.Mensaje += "Ingresa el Celular: " : usuario.Celuar;
                    usuario.Curp = (usuario.Curp == "") ? error.Mensaje += "Ingresa la CURP: " : usuario.Curp;
                    //usuario.Imagen = (usuario.UserName == "") ? error.Mensaje += "Ingresa el Nombre: " : usuario.UserName;
                    //usuario.Estatus = (usuario.UserName == "") ? error.Mensaje += "Ingresa el Nombre: " : usuario.UserName;

                    usuario.Rol = new ML.Rol();
                    //usuario.Rol.IdRol = (usuario.Rol.IdRol == 0) ? error.Mensaje += "Ingresa el Rol" : usuario.Rol.IdRol;
                    
                    
                    usuario.Direccion = new ML.Direccion();
                    //usuario.Direccion.Calle = (usuario.Direccion.IdDireccion == 0) ? error.Mensaje += "Ingresa el Id de Direccion" :int.Parse(usuario.Direccion.IdDireccion);
                    usuario.Direccion.Calle = (usuario.Direccion.Calle == "") ? error.Mensaje += "Ingresa la Calle" : usuario.Direccion.Calle;
                    usuario.Direccion.NumeroInterior = (usuario.Direccion.NumeroInterior == "") ? error.Mensaje += "Ingresa el Numero Interior de la Calle" : usuario.Direccion.NumeroInterior;
                    usuario.Direccion.NumeroExterior = (usuario.Direccion.NumeroExterior == "") ? error.Mensaje += "Ingresa el Numero Interior Calle" : usuario.Direccion.NumeroExterior;

                    usuario.Direccion.Colonia = new ML.Colonia();
                    //usuario.Direccion.Colonia.IdColonia = (usuario.Direccion.Colonia.IdColonia == 0) ? (error.Mensaje += "Ingresa la Colonia") : usuario.Direccion.Colonia.IdColonia;

                    if (error.Mensaje != null)
                    {
                        result.Objects.Add(error);
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