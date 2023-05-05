using ConsultaSerpro.Models;
using ConsultaSerpro.Models.Servico;
using ConsultaSerpro.Repositorio;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ConsultaSerpro
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllers();
            var stringConexao = builder.Configuration.GetValue<string>("Conexao:ConnectionString");
            builder.Services.AddDbContext<ApiDBContext>(options => options.UseNpgsql(stringConexao));


            builder.Services.AddTransient<IDadosConsultaRepositorio, DadosConsultaRepositorio>();
            builder.Services.AddTransient<IUsuarioRepositorio, UsuarioRepositorio>();
            builder.Services.AddTransient<IConsumoSerpro, ConsumoSerpro>();
            builder.Services.AddTransient<IRetornoMultaRepositorio, RetornoMultaRepositorio>();
            builder.Services.AddTransient<IRetornoVeiculoRepositorio, RetornoVeiculoRepositorio>();
            builder.Services.AddTransient<IRetornoCNHRepositorio, RetornoCNHRepositorio>();


            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Consulta dados Serpro",
                    Version = "v1",
                    Description = "API de acesso a informações disponibilizada pelo Serpro",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Name = "Nome da empresa",
                        Email = "email da empresa",
                        Url = new Uri("site.com")
                    }
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}