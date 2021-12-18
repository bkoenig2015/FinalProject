using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TripLog.Models
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected TripLogContext context { get; set; }
        private DbSet<T> dbset { get; set; }

        public Repository(TripLogContext ctx) {
            context = ctx;
            dbset = context.Set<T>();
        }

        public virtual IEnumerable<T> List(QueryOptions<T> options) {
            IQueryable<T> query = BuildQuery(options);
            return query.ToList();
        }

        public virtual T Get(QueryOptions<T> options) {
            IQueryable<T> query = BuildQuery(options);
            return query.FirstOrDefault();
        }
        public virtual T Get(int id) => dbset.Find(id);

        public virtual void Insert(T entity) => dbset.Add(entity);
        public virtual void Update(T entity) => dbset.Update(entity);
        public virtual void Delete(T entity) => dbset.Remove(entity);

        public virtual void Save() => context.SaveChanges();

        private IQueryable<T> BuildQuery(QueryOptions<T> options)
        {
            IQueryable<T> query = dbset;

            foreach (string include in options.GetIncludes()) {
                query = query.Include(include);
            }
            if (options.HasWhere)
                query = query.Where(options.Where);
            if (options.HasOrderBy)
                query = query.OrderBy(options.OrderBy);

            return query;
        }
    }

}
