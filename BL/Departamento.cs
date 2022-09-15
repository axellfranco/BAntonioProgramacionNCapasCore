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

        //public static ML.Result GetAllEF()
        //{
        //    ML.Result result = new ML.Result();
        //    ML.Departamento departamento = new ML.Departamento();
        //    try

        //    {
        //        using (DL.BantonioProgramacionNcapasContext context = new DL.BantonioProgramacionNcapasContext())
        //        {
        //            var query = context.Usuarios.FromSqlRaw($"DepartamentoGetAll {departamento.IdDepartamento},'{departamento.Nombre}',{departamento.Area.IdArea}").ToList();

                    
        //            result.Objects = new List<object>();
        //            if (query != null)
        //            {
        //                foreach (var obj in query)
        //                {

        //                    departamento = new ML.Departamento();


        //                    departamento.IdDepartamento = obj;
        //                    departamento.Nombre = obj.Nombre;


        //                    departamento.Area = new ML.Area();
        //                    departamento.Area.IdArea = (byte)obj.IdArea;

        //                    result.Objects.Add(departamento);
        //                }
        //                result.Correct = true;
        //            }
        //            else
        //            {
        //                result.Correct = false;
        //            }
        //        }

        //    }
        //    catch (Exception Ex)
        //    {
        //        result.Ex = Ex;
        //        throw;
        //    }

        //    return result;
        //}

        //public static ML.Result GetByIdEF(ML.Departamento departamento)
        //{
        //    ML.Result result = new ML.Result();

        //    try
        //    {
        //        using (DL.BantonioProgramacionNcapasContext context = new DL.BantonioProgramacionNcapasContext())
        //        {

        //            var query = context.Usuarios.FromSqlRaw($"DepartamentoGetById {departamento.IdDepartamento}").AsEnumerable().FirstOrDefault();


        //            result.Objects = new List<object>();

        //            if (query != null)
        //            {
        //                ML.Departamento departamentos = new ML.Departamento();

        //                departamentos.IdDepartamento = ((byte)query.IdDepartamento);
        //                departamentos.Nombre = query.Nombre;

        //                departamentos.Area = new ML.Area();
        //                departamentos.Area.IdArea = (byte)query.IdArea;

        //                result.Object = departamentos;

        //                result.Correct = true;
        //            }
        //            else
        //            {
        //                result.Correct = false;
        //            }

        //        }

        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //    return result;
        //}

    }


}
