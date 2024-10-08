﻿using DK.PolicyService.Domain.Contracts;
using NHibernate;
using NHibernate.Bytecode;
using NHibernate.Cfg;
using NHibernate.Connection;
using NHibernate.Dialect;
using NHibernate.Driver;
using System.Data;

namespace DK.PolicyService.DataAccess.ORM;

public static class NHibernateInstaller
{
    public static IServiceCollection AddNHibernate(this IServiceCollection services, string cnString)
    {
        var cfg = new Configuration();

        cfg.DataBaseIntegration(db =>
        {
            db.Dialect<PostgreSQL83Dialect>();
            db.Driver<NpgsqlDriver>();
            db.ConnectionProvider<DriverConnectionProvider>();
            db.BatchSize = 500;
            db.IsolationLevel = IsolationLevel.ReadCommitted;
            db.LogSqlInConsole = false;
            db.ConnectionString = cnString;
            db.Timeout = 30; /*seconds*/
            db.SchemaAction = SchemaAutoAction.Update;
        });

        cfg.Proxy(p => p.ProxyFactoryFactory<StaticProxyFactoryFactory>());

        cfg.Cache(c => c.UseQueryCache = false);

        cfg.AddAssembly(typeof(NHibernateInstaller).Assembly);

        services.AddSingleton(cfg.BuildSessionFactory());

        services.AddScoped(s => s.GetService<ISessionFactory>().OpenSession());

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
