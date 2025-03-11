
using MyRepositories;
using SqlSugar;

namespace MyWebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //builder.Services.AddSqlSugar(new SqlSugar.IOC.IocConfig()
            //{
            //    ConnectionString = builder.Configuration["SqlConn"],
            //    DbType = SqlSugar.IOC.IocDbType.SqlServer,
            //    IsAutoCloseConnection = true
            //});
            builder.Services.AddSingleton<ISqlSugarClient>(s =>
            {
                SqlSugarScope sqlSugar = new SqlSugarScope(new ConnectionConfig()
                {
                    DbType = SqlSugar.DbType.Sqlite,
                    ConnectionString = builder.Configuration["SqlConn"],
                    IsAutoCloseConnection = true,
                });
                return sqlSugar;
            });
            builder.Services.AddCustomIOC();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }

    public static class IOCExtend
    {
       public static IServiceCollection AddCustomIOC(this IServiceCollection services)
        {
            services.AddScoped<IBlogNewsRepository, BlogNewsRepository>();
            services.AddScoped<ITypeInfoRepository, TypeInfoRepository>();
            services.AddScoped<IWriterInfoRepository, WriterInfoRepository>();
            return services;
        }

        
    }
}
