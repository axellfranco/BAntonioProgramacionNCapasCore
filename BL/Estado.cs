using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Estado
    {
        public static ML.Result EstadoGetByIdPais(int IdEstado)
        {
            ML.Result result = new ML.Result();

            try

            {
                using (DL.BantonioProgramacionNcapasContext context = new DL.BantonioProgramacionNcapasContext())
                {
                    //var query = context.EstadoGetByIdPais(IdEstado)
                    var query = context.Estados.FromSqlRaw($"EstadoGetByIdPais {IdEstado}").ToList();

                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var obj in query)
                        {
                            
                            ML.Estado estado = new ML.Estado();
                            estado.Pais = new ML.Pais();

                            estado.IdEstado = obj.IdEstado;
                            estado.Nombre = obj.Nombre;



                            estado.Pais.IdPais = obj.IdPais.Value;

                            result.Objects.Add(estado);

                           

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
