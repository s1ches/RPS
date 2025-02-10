using System.Linq.Expressions;

namespace RPS.Common.Specification;

public class Specification<T>(Expression<Func<T, bool>> expression)
{
    private readonly Expression<Func<T, bool>> _specExpression =
        expression ?? throw new ArgumentNullException(nameof(expression));
    
    private readonly Func<T, bool> _specFunc = expression.Compile();

    public static implicit operator Expression<Func<T, bool>>(Specification<T> specification)
    {
        return specification._specExpression;
    }
    
    public static implicit operator Func<T, bool>(Specification<T> specification)
    {
        return specification._specFunc;
    }
    
    public static Specification<T> operator &(Specification<T> spec1, Specification<T> spec2)
    {
        if (spec1 == null || spec2 == null)
            throw new ArgumentNullException();

        var combinedExpression = Expression.Lambda<Func<T, bool>>(
            Expression.AndAlso(
                spec1._specExpression.Body,
                new ExpressionParameterRebinder(spec2._specExpression.Parameters, spec1._specExpression.Parameters)
                    .Visit(
                        spec2._specExpression.Body)
            ),
            spec1._specExpression.Parameters
        );

        return new Specification<T>(combinedExpression);
    }

    public static bool operator false(Specification<T> spec1)
    {
        return false;
    }

    public static bool operator true(Specification<T> spec1)
    {
        return false;
    }

    public static Specification<T> operator |(Specification<T> spec1, Specification<T> spec2)
    {
        if (spec1 == null || spec2 == null)
            throw new ArgumentNullException();

        var combinedExpression = Expression.Lambda<Func<T, bool>>(
            Expression.OrElse(
                spec1._specExpression.Body,
                new ExpressionParameterRebinder(spec2._specExpression.Parameters, spec1._specExpression.Parameters)
                    .Visit(
                        spec2._specExpression.Body)
            ),
            spec1._specExpression.Parameters
        );

        return new Specification<T>(combinedExpression);
    }

    public static Specification<T> operator !(Specification<T> spec)
    {
        ArgumentNullException.ThrowIfNull(spec);

        var negatedExpression = Expression.Lambda<Func<T, bool>>(
            Expression.Not(spec._specExpression.Body),
            spec._specExpression.Parameters
        );

        return new Specification<T>(negatedExpression);
    }
}

public class ExpressionParameterRebinder : ExpressionVisitor
{
    private readonly ParameterExpression[] _from;
    private readonly ParameterExpression[] _to;

    public ExpressionParameterRebinder(IReadOnlyCollection<ParameterExpression> from,
        IReadOnlyCollection<ParameterExpression> to)
    {
        if (from.Count != to.Count)
            throw new ArgumentException();

        _from = from.ToArray();
        _to = to.ToArray();
    }

    protected override Expression VisitParameter(ParameterExpression node)
    {
        for (var i = 0; i < _from.Length; i++)
        {
            if (node == _from[i])
                return _to[i];
        }

        return base.VisitParameter(node);
    }
}