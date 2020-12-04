namespace CandidatosProyect.Service
{
    using CandidatosProyect.Datos;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Service<T> : IService<T> where T : class
    {
        protected UnitOfWork uow;

        public virtual ICollection<T> ObtenerTodos()
        {
            var repository = this.CrearRepository();
            var lista = this.LogicaObtenerTodos(repository);

            this.CerrarRepository();

            return lista;
        }

        protected virtual List<T> LogicaObtenerTodos(IRepository<T> repository) => repository.GetAll().ToList();

        public virtual T ObtenerUno(int idEntity)
        {
            var repository = this.CrearRepository();

            var objeto = this.LogicaObtenerUno(idEntity, repository);

            this.CerrarRepository();

            return objeto;
        }

        protected virtual T LogicaObtenerUno(int idEntity, IRepository<T> repository) => repository.GetById(idEntity);

        protected virtual IRepository<T> CrearRepository()
        {
            this.uow = new UnitOfWork();

            var repository = this.uow.GetRepository<T>();

            return repository;
        }

        protected virtual void CerrarRepository()
        {
            this.uow.Save();

            this.uow.Dispose();
        }

        public virtual void Eliminar(int idEntity)
        {
            var repository = this.CrearRepository();

            try
            {
                this.LogicaParaEliminar(idEntity, repository);

                this.CerrarRepository();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public virtual void Alta(T entity)
        {
            var repository = this.CrearRepository();

            try
            {
                this.LogicaAlta(entity, repository);

                this.CerrarRepository();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public virtual bool AltaMasiva(List<T> entities)
        {
            var repository = this.CrearRepository();

            try
            {
                foreach (var entity in entities)
                {
                    this.LogicaAlta(entity, repository);
                }

                this.CerrarRepository();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public virtual void Modificar(T entity)
        {
            var repository = this.CrearRepository();

            try
            {
                this.LogicaModificacion(entity, repository);

                this.CerrarRepository();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        protected virtual void LogicaAlta(T entity, IRepository<T> repository) => repository.Add(entity);

        protected virtual void LogicaModificacion(T entity, IRepository<T> repository) => repository.Edit(entity);

        protected virtual void LogicaParaEliminar(int idEntity, IRepository<T> repository)
        {
            var objeto = repository.GetById(idEntity);

            repository.Remove(objeto);
        }
    }
}