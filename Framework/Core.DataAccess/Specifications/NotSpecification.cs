using System;
using System.Linq;
using System.Linq.Expressions;

namespace Core.DataAccess.Specifications
{
    public class NotSpecification<TEntity> : Specification<TEntity>
    {
        Specification<TEntity> _spec;

        public NotSpecification(Specification<TEntity> spec)
        {
            _spec = spec;
        }

        public override Expression<Func<TEntity, bool>> ToExpression()
        {
            var expression = _spec.ToExpression();
            var notExpression = Expression.Not(expression.Body);
            return Expression.Lambda<Func<TEntity, bool>>(notExpression, expression.Parameters.Single());
        }
    }
}
