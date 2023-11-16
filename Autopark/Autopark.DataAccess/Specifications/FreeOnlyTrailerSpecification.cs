using System.Linq.Expressions;
using Autopark.DataAccess.Entities;
using Autopark.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Autopark.DataAccess.Specifications;

public class FreeOnlyTrailerSpecification : ISpecification<Trailer>
{
	private readonly Expression<Func<Trailer, bool>> _expression;
	
	public FreeOnlyTrailerSpecification(DateTime start, DateTime finish)
	{
		_expression = t => t.Shedules
			.All(t => t.PlanFinish < start || t.PlanStart > finish);
	}
	
	public IQueryable<Trailer> Build(IQueryable<Trailer> source)
	{
		return source.Include(t => t.Shedules).Where(_expression);
	}

}