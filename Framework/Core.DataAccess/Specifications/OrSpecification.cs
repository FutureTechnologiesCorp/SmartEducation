using System;
using System.Linq;
using System.Linq.Expressions;

namespace Core.DataAccess.Specifications
{
    public class OrSpecification<TEntity> : Specification<TEntity>
    {
        Specification<TEntity> _leftSpec;
        Specification<TEntity> _rightSpec;

        public OrSpecification(Specification<TEntity> leftSpec, Specification<TEntity> rightSpec)
        {
            _leftSpec = leftSpec;
            _rightSpec = rightSpec;
        }

        public override Expression<Func<TEntity, bool>> ToExpression()
        {
            var leftExpression = _leftSpec.ToExpression();
            var rightExpression = _rightSpec.ToExpression();

            var orExpression = Expression.OrElse(leftExpression.Body, rightExpression.Body);
            return Expression.Lambda<Func<TEntity, bool>>(orExpression, leftExpression.Parameters.Single());
        }
    }
}
