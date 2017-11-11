using Services.Manager;
using Services.Entities;
using NHibernate;
using DocStore.Domain.Model;
using System.Collections.Generic;

namespace DocStore.Domain.NHiberante {
    public class DocumentManager : EntityManager<IDocument>, IDocumentManager {

        public IEnumerable<IDocument> GetList() {

            using (ISession session = NHibernateHelper.OpenSession()) {

                return session.QueryOver<Document>()
                    .JoinQueryOver(d => d.User)
                    .And(u => u.Login == u.Login)
                    .List();

            }

        }
    }
}
