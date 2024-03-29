﻿using SpaceSmileBrianKaddour.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace SpaceSmileBrianKaddour.ApplicationCore.Specifications
{
    //Stub for Specification work, would use this pattern on DBs after implementation.
    internal sealed class IdentitySpecification<T> : Specification<T>
    {
        public override Expression<Func<T, bool>> ToExpression()
        {
            return x => true;
        }
    }

    public abstract class Specification<T>
    {
        public static readonly Specification<T> All = new IdentitySpecification<T>();

        public bool IsSatisfiedBy(T entity)
        {
            Func<T, bool> predicate = ToExpression().Compile();
            return predicate(entity);
        }

        public abstract Expression<Func<T, bool>> ToExpression();

        public Specification<T> And(Specification<T> specification)
        {
            if (this == All)
                return specification;
            if (specification == All)
                return this;

            return new AndSpecification<T>(this, specification);
        }

        public Specification<T> Or(Specification<T> specification)
        {
            if (this == All || specification == All)
                return All;

            return new OrSpecification<T>(this, specification);
        }

        public Specification<T> Not()
        {
            return new NotSpecification<T>(this);
        }
    }

    internal sealed class AndSpecification<T> : Specification<T>
    {
        private readonly Specification<T> _left;
        private readonly Specification<T> _right;

        public AndSpecification(Specification<T> left, Specification<T> right)
        {
            _right = right;
            _left = left;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            Expression<Func<T, bool>> leftExpression = _left.ToExpression();
            Expression<Func<T, bool>> rightExpression = _right.ToExpression();

            var invokedExpression = Expression.Invoke(rightExpression, leftExpression.Parameters);

            return (Expression<Func<T, Boolean>>)Expression.Lambda(Expression.AndAlso(leftExpression.Body, invokedExpression), leftExpression.Parameters);
        }
    }

    internal sealed class OrSpecification<T> : Specification<T>
    {
        private readonly Specification<T> _left;
        private readonly Specification<T> _right;

        public OrSpecification(Specification<T> left, Specification<T> right)
        {
            _right = right;
            _left = left;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            Expression<Func<T, bool>> leftExpression = _left.ToExpression();
            Expression<Func<T, bool>> rightExpression = _right.ToExpression();

            var invokedExpression = Expression.Invoke(rightExpression, leftExpression.Parameters);

            return (Expression<Func<T, Boolean>>)Expression.Lambda(Expression.OrElse(leftExpression.Body, invokedExpression), leftExpression.Parameters);
        }
    }

    internal sealed class NotSpecification<T> : Specification<T>
    {
        private readonly Specification<T> _specification;

        public NotSpecification(Specification<T> specification)
        {
            _specification = specification;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            Expression<Func<T, bool>> expression = _specification.ToExpression();
            UnaryExpression notExpression = Expression.Not(expression.Body);

            return Expression.Lambda<Func<T, bool>>(notExpression, expression.Parameters.Single());
        }
    }


    //Consider splitting these up
    public sealed class LaunchedFromHouston : Specification<LaunchPadInfo>
    {
        // TODO: *Implement Database* Need to implement filters as well for reduction of our data, also consider using OData endpoints
        // TODO: *Implement Performance* also consider using OData endpoints, because they are awesome!
        // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/statements-expressions-operators/lambda-expressions
        public override Expression<Func<LaunchPadInfo, bool>> ToExpression()
        {
            //Example use an anonymous Expression Lambda
            return launchPadInfo => launchPadInfo.Location.Name == "Houston" ;
        }
    }
}
