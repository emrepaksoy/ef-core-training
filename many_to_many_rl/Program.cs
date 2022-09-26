using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


#region Default Convention
//İki entity arasındaki ilişki navigation propertyler üzerinden çoğul olarak kurulmalıdır. (ICollection, List)
//Default Convention'da cross table'ı manuel oluşturmak zorunda değiliz. EF Core tasarıma uygun bir şekilde
//cross table'ı kendisi otomatik basacak ve generate edecektir.
//Ve oluşturulan cross table'ın içerisinde composite primary key'i de otomatik oluşturmuş olacaktır.

//class Kitap
//{
//    public int Id { get; set; }
//public string KitapAdi { get; set; }

//public List<Yazar> Yazarlar { get; set; } // Navigation property

//}
//class Yazar
//{
//    public int Id { get; set; }
//    public string YazarAdi { get; set; }

//    public List<Kitap> Kitaplar { get; set; } // Navigation property
//}
#endregion

#region Data Annotations
// Cross table manuel olarak oluşturulmak zorundadır.
// Entityler arasında navigation propertyler ile ilişki kurulmalıdır.
// Cross Table da composite primary key data annotations lar ile manuel olarak kurulamıyor.Bunun içn Fluent API oluşturulmalıdır.

// Cross Table karlışık bir entity modeli oluşturuyorsak bunu manuel olarak context sınıfı içerisnde DbSet property
// olarak bildirmek zorunda değiliz. Ef core ilişkiler üzerinde cross table oluşturur.
//class Kitap
//{
//    public int Id { get; set; }
//    public string KitapAdi { get; set; }

//    public ICollection<KitapYazar> Yazarlar { get; set; } // Navigation property

//}

//// Cross Table
//class KitapYazar
//{   
//    public int YazarId { get; set; }
//    public int KitapId { get; set; }
//    public Yazar Yazar {get; set;}
//    public Kitap Kitap{get; set;}
//}
//class Yazar
//{
//    public int Id { get; set; }
//    public string YazarAdi { get; set; }

//    public ICollection<Kitap> Kitaplar { get; set; } // Navigation property
//}
#endregion

#region Fluent API
// Cross Table manuel olarak oluşturulmalı. Dbset olarak tanımlanmasına gerek yok.
// Composit Primary Key HasKey metodu ile kurulmalıdır.
class Kitap
{
    public int Id { get; set; }
    public string KitapAdi { get; set; }

    public ICollection<KitapYazar> Yazarlar  { get; set; }

}
//Cross Table 
class KitapYazar
{
    public int KitapId { get; set; }
    public int  YazarId { get; set; }

    public Kitap Kitap { get; set; }
    public Yazar Yazar { get; set; }

}
class Yazar
{
    public int Id { get; set; }
    public string YazarAdi { get; set; }
    public ICollection<KitapYazar> Kitaplar { get; set; }


}
#endregion
class ESirketDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("server=localhost\\sqlexpress06;database=EKitapDB;trusted_connection=true");
    }
    public DbSet<Kitap> Kitaplar { get; set; }
    public DbSet<Yazar> Yazarlar { get; set; }




    // Data Annotations
    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    modelBuilder.Entity<KitapYazar>()
    //        .HasKey(ky  => new {ky.KitapId, ky.YazarId}); // composite primary key  oluşturma
    //}



    //Fluent API
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<KitapYazar>()
              .HasKey(ky => new { ky.KitapId, ky.YazarId });

        modelBuilder.Entity<KitapYazar>()
             .HasOne(ky => ky.Kitap)
             .WithMany(y => y.Yazarlar)
             .HasForeignKey(k => k.KitapId);

        modelBuilder.Entity<KitapYazar>()
            .HasOne(ky => ky.Yazar)
            .WithMany(k => k.Kitaplar)
            .HasForeignKey(y => y.YazarId);
    }


}
