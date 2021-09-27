﻿using Microsoft.EntityFrameworkCore;
using ShopApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Data.Concrete.EfCore
{
    public static class SeedDatabase
    {
        public static void Seed()
        {
            var context = new ShopContext();
            if (context.Database.GetPendingMigrations().Count()==0) //bütün migrationlar uygulandıysa
            {
                if(context.Categories.Count()==0)  //daha önce herhangi bir kategori eklenmemişse
                {
                    context.Categories.AddRange(Categories); //bilgiler DB'e eklenecek
                }

                if (context.Products.Count() == 0)   
                {
                    context.Products.AddRange(Products);    
                }
            }
            context.SaveChanges();
        }


        private static Category[] Categories = {
            new Category(){Name ="Telefon"},
            new Category(){Name = "Bilgisayar"},
            new Category(){Name = "Elektronik"},
        };
        private static Product[] Products = { 
            new Product(){Name="Samsung S5",Cost=2000,ImageUrl="1.jpg",Description="iyi telefon",IsApproved=true},
            new Product(){Name="Samsung S6",Cost=3000,ImageUrl="1.jpg",Description="iyi telefon",IsApproved=false},
            new Product(){Name="Samsung S7",Cost=4000,ImageUrl="1.jpg",Description="iyi telefon",IsApproved=true},
            new Product(){Name="Samsung S8",Cost=5000,ImageUrl="1.jpg",Description="iyi telefon",IsApproved=true},
            new Product(){Name="Samsung S9",Cost=6000,ImageUrl="1.jpg",Description="iyi telefon",IsApproved=true}
        };


    }
}
