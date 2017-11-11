using DocStore.Domain.Model;
using FluentNHibernate.Mapping;

namespace DocStore.Domain.NHiberante.Mapping {

    public class UserMap : ClassMap<User> {

        public UserMap() {
            Id(u => u.Id).GeneratedBy.Native();
            Map(u => u.Login).Not.Nullable();
            Map(u => u.Password).Not.Nullable();
            HasMany(u => u.Documents).Cascade.All();
        }

    }


}
