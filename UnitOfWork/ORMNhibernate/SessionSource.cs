using FluentNHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace ORMNhibernate
{
    public class SessionSource : ISessionSource
    {
        public Configuration Configuration { get; }
        public ISessionFactory SessionFactory { get; }
        
        public SessionSource()
        {
            
            const string server = "localhost";
            const string databaseName = "dew_db";
            const string userName = "dew_user";
            const string password = "testpw";
            Configuration = Fluently
                .Configure()
                .Database(
                    MySQLConfiguration.Standard.ConnectionString($"server={server};database={databaseName};uid={userName};pwd={password}"))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<UserMap>())
                .BuildConfiguration();
    
            SessionFactory = Configuration
                .BuildSessionFactory();
        }
        public ISession CreateSession()
        {
            return SessionFactory.OpenSession();
        }

        public void BuildSchema()
        {
            var exporter = new SchemaExport(Configuration);
            exporter.Execute(true, true, false);
        }
    }
}