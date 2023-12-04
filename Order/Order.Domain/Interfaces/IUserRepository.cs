using Order.Domain.Entities;

namespace Order.Domain.Interfaces;

public interface IUserRepository: IRepository<User, long>
{
	Task<bool> DoesItExistAsync(long id);
	Task<bool> DoesItExistAsync(User user);	
}
