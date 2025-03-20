
using MyModels;
using MyRepositories;
using SqlSugar;

namespace MyJWT
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
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
            builder.Services.AddScoped<IWriterInfoRepository, WriterInfoRepository>();
            builder.Services.Configure<JwtOption>(builder.Configuration.GetSection(nameof(JwtOption)));
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
}
