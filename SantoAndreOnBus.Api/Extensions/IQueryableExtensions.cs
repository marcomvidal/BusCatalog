using System.Linq.Expressions;

namespace SantoAndreOnBus.Api.Extensions;

public static class IQueryableExtensions
{
    public static IQueryable<T> WhereIfTrue<T>(
        this IQueryable<T> queryable,
        bool condition,
        Expression<Func<T, bool>> predicate) =>
        !condition ? queryable : queryable.Where(predicate);
}
