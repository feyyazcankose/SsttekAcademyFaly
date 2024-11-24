namespace Faly.DataAccessLayer.Data;

public interface IUnitOfWork
{
    int Commit();
}