namespace Ship.Domain.Interfaces;

public interface ISpecification<T>
{
    IQueryable<T> Build(IQueryable<T> query);
}