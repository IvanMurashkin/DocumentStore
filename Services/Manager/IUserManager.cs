using Services.Entities;

namespace Services.Manager {
    public interface IUserManager : IEntityManager<IUser> {
        IUser Get(string login);
        bool Check(string login, string password);
    }
}
