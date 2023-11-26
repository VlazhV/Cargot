using System.Linq.Expressions;
using Autopark.DataAccess.Entities;
using Autopark.DataAccess.Interfaces;

namespace Autopark.DataAccess.Specifications;

public class FreeOnlySpecification<TEntity, TSchedule> : ISpecification<TEntity> 
	where TEntity: class, ISchedulable<TSchedule> where TSchedule: ISchedule
{
	private readonly Expression<Func<TEntity, bool>> _expression;
	public FreeOnlySpecification(DateTime start, DateTime finish)
	{
		_expression = entity => entity.Schedules
			.All(schedule => schedule.PlanFinish < start || schedule.PlanStart > finish);
	}
	
	public IQueryable<TEntity> Build(IQueryable<TEntity> source)
	{
		return source.Where(_expression);		
	}

}
