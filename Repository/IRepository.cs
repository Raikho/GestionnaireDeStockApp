using System.Collections.Generic;

namespace Repository
{
    public interface IRepository<Entity> where Entity : class
    {
        IEnumerable<Entity> GetAll();
        Entity GetSingleById(int Id);
        void Insert(Entity obj);
        void Update(Entity obj);
        void Delete(Entity obj);
        void Save();
    }
}
