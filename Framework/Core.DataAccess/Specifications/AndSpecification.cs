using System;
using System.Linq;
using System.Linq.Expressions;

namespace Core.DataAccess.Specifications
{
    public class AndSpecification<TEntity> : Specification<TEntity>
    {
        Specification<TEntity> _leftSpec;
        Specification<TEntity> _rightSpec;

        public AndSpecification(Specification<TEntity> leftSpec, Specification<TEntity> rightSpec)
        {
            _leftSpec = leftSpec;
            _rightSpec = rightSpec;
        }

        public override Expression<Func<TEntity, bool>> ToExpression()
        {
            var leftExpression = _leftSpec.ToExpression();
            var rightExpression = _rightSpec.ToExpression();

            var andExpression = Expression.AndAlso(leftExpression.Body, rightExpression.Body);
            return Expression.Lambda<Func<TEntity, bool>>(andExpression, leftExpression.Parameters.Single());
        }
    }
}
