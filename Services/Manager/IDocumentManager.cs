using Services.Entities;
using System.Collections.Generic;

namespace Services.Manager {
    public interface IDocumentManager : IEntityManager<IDocument> {
        IEnumerable<IDocument> GetList();
    }
}
