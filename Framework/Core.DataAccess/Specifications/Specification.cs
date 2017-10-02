using System;
using System.Linq.Expressions;

namespace Core.DataAccess.Specifications
{
    public abstract class Specification<TEntity>
    {
        public bool IsSatisfiedBy(TEntity entity)
        {
            Func<TEntity, bool> predicate = ToExpression().Compile();
            return predicate(entity);
        }

        public abstract Expression<Func<TEntity, bool>> ToExpression();

        public Specification<TEntity> And(Specification<TEntity> specification)
        {
            return new AndSpecification<TEntity>(this, specification);
        }

        public Specification<TEntity> Or(Specification<TEntity> specification)
        {
            return new OrSpecification<TEntity>(this, specification);
        }

        public Specification<TEntity> Not()
        {
            return new NotSpecification<TEntity>(this);
        }
    }
}
