using DocStore.Domain.Model;
using FluentNHibernate.Mapping;
using System;

namespace DocStore.Domain.NHiberante.Mapping {

    public class DocumentMap : ClassMap<Document> {

        public DocumentMap() {

            Id(d => d.Id).GeneratedBy.Native();
            Map(d => d.Name).Not.Nullable();
            Map(d => d.Date).Not.Nullable();
            Map(d => d.OriginalName).Not.Nullable();
            Map(d => d.DocumentData).Nullable().Length(Int32.MaxValue);
            Map(d => d.DocumentMimeType).Nullable();
            References(d => d.User);

        }

    }

}
