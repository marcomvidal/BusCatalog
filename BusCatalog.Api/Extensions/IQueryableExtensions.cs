using System.Linq.Expressions;

namespace BusCatalog.Api.Extensions;

public static class IQueryableExtensions
{
    public static IQueryable<T> WhereIfTrue<T>(
        this IQueryable<T> queryable,
        bool condition,
        Expression<Func<T, bool>> predicate) =>
        condition ? queryable.Where(predicate) : queryable;

    public static IQueryable<T> LimitIfHasQuantity<T>(
        this IQueryable<T> queryable,
        int? quantity = null) =>
        quantity is not null ? queryable.Take(quantity.Value) : queryable;
}
