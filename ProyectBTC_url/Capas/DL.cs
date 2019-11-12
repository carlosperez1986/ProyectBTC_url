using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Capas
{
    public class DL
    {
        // const string strcnn = @"Data Source=82.223.108.84,10606;Initial Catalog=btcDB; User ID=sa;Password=Carloselias23.; Packet Size=4096";
        //const string strcnn = @"Data Source=localhost;Initial Catalog=gamekeyspotdb;User ID=sa;Password=Carloselias23.;Packet Size=4096";

        public async Task<Dictionary<string, object>> xxAsync(ProductosModel model)
        {
            Dictionary<string, object> d = new Dictionary<string, object>();

            string sql = "select * from alojamientos";

            var xx = await FuncionesSQL.GetData(sql, null);

            List<SqlParameter> s = new List<SqlParameter>();

            return d;
        }
        //public class Context : DbContext
        //{
        //    public Context() : base(strcnn)
        //    {
        //        // Database.SetInitializer<SchoolDBContext>(null);
        //    }
        //    public DbSet<prueba> Prueba { get; set; }

        //    protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //    {
        //        //modelBuilder.Configurations.Add(new Prueba());
        //        //Configure domain classes using modelBuilder here..
        //    }
        //}

        //[Table("prueba")]
        //public class prueba
        //{
        //    public prueba() { }

        //    [Key]
        //    public int Id { get; set; }

        //    [Column("Name", TypeName = "ntext")]
        //    [MaxLength(20)]
        //    public string StudentName { get; set; }

        //    [NotMapped]
        //    public int? Age { get; set; }


        //    //public int StdId { get; set; }

        //    //[ForeignKey("StdId")]
        //    //public virtual Standard Standard { get; set; }
        //}

    }
}