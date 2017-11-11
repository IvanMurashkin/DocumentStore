using DocStore.Domain.Model;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace DocStore.Domain.NHiberante {
    public class NHibernateHelper {
        private static ISessionFactory _sessionFactory;
        public static ISessionFactory SessionFactory {
            get {
                if (_sessionFactory == null) {
                    var dbConfig = MsSqlConfiguration.MsSql2012
                      .ConnectionString(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\GitRepository\DocumentsStorage\DocStore.Domain\App_Data\DocumentsStorageDB.mdf;Integrated Security=True")
                      .ShowSql();

                    var configuration = Fluently.Configure()
                      .Database(dbConfig)
                      .Mappings(m => m.FluentMappings.AddFromAssemblyOf<User>())
                      .BuildConfiguration();

                    _sessionFactory = configuration.BuildSessionFactory();
                }
                return _sessionFactory;
            }
        }

        public static ISession OpenSession() {
            return SessionFactory.OpenSession();
        }
    }
}

