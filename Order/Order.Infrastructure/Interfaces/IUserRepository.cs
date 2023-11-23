using Order.Domain.Entities;

namespace Order.Infrastructure.Interfaces;

public interface IUserRepository: IRepository<User, long>
{
}
