using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using apiSupplier.Entities;


namespace apiSupplier
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
           
            services.AddMemoryCache();
            services.AddCors();
            services.AddControllers();

            services.AddConsulConfig(Configuration);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Version = "v1", Title = "apiSupplier", Description = "Api Supplier" });
            });

            services.AddHttpClient<msProductoClient>((client) =>
            {
                client.BaseAddress = new Uri(Configuration.GetSection("Configuration").GetValue<string>("msproducto.urlbase"));
            });
            services.AddHttpClient<msMembresiaClient>((client) =>
            {
                client.BaseAddress = new Uri(Configuration.GetSection("Configuration").GetValue<string>("msmembresia.urlbase"));
            });
            services.AddHttpClient<msSalaClient>((client) =>
            {
                client.BaseAddress = new Uri(Configuration.GetSection("Configuration").GetValue<string>("mssala.urlbase"));
                //client.BaseAddress = new Uri(Configuration.GetSection("Configuration").GetValue<string>("mstipo.urlbase"));
            });
            services.AddHttpClient<msPaqueteClient>((client) =>
            {
                client.BaseAddress = new Uri(Configuration.GetSection("Configuration").GetValue<string>("mspaquete.urlbase"));
            });
            services.AddHttpClient<msNotificacionesClient>((client) =>
            {
                client.BaseAddress = new Uri(Configuration.GetSection("Configuration").GetValue<string>("msnotificaciones.urlbase"));
            });
            services.AddHttpClient<msCalificacionClient>((client) =>
            {
                client.BaseAddress = new Uri(Configuration.GetSection("Configuration").GetValue<string>("mscalificacion.urlbase"));
            });
            services.AddHttpClient<msTransaccionClient>((client) =>
            {
                client.BaseAddress = new Uri(Configuration.GetSection("Configuration").GetValue<string>("mstransaccion.urlbase"));
            });
            services.AddHttpClient<msSearchClient>((client) =>
            {
                client.BaseAddress = new Uri(Configuration.GetSection("Configuration").GetValue<string>("mssearch.urlbase"));
            });
            services.AddHttpClient<msTipoClient>((client) =>
            {
                client.BaseAddress = new Uri(Configuration.GetSection("Configuration").GetValue<string>("mstipo.urlbase"));
            });
            services.AddHttpClient<msMailClient>((client) =>
            {
                client.BaseAddress = new Uri(Configuration.GetSection("Configuration").GetValue<string>("msmail.urlbase"));
            });
            services.AddHttpClient<msUsuarioClient>((client) =>
            {
                client.BaseAddress = new Uri(Configuration.GetSection("Configuration").GetValue<string>("msusuario.urlbase"));
            });
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UsePathBase("/apiSupplier");
            app.UseSwagger(c => {
                c.SerializeAsV2 = true;
                c.RouteTemplate = "swagger/{documentName}/swagger.json";
            });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "apiSupplier");
                c.RoutePrefix = "swagger";
            });
            app.UseExceptionHandler("/error");
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseHttpsRedirection();
            //app.UseConsul();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
