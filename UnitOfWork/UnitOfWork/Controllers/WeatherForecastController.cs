using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.DataServices;
using DataAccess.Interfaces;
using DomainModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ORMEntityFramework;
using ORMNhibernate;

namespace UnitOfWork.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private IRepository<User> _nhUserRepo;
        private IRepository<User> _efUserRepo;
        public WeatherForecastController(INhRepository<User> pNhUserRepo, IEfRepository<User> pEfRepo)
        {
            _nhUserRepo = pNhUserRepo;
            _efUserRepo = pEfRepo;
        }
        [HttpGet]
        public IEnumerable<User> Get()
        {
            var id = 1;
            var nhService = new UserSecurityService(_nhUserRepo);
            var efService = new UserSecurityService(_efUserRepo);
    
            
            var ret = new List<User>();
            ret.AddRange(nhService.GetUsers());
            ret.AddRange(efService.GetUsers());

            return ret;
        }
    }
}