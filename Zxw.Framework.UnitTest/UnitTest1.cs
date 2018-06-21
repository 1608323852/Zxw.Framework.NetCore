using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zxw.Framework.NetCore.DbContextCore;
using Zxw.Framework.NetCore.Extensions;
using Zxw.Framework.NetCore.IoC;
using Zxw.Framework.NetCore.Options;

namespace Zxw.Framework.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestGetDataTable()
        {
            BuildService();
            var dbContext = AspectCoreContainer.Resolve<IDbContextCore>();
            var dt = dbContext.GetDataTable("select * from \"public\".\"SysMenu\"");
            Assert.IsNotNull(dt);
        }

        public IServiceProvider BuildService()
        {
            IServiceCollection services = new ServiceCollection();

            //������ע��EF������
            services = RegisterPostgreSQLContext(services);

            services.AddOptions();
            return AspectCoreContainer.BuildServiceProvider(services);//����AspectCore.Injector
        }
        /// <summary>
        /// ע��SQLServer������
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public IServiceCollection RegisterSqlServerContext(IServiceCollection services)
        {
            services.Configure<DbContextOption>(options =>
            {
                options.ConnectionString = "initial catalog=NetCoreDemo;data source=127.0.0.1;password=admin123!@#;User id=sa;MultipleActiveResultSets=True;";
                options.ModelAssemblyName = "Zxw.Framework.Website.Models";
            });
            services.AddScoped<IDbContextCore, SqlServerDbContext>(); //ע��EF������
            return services;
        }
        /// <summary>
        /// ע��MySQL������
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public IServiceCollection RegisterMySqlContext(IServiceCollection services)
        {
            services.Configure<DbContextOption>(options =>
            {
                options.ConnectionString = "Server=183.230.47.18;Database=NetCoreDemo; User ID=root;Password=qazwsxedc123456;port=3306;CharSet=utf8;pooling=true;";
                options.ModelAssemblyName = "Zxw.Framework.Website.Models";
            });
            services.AddScoped<IDbContextCore, MySqlDbContext>(); //ע��EF������
            return services;
        }
        /// <summary>
        /// ע��PostgreSQL������
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public IServiceCollection RegisterPostgreSQLContext(IServiceCollection services)
        {
            services.Configure<DbContextOption>(options =>
            {
                options.ConnectionString = "User ID=zengxw;Password=123456;Host=localhost;Port=5432;Database=ZxwPgDemo;Pooling=true;";
                options.ModelAssemblyName = "Zxw.Framework.Website.Models";
            });
            services.AddScoped<IDbContextCore, PostgreSQLDbContext>(); //ע��EF������
            return services;
        }
    }
}
