namespace CandidatosProyect.Datos
{
    using System.Collections.Generic;
    using System.Linq;

    public interface IRepository<T> where T : class
    {
        void Add(T entity);

        void Detach(T entity);

        void Edit(T entity);

        bool ExistId(object id);

        IEnumerable<T> GetAll();

        T GetById(object id);

        IQueryable<T> GetQueryable();

        IEnumerable<T> Query(System.Linq.Expressions.Expression<System.Func<T, bool>> filter);

        void Remove(T entity);
    }
}