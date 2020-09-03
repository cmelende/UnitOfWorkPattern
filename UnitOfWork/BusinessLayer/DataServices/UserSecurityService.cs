using System.Collections.Generic;
using DataAccess.Interfaces;
using DomainModel;

namespace BusinessLayer.DataServices
{
    public class UserSecurityService : IUserSecurityService
    {
        private IRepository<User> repo;
        public UserSecurityService(IRepository<User> userRepo)
        {
            repo = userRepo;
        }
        
        public IEnumerable<User> GetUsers()
        {
            return repo.GetAll();
        }
    }
}