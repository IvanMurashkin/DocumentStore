using NHibernate;
using Services.Entities;
using Services.Manager;
using System;

namespace DocStore.Domain.NHiberante {
    public class EntityManager<T> : IEntityManager<T> where T : IEntity {

        public void Delete(T entity) {

            using (ISession session = NHibernateHelper.OpenSession()) {

                using (ITransaction tr = session.BeginTransaction()) {

                    try {
                        session.Delete(entity);
                    } catch (Exception) {
                        tr.Rollback();
                        return;
                    }
                    tr.Commit();
                }                    

            }

        }

        public T Get(long id) {

            using (ISession session = NHibernateHelper.OpenSession()) {
                return session.Get<T>(id);
            }

        }

        public void Save(T entity) {

            using (ISession session = NHibernateHelper.OpenSession()) {

                using (ITransaction tr = session.BeginTransaction()) {

                    try {
                        session.SaveOrUpdate(entity);
                    } catch (Exception ex) {
                        tr.Rollback();
                        return;
                    }
                    tr.Commit();

                }

            }

        }

    }
}
