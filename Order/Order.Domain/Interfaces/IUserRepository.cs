using Order.Domain.Entities;

namespace Order.Domain.Interfaces;

public interface IUserRepository: IRepository<User, long>
{
	Task<bool> IsUserExistsAsync(long id);
	Task<bool> IsUserExistsAsync(User user);	
}
