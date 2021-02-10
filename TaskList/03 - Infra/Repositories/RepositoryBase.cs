using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TaskList._01___Domain.Interfaces;
using TaskList._03___Infra.Repositories.DatabaseContext;

namespace TaskList._03___Infra.Repositories
{
    public class RepositoryBase<Entity> : IRepositoryBase<Entity> where Entity : class
    {

        private readonly DataContext dataContext;

        public RepositoryBase(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<Entity> Get(long id)
        {
            var entity = await this.dataContext.Set<Entity>().FindAsync(id);
            return entity;
        }

        public void Remove(Entity entity)
        {

            this.dataContext.Set<Entity>().Remove(entity);

        }

        public async Task Save(Entity entity)
        {

            await this.dataContext.Set<Entity>().AddRangeAsync(entity);

        }


        public void Update(Entity entity)
        {

            this.dataContext.Entry(entity).State = EntityState.Modified;

        }

    }
}
