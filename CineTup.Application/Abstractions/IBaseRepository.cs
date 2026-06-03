using System;
using System.Collections.Generic;
using System.Text;

namespace CineTup.Application.Abstractions
{
    public interface IBaseRepository<T> where T : class
    {
        List<T> GetAll();
        T? GetById(int id);
        T Add(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}
