

using NHibernate;
using NHibernate.Cfg;
using System;
using System.IO;
using System.Reflection;
using System.Web;



namespace KozmetickiClassLibrary.Model
{
   public class NHibertnateSession

    {
        private static ISessionFactory _sessionFactory;
        public static ISession OpenSession()
        {
            try
            {
                if (_sessionFactory == null)
                {
                    _sessionFactory = OpenSessionFactory();
                }
                ISession session = _sessionFactory.OpenSession();
                return session;
            }
            catch (Exception e)
            {
                throw e.InnerException ?? e;
            }
        }
        private static ISessionFactory OpenSessionFactory()
        {
            var c = new Configuration();
            c.Configure();
            c.AddAssembly(Assembly.GetCallingAssembly());
            var sessionFactory = c.BuildSessionFactory();
            
            return sessionFactory;
        }
    }
}
