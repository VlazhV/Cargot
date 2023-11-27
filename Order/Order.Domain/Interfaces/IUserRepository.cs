using Order.Domain.Entities;

namespace Order.Domain.Interfaces;

public interface IUserRepository: IRepository<User, long>
{
	bool DoesItExist(User user);	
}