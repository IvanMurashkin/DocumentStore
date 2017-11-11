using Services.Manager;
using Services.Entities;
using NHibernate;
using DocStore.Domain.Model;
using NHibernate.Criterion;

namespace DocStore.Domain.NHiberante {
    public class UserManager : EntityManager<IUser>, IUserManager {

        public IUser Get(string login) {
            using (ISession session = NHibernateHelper.OpenSession()) {

                var user = session.QueryOver<User>()
                    .And(u => u.Login == login)
                    .SingleOrDefault();

                return user;
            }
        }

        public bool Check(string login, string password) {

            using (ISession session = NHibernateHelper.OpenSession()) {

                var user = session.CreateCriteria<User>()
                    .Add(Restrictions.Eq("Login", login))
                    .Add(Restrictions.Eq("Password", password))
                    .UniqueResult<User>();

                return user != null;

            }

        }

    }
}
