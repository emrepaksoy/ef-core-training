using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



#region DefaultConvention
// bire çok ilişkiler de bu yapılanmada foreign key kolonuna karşılık gelen property tanımlamak zorunda değiliz. Eğer tanımlamazsak
// Ef core bunu kendisi tanımlayacaktır. Eğer kendimiz tanımlamarsak tanımladığımız kolonu baz alacaktır.
//class Calisan //Dependent Entity
//{
//    public int Id { get; set; }
//    public string Adi { get; set; }
//    public Departman Departman { get; set; } // Navigation Property

//}

//class Departman
//{
//    public int Id { get; set; }
//    public string DepartmanAdi { get; set; }

//    public ICollection<Calisan>  Calisanlar { get; set; } //Navigation property
//}
#endregion


#region Data Annotations
//bu yapılanmada foreign key kolonuna property tanımlarken property ismi geleneksel entity tanımlama kurallarına
//uymuyorsa bu yapılanma ile müdahale edilebilir. 
//class Calisan //Dependent Entity
//{
//    public int Id { get; set; }

//    [ForeignKey(nameof(Departman))]
//    public int DId { get; set; }
//    public string Adi { get; set; }
//    public Departman Departman { get; set; }

//}

//class Departman
//{
//    public int Id { get; set; }

//    public string DepartmanAdi { get; set; }

//    public ICollection<Calisan> Calisanlar { get; set; }
//}
#endregion

#region Fluent API
class Calisan //Dependent Entity
{
    public int Id { get; set; }

    //public int DId { get; set; }
    public string Adi { get; set; }
    public Departman Departman { get; set; }

}

class Departman
{
    public int Id { get; set; }

    public string DepartmanAdi { get; set; }

    public ICollection<Calisan> Calisanlar { get; set; }
}

#endregion


class ESirketDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("server=localhost\\sqlexpress06;database=ESirketDb;trusted_connection=true");
    }
    public DbSet<Calisan> Calisanlar { get; set; }
    public DbSet<Departman> Departmanlar { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Calisan>()
            .HasOne(a => a.Departman)
            .WithMany(d => d.Calisanlar);
            //.HasForeignKey(c => c.DId);
    }   
}