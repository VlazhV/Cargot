using Ship.Domain.Interfaces;

namespace Ship.Application.Specifications;

public class PageSpecification<T> : ISpecification<T>
{
	private readonly int? _offset;
	private readonly int? _limit;	

	public PageSpecification(int? offset, int? limit)
	{
		_offset = offset;
		_limit = limit;
	}
	
	public IQueryable<T> Build(IQueryable<T> query)
	{
		if (_offset.HasValue && _limit.HasValue)
		{
			return query.Skip<T>(_offset.Value).Take(_limit.Value);
		}
				
		return query;        	
	}
}