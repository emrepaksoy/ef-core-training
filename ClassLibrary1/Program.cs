using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



#region DefaultConvention
// One_to_One ilişki türünde dependent entitiy nin hangisi olduğunu default olarak belirleyebilmek pek kolay değildir.
//Bu durumda foreign key e karşılık bir property/kolon  tanımlayarak çözüm üretilir. Bölyece foreign key e karşılık property tanımlayarak 
// lüzumsuz bir kolon oluşturmuş oluyoruz bu yaklaşımda.

//class Calisan
//{
//    public int Id { get; set; }
//    public string Adi { get; set; }
//    public CalisanAdresi CalisanAdresi { get; set; } // Navigation property

//}

//class CalisanAdresi
//{
//    public int Id { get; set; } //primary key
//    public int CalisanId { get; set; } // Calisan Entity Id ForeignKey
//    public string Adres { get; set; }

//    public Calisan Calisan { get; set; }  // Navigation property
//}
#endregion

#region Data Annotations
// bu yapılanmada iki entity arasında kurulacak ilişki attribute lar ile belirlenir.
// Foreign Key kolonunun ismi Default Convention'ın dışında bir kolon olacaksa Foreign Key attribute ile belirtilebilir.
// Foreign Key kolonu oluşturulmak zorunda değildir.Bire bir ilişkide ekstradan foreign key kolonuna ihtiyaqç yoktur.
// Dependent Entity deki Id kolonu hem foreign key hem primary key olarak kullanılabilir. Bu durum tercih edilir.


//class Calisan
//{
//    public int Id { get; set; }
//public string Adi { get; set; }
//public CalisanAdresi CalisanAdresi { get; set; } // Navigation property

//}

//class CalisanAdresi
//{
//    [Key, ForeignKey(nameof(Calisan))]
//    public int Id { get; set; } //primary key, foreign Key = Calisan.Id


//    //[ForeignKey(nameof(Calisan))]
//    //public int CalisanId { get; set; }
//    public string Adres { get; set; }

//    public Calisan Calisan { get; set; }  // Navigation property
//}
#endregion

#region Fluent API

// Fluent API yönteminde Entity ler arasındaki ilişki context sınıfı içerisinde OnModelCreating fonksiyonu
// override edilerek metotlar aracılığıyla tasarlanır.
class Calisan
{
    public int Id { get; set; }
    public string Adi { get; set; }
    public CalisanAdresi CalisanAdresi { get; set; } // Navigation property

}

class CalisanAdresi
{

    public int Id { get; set; } 

    public string Adres { get; set; }

    public Calisan Calisan { get; set; }  // Navigation property
}
#endregion


class ESirketDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("server=localhost\\sqlexpress06;database=ESirketDb;trusted_connection=true");
    }
    public DbSet<Calisan> Calisanlar { get; set; }
    public DbSet<CalisanAdresi> CalisanAdresleri { get; set; }


    //Entity lerin veritabanında generate edilecek yapıları bu fonksiyon içerisnde konfigure edilir.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CalisanAdresi>()
            .HasKey(x => x.Id); // CalisanAdresi içerisindeki Id primary key yapma.

        // Entity ler arasında bire bir ilişki kurma
        modelBuilder.Entity<Calisan>()
            .HasOne(c => c.CalisanAdresi)
            .WithOne(c => c.Calisan).
            HasForeignKey<CalisanAdresi>(c => c.Id);
    }
}