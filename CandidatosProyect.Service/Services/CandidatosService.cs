namespace CandidatosProyect.Service
{
    using CandidatosProyect.Datos;
    using CandidatosProyect.Entidades;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;

    public class CandidatosService : Service<Candidatos>, ICandidatosService
    {
        protected override Candidatos LogicaObtenerUno(int idEntity, IRepository<Candidatos> repository)
        {
            var objeto = repository.Query(x => x.can_Id == idEntity)
                                   .AsQueryable()
                                   .Include(x => x.Empleos)
                                   .FirstOrDefault();

            return objeto;
        }

        protected override List<Candidatos> LogicaObtenerTodos(IRepository<Candidatos> repository)
        {
            var lista = repository.GetQueryable()
                                  .Include(x => x.Empleos)
                                  .ToList();

            return lista;
        }

        protected override void LogicaParaEliminar(int idEntity, IRepository<Candidatos> repository)
        {
            var objeto = repository.Query(x => x.can_Id == idEntity)
                                   .AsQueryable()
                                   .Include(x => x.Empleos)
                                   .FirstOrDefault();

            repository.Remove(objeto);
        }
    }
}