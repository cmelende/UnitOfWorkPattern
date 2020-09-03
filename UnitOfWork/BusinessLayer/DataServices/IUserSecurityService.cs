using System.Collections.Generic;
using DomainModel;

namespace BusinessLayer.DataServices
{
    public interface IUserSecurityService
    {
        public IEnumerable<User> GetUsers();
    }
}