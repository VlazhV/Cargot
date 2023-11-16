using System.Linq.Expressions;
using Autopark.DataAccess.Entities;
using Autopark.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Autopark.DataAccess.Specifications;

public class FreeOnlyCarSpecification : ISpecification<Car>
{
	private readonly Expression<Func<Car, bool>> _expression;
	
	public FreeOnlyCarSpecification(DateTime start, DateTime finish)
	{
		_expression = c => c.Shedules
			.All(s => s.PlanFinish < start || s.PlanStart > finish);			
	}
	
	public IQueryable<Car> Build(IQueryable<Car> source)
	{
		return source.Include(c => c.Shedules).Where(_expression);
	}

}