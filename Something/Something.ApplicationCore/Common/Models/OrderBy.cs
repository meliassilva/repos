using System;
using System.Linq.Expressions;
using Byui.Something.ApplicationCore.Common.Interfaces.Persistence;

namespace Byui.Something.ApplicationCore.Common.Models
{
    public class OrderBy<T> : IOrderBy<T>
    {
        public Expression<Func<T, object>> OrderByExpression { get; }
        public bool Desc { get; }
        
        public OrderBy(Expression<Func<T, object>> orderByExpression, bool desc)
        {
            OrderByExpression = orderByExpression;
            Desc = desc;
        }

    }
}