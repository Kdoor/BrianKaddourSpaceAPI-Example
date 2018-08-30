using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SpaceSmileBrianKaddour.ApplicationCore.Interfaces
{
    //Stub for Specification work, would use this pattern on DBs after implementation.
    public interface ISpecification<T>
    {
        bool IsSatisfiedBy(T obj);
        Expression<Func<T, bool>> ToExpression();
    }
}
