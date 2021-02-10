using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TaskList._01___Domain.Interfaces
{

    public interface IRepositoryBase<Entity> where Entity : class
    {

        Task Save(Entity entity);

        void Update(Entity entity);

        Task<Entity> Get(long id);

        void Remove(Entity entity);

    }
}


