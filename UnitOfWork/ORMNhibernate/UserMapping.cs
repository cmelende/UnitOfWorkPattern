using DomainModel;
using FluentNHibernate.Mapping;

namespace ORMNhibernate
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Table("users");

            Id(t => t.Id, "id").GeneratedBy.Identity();
            
            Map(t => t.FirstName, "first_name")
                .Length(50);
            Map(t => t.MiddleName, "middle_name")
                .Length(50);
            Map(t => t.LastName, "last_name")
                .Length(50);
        }
    }

}