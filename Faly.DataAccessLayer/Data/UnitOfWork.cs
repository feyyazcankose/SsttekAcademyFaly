namespace Faly.DataAccessLayer.Data;

public class UnitOfWork(AppDbContext context): IUnitOfWork
{
    public int Commit()
    {   
        return context.SaveChanges();
    }
}