using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositiryex1
{
    public interface IGenericReposiyory<TEntity>where TEntity:class
    {
        void Add(TEntity item);//insert
        TEntity FindById(object id);//select
        IEnumerable<TEntity>GetAll();//select *

        IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate);
        void Remove(TEntity item);
        void Update(TEntity item);

    }
}
