using TaskList._01___Domain.Interfaces;

namespace TaskList._03___Infra.Repositories.DatabaseContext
{
    public class DataTransaction : IDataTransaction
    {

        private readonly DataContext context;

        public DataTransaction(DataContext _dataContext)
        {

            context = _dataContext;

        }
        public void Commit()
        {

            context.SaveChanges();

        }

        public void RollBack()
        { 
            context.Dispose();  
            
        }
    }
}