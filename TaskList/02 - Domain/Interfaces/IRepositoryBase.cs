using System;
using System.Collections.Generic;
using System.Text;

namespace TaskList._01___Domain.Interfaces
{

    public interface IRepositoryBase<Entity> where Entity : class
    {

        void Save(Entity entity);

        void Update(Entity entity);

        Entity Get(long id);

        void Remove(Entity entity);

    }
}


