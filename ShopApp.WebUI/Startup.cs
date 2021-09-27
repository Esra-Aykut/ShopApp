using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using ShopApp.Business.Abstract;
using ShopApp.Business.Concrete;
using ShopApp.Data.Abstract;
using ShopApp.Data.Concrete.EfCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApp.WebUI
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IProductRepository, EfCoreProductRepository>(); //IProductRepository �a�r�ld���nda bu I'in dolu kar��l��� olan EfCoreProductRepository gelsin
            services.AddScoped<IProductService, ProductManager>();  //IProductService �a�r�l�nca ProductManager �a�r�lacak.. Product Manager'da IProductManager �a�r�lacak.
                                                                    //Bunlardan sonra WebUI.csproj'da AddScopelar� yap.

            services.AddControllersWithViews();
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseHttpsRedirection();


            //******************************
            app.UseStaticFiles(); //wwroot alt�ndakilere eri�ebilmek i�in
            
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider= new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(),"node_modules")),
                RequestPath="/modules"
            });
            //******************************


            if (env.IsDevelopment()) //Uygulama geli�tirilirken yap�lanlar. Uygulama yay�nland���nda false d�ner. -> launch.json'da yazar: Development, Production.
            {
                SeedDatabase.Seed(); // ne zaman �al��acak: herhangi bir migration beklemiyor ve veritaban�n Product & Categori tablolar�nda herhangi bir bilgi yoksa verdi�imiz bilgiler DB'e eklenecek.
                app.UseDeveloperExceptionPage(); //hata old. zaman detay�n� g�rmemizi sa�layan k�s�m. (Sadece Geli�tirme a�amas�nda.) 
            }

            app.UseRouting();

            //localhost:5000
            //localhost:5000/home
            //localhost:5000/home/index
            //localhost:5000/product/details/2
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=home}/{action=index}/{id?}"
                );
            });
        }
    }
}
