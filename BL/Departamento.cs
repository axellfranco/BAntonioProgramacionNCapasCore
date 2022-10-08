using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Departamento
    {
        public static ML.Result AddEF(ML.Departamento departamento)
        {
            ML.Result result = new ML.Result();
            //usuario.Rol = new ML.Rol();

            try
            {
                using (DL.BantonioProgramacionNcapasContext context = new DL.BantonioProgramacionNcapasContext())
                {

                    var query = context.Database.ExecuteSqlRaw($"DepartamentoAdd '{departamento.Nombre}',{departamento.Area.IdArea}");



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

        public static ML.Result UpdateEF(ML.Departamento departamento)
        {
            ML.Result result = new ML.Result();

            try

            {
                using (DL.BantonioProgramacionNcapasContext context = new DL.BantonioProgramacionNcapasContext())
                {

                    var query = context.Database.ExecuteSqlRaw($"DepartamentoUpdate {departamento.IdDepartamento} '{departamento.Nombre}',{departamento.Area.IdArea}");

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

        public static ML.Result DeleteEF(ML.Departamento departamento)
        {
            ML.Result result = new ML.Result();

            try

            {
                using (DL.BantonioProgramacionNcapasContext context = new DL.BantonioProgramacionNcapasContext())
                {

                    var query = context.Database.ExecuteSqlRaw($"DepartamentoDelete {departamento.IdDepartamento} ");

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

        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try

            {
                using (DL.BantonioProgramacionNcapasContext context = new DL.BantonioProgramacionNcapasContext())
                {

                    var query = context.Departamentos.FromSqlRaw($"DepartamentoGetAll").ToList();

                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach (var obj in query)
                        {

                            ML.Departamento departamento = new ML.Departamento();

                            departamento.IdDepartamento = ((byte)obj.IdDepartamento);
                            departamento.Nombre = obj.Nombre;


                            result.Objects.Add(departamento);
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

        public static ML.Result GetbyId(int IdDepartamento)
        {
            ML.Result result = new ML.Result();

            try

            {
                using (DL.BantonioProgramacionNcapasContext context = new DL.BantonioProgramacionNcapasContext())
                {
                    var query = context.Departamentos.FromSqlRaw($"DepartamentoGetByIdArea {IdDepartamento}").AsEnumerable().FirstOrDefault();

                    if (query != null)
                    {

                        ML.Departamento departamento = new ML.Departamento();

                        departamento.IdDepartamento = (byte)query.IdDepartamento;
                        departamento.Nombre = query.Nombre;

                        departamento.Area = new ML.Area();
                        departamento.Area.IdArea = (byte)query.IdArea;


                        result.Object = departamento;


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

        public static ML.Result DepartamentoGetByArea(int IdDepartamento)
        {
            ML.Result result = new ML.Result();

            try

            {
                using (DL.BantonioProgramacionNcapasContext context = new DL.BantonioProgramacionNcapasContext())
                {

                    var query = context.Departamentos.FromSqlRaw($"DepartamentoGetByIdArea {IdDepartamento}").ToList();

                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var obj in query)
                        {
                            ML.Departamento departamento = new ML.Departamento();
                            departamento.Area = new ML.Area();


                            departamento.IdDepartamento = (byte)obj.IdDepartamento;
                            departamento.Nombre = obj.Nombre;

                            
                            departamento.Area.IdArea = ((byte)obj.IdArea);

                            result.Objects.Add(departamento);


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

    }


}
