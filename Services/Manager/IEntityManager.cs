using Services.Entities;

namespace Services.Manager {
    public interface IEntityManager<T> where T : IEntity {

        void Save(T entity);
        void Delete(T entity);
        T Get(long id);

    }
}
