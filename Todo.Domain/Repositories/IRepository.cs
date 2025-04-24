using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Domain.Repositories
{
    public interface IRepository<TEntity> 
        where TEntity : class
    {
        public TEntity? GetById(int id);
        public IEnumerable<TEntity> GetAll();
        public void Update(TEntity entity);
        public int? Create(TEntity entity);
        public void Delete(TEntity entity);
    }
}
