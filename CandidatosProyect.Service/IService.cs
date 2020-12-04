namespace CandidatosProyect.Service
{
    using System.Collections.Generic;

    public interface IService<T> where T : class
    {
        void Alta(T entity);

        bool AltaMasiva(List<T> entities);

        void Eliminar(int idEntity);

        void Modificar(T entity);

        ICollection<T> ObtenerTodos();

        T ObtenerUno(int idEntity);
    }
}