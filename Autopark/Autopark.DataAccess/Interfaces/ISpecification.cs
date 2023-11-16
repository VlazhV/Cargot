namespace Autopark.DataAccess.Interfaces;

public interface ISpecification<T>
{
    IQueryable<T> Build(IQueryable<T> source);
}