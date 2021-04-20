using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositiryex1
{
    public class GenericUnitOfWork : IDisposable
    {
        DbContext context;

        public GenericUnitOfWork(DbContext context)
        {
            this.context = context;
        }

        public void SaveChanes()
        {
            context.SaveChanges();
        }

        public Dictionary<Type, object> repositories = new Dictionary<Type, object>();
        public IGenericReposiyory<T>Reposiyory<T>()where T : class
        {
            if (repositories.Keys.Contains(typeof(T)) == true)
            {
                return repositories[typeof(T)] as IGenericReposiyory<T>;
            }
            IGenericReposiyory<T> repo = new EFGenericRepository<T>(context);
            repositories.Add(typeof(T), repo);
            return repo;
        }

        public void Dispose()
        {
           context.Dispose();
        }
    }
}
