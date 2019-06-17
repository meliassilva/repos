using System;
using System.Linq.Expressions;

namespace Byui.Something.ApplicationCore.Common.Interfaces.Persistence
{
    public interface IOrderBy<T>
    {
        Expression<Func<T, object>> OrderByExpression { get; }
        bool Desc { get; }
    }
}