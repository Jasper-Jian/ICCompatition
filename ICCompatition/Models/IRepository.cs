using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICCompatition.Models
{
    public interface IRepository<T> where T : ExerciseViewModel
    {
        IQueryable<T> GetAll();
        IQueryable<T> Get(int id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}