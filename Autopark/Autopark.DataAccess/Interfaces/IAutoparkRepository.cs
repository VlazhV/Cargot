namespace Autopark.DataAccess.Interfaces;

public interface IAutoparkRepository: IRepository<Entities.Autopark>
{
    bool DoesItExist(int id);
}