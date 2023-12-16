namespace Autopark.DataAccess.Interfaces;

public interface IAutoparkRepository: IRepository<Entities.Autopark>
{
    Task<bool> DoesItExistAsync(int id);
}