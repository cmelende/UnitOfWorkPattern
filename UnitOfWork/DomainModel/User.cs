using DataAccess.Interfaces;

namespace DomainModel
{
    public class User :  IEntity
    {
        public virtual int Id { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string MiddleName { get; set; }
        public virtual string LastName { get; set; }
    }
}