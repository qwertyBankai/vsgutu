using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class EFDBContext : DbContext
    {
        public DbSet<Discipline> Discipline { get; set; }
        public DbSet<Groups> Groups { get; set; }
        public DbSet<Lesson> Lesson { get; set; }
        public DbSet<RolesOfUsers> RolesOfUsers { get; set; }
        public DbSet<Score> Score { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<TypeLesson> TypeLessons { get; set; }
        public DbSet<EK> EKs { get; set; }
        public DbSet<SessionScore> sessionScores { get; set; }


        public EFDBContext(DbContextOptions<EFDBContext> options) : base(options) { }

    }

    public class EFDBContextFactory : IDesignTimeDbContextFactory<EFDBContext>
    {
        public EFDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<EFDBContext>();
            optionsBuilder.UseSqlServer("Server=DESKTOP-A2JBAO1\\SQLEXPRESS;Database=vsgutu;Trusted_Connection=True;MultipleActiveResultSets=true", b => b.MigrationsAssembly("DataLayer"));

            return new EFDBContext(optionsBuilder.Options);
        }
    }
}
