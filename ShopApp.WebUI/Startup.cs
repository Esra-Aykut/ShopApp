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
            services.AddScoped<ICategoryRepository, EfCoreCategoryRepository>(); //IProductRepository çaðrýldýðýnda bu I'in dolu karþýlýðý olan EfCoreProductRepository gelsin
            services.AddScoped<ICategoryService, CategoryManager>(); //IProductRepository çaðrýldýðýnda bu I'in dolu karþýlýðý olan EfCoreProductRepository gelsin
           
            services.AddScoped<IProductRepository, EfCoreProductRepository>(); //IProductRepository çaðrýldýðýnda bu I'in dolu karþýlýðý olan EfCoreProductRepository gelsin
            services.AddScoped<IProductService, ProductManager>();  //IProductService çaðrýlýnca ProductManager çaðrýlacak.. Product Manager'da IProductManager çaðrýlacak.
                                                                    //Bunlardan sonra WebUI.csproj'da AddScopelarý yap.
            services.AddControllersWithViews();
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
       
        
        
        
        
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //******************************
            app.UseStaticFiles(); //wwroot altýndakilere eriþebilmek için
            
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider= new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(),"node_modules")),
                    RequestPath="/modules"
            });
            //******************************


            if (env.IsDevelopment()) //Uygulama geliþtirilirken yapýlanlar. Uygulama yayýnlandýðýnda false döner. -> launch.json'da yazar: Development, Production.
            {
                SeedDatabase.Seed(); // ne zaman çalýþacak: herhangi bir migration beklemiyor ve veritabanýn Product & Categori tablolarýnda herhangi bir bilgi yoksa verdiðimiz bilgiler DB'e eklenecek.
                app.UseDeveloperExceptionPage(); //hata old. zaman detayýný görmemizi saðlayan kýsým. (Sadece Geliþtirme aþamasýnda.) 
            }

            app.UseRouting();

            

                //localhost:5000
                //localhost:5000/home
                //localhost:5000/home/index
                //localhost:5000/product/details/2
                app.UseEndpoints(endpoints =>
                {

                    endpoints.MapControllerRoute(
                        name: "adminproducts",
                        pattern: "admin/products",
                        defaults: new { controller = "Admin", action = "ProductList" }

                     );

                    endpoints.MapControllerRoute(
                        name: "adminproductcreate",
                        pattern: "admin/products/create",
                        defaults: new { controller = "Admin", action = "ProductCreate" }
                     );

                    endpoints.MapControllerRoute(
                        name: "adminproductedit",
                        pattern: "admin/products/{id?}",
                        defaults: new { controller = "Admin", action = "ProductEdit" }
                     );

                    endpoints.MapControllerRoute(
                        name: "admincategories",
                        pattern: "admin/categories",
                        defaults: new { controller = "Admin", action = "CategoryList" }
                     );        
                    
                    endpoints.MapControllerRoute(
                        name: "admincategoriycreate",
                        pattern: "admin/categories/create",
                        defaults: new { controller = "Admin", action = "CategoryCreate" }
                     );

                    endpoints.MapControllerRoute(
                        name: "admincategoryedit",
                        pattern: "admin/categories/{id?}",
                        defaults: new { controller = "Admin", action = "CategoryEdit" }
                     );


                    endpoints.MapControllerRoute(
                        name: "search",
                        pattern: "search",
                        defaults: new { controller = "Shop", action = "search" }
                     );



                    //{productname} -> Samsung S5
                    //{productname}-{id} -> Samsung S5-10

                    endpoints.MapControllerRoute(
                        name: "productdetails",
                        pattern: "{url}", //shopController>>details'e gidecek ve giderken götürdüðü parametre=url
                        defaults: new { controller = "Shop", action = "details" }
                    );

                    endpoints.MapControllerRoute(
                        name: "products",   //route'a bir isim verelim
                        pattern: "products/{category?}",  //"/products" , "/products/telefon" 
                        defaults: new { controller = "Shop", action = "list" } //kullanýcý "products"a gittiðinde gelecek olan default deðer 
                    );

                    endpoints.MapControllerRoute(   //diðerlerini defaultun üstünde tanýmla
                        name: "default",
                        pattern: "{controller=Home}/{action=Index}/{id?}" //conroller olmadýðý zaman:home, action olmadýðý zaman:index gelsin default. id olabilir de olmayabilir de.
                    );
            });
        }
    }
}
