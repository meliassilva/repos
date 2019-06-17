using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Byui.Something.ApplicationCore.Common.Interfaces.Persistence;
using Byui.Something.ApplicationCore.Common.Models;

namespace Byui.Something.ApplicationCore.Common
{
    public abstract class BaseSpecification<T> : ISpecification<T>
    {
        protected BaseSpecification()
        {
        }
        protected BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }
        public Expression<Func<T, bool>> Criteria { get; protected set; }
        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();
        public List<string> IncludeStrings { get; } = new List<string>();
        public List<IOrderBy<T>> OrderBys { get; } = new List<IOrderBy<T>>();

        protected virtual void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }
        protected virtual void AddInclude(string includeString)
        {
            IncludeStrings.Add(includeString);
        }

        protected virtual void AddOrderBy(Expression<Func<T, object>> orderByExpression, bool desc)
        {
            OrderBys.Add(new OrderBy<T>(orderByExpression, desc));
        }
    }
}