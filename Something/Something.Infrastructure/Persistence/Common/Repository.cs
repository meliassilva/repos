using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Byui.Something.ApplicationCore.Common;
using Byui.Something.ApplicationCore.Common.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Byui.Something.Infrastructure.Persistence.Common
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        protected readonly DatabaseContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public Repository(DatabaseContext context)
        {
            _context = context;
        }


        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> GetSingleBySpecAsync(ISpecification<T> spec)
        {
            return await Query(spec).FirstOrDefaultAsync();
        }

        public async Task<List<T>> ListAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<List<T>> ListAsync(ISpecification<T> spec, int? skip = null, int? take = null, bool? distinct = null)
        {
            var q = Query(spec);

            if (spec.OrderBys?.Any() == true)
            {
                for (int i = 0; i < spec.OrderBys.Count; i++)
                {
                    if (i == 0)
                    {
                        if (spec.OrderBys[i].Desc)
                        {
                            q = q.OrderByDescending(spec.OrderBys[i].OrderByExpression);
                        }
                        else
                        {
                            q = q.OrderBy(spec.OrderBys[i].OrderByExpression);
                        }
                    }
                    else
                    {
                        if (spec.OrderBys[i].Desc)
                        {
                            q = ((IOrderedQueryable<T>)q).ThenByDescending(spec.OrderBys[i].OrderByExpression);
                        }
                        else
                        {
                            q = ((IOrderedQueryable<T>)q).ThenBy(spec.OrderBys[i].OrderByExpression);
                        }
                    }
                }
            }

            if (skip.HasValue)
            {
                q = q.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                q = q.Take(take.Value);
            }

            if (distinct == true)
            {
                q = q.Distinct();
            }
            return await q.ToListAsync();
        }

        public async Task<int> Count(ISpecification<T> spec)
        {
            return await Query(spec).CountAsync();
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Add(IEnumerable<T> entities)
        {
            _context.AddRange(entities);
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void Delete(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        public void Detach(T entity)
        {
            _context.Entry(entity).State = EntityState.Detached;
        }

        protected virtual IQueryable<T> Query(ISpecification<T> spec)
        {
            // fetch a Queryable that includes all expression-based includes
            var queryableResultWithIncludes = spec.Includes
                .Aggregate(_context.Set<T>().AsQueryable(),
                    (current, include) => current.Include(include));

            // modify the IQueryable to include any string-based include statements
            var secondaryResult = spec.IncludeStrings
                .Aggregate(queryableResultWithIncludes,
                    (current, include) => current.Include(include));

            // return the result of the query using the specification's criteria expression
            return secondaryResult
                .Where(spec.Criteria);
        }
    }
}