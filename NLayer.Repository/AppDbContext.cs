using Microsoft.EntityFrameworkCore;
using NLayer.Core.Models;
using NLayer.Repository.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository
{
    public class AppDbContext :DbContext
    {
        //Veritabanı yolunu startup dosyasından vermek istediğimizde DbContextOptions'dan yardım alırız.
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options) 
        {
            //ProductFeature Product üzerinden de bu şekilde eklenebilir.
            //var p = new Product() { ProductFeature = new ProductFeature() { Color... } };
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductFeature> ProductFeatures { get; set; } //ProductFeature Product üzerinden de eklenebilir.

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Category>().HasKey(x => x.Id); //Bu sayfa dolmasın diye configurasyonları Configurasyon klasörü altına aldık.

            //Diğer klasörlerdeki configurationları iki farklı şekilde apply edebiliyoruz.
            //1.Tek tek söylemek ama çok fazla konfigurasyonun varsa uzun yol olacaktır.
            //modelBuilder.ApplyConfiguration(new ProductConfiguration());
            //2.yol:Reflection yardımıyla.
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); //Assembly.GetExecutingAssembly() = çalışmış olduğum katman(NLayer.Repository)daki konfigurasyonları bul.


            //ProductFeature örnek datasını da buradan yükleyelim :
            modelBuilder.Entity<ProductFeature>().HasData(new ProductFeature()
            {
                Id = 1,
                Color = "Kırmızı",
                Height = 100,
                Width = 200,
                ProductId = 1,
            }, 
            new ProductFeature()
            {
                Id = 2,
                Color = "Mavi",
                Height = 300,
                Width = 500,
                ProductId = 2,
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
