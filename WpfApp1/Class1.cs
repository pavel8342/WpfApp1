using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;


namespace WpfApp1
{
    public class Context : DbContext
    {
        public DbSet<история> историяs { get; set; }
        public DbSet<города> городаs { get; set; }
        public Context() : base(@"Data Source=localhost\MSSQLSERVER01; Initial Catalog = protgram; Integrated Security=True") { }
    }
    public class история
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int ID { get; set; }
        [Required] public string дата { get; set; }
        [Required] public string время { get; set; }
        [Required] public string город { get; set; }
        [Required] public string погода { get; set; }
        public virtual UserType UserType { get; set; }
    }
    public class города
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int ID { get; set; }
        [Required] public string город { get; set; }
        public virtual UserType UserType { get; set; }
    }
    public class UserType
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int ID { get; set; }
    }
    static public class d_sewe
    {
        public static Context db = new Context();
    }
}
