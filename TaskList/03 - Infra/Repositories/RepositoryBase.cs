using Microsoft.EntityFrameworkCore;
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

        public Entity Get(long id)
        {
            var entity = this.dataContext.Set<Entity>().Find(id);
            return entity;
        }

        public void Remove(Entity entity)
        {

            this.dataContext.Set<Entity>().Remove(entity);

        }

        public void Save(Entity entity)
        {

            this.dataContext.Set<Entity>().AddRange(entity);

        }


        public void Update(Entity entity)
        {

            this.dataContext.Entry(entity).State = EntityState.Modified;

        }

    }
}
