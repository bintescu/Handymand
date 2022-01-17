using Handymand.Models;
using Handymand.Models.Many_to_Many;
using Handymand.Models.One_to_Many;
using Handymand.Models.One_to_One;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Handymand.Data
{
    public class HandymandContext : DbContext
    {
        //public DbSet<DatabaseModel> DataBaseModels { get; set; }


        //One to Many

        public DbSet<Model1OM> Model1OMs { get; set; }
        public DbSet<Model2OM> Model2OMs { get; set; }

        public DbSet<Model1OO> Model1OOs { get; set; }
        public DbSet<Model2OO> Model2OOs { get; set; }

        public DbSet<Model1MM> Model1MMs { get; set; }
        public DbSet<Model2MM> Model2MMs { get; set; }
        public DbSet<ModelsRelation> ModelsRelations { get; set; }

        public HandymandContext(DbContextOptions<HandymandContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //One to Many
            modelBuilder.Entity<Model1OM>()
                .HasMany(m2 => m2.Models2OM).WithOne(m1 => m1.Model1OM);

            /*            modelBuilder.Entity<Model2OM>()
                            .HasOne(m1 => m1.Model1OM).WithMany(m2 => m2.Models2OM);*/


            //One to One

            modelBuilder.Entity<Model1OO>()
                .HasOne(m2 => m2.Model2OO).WithOne(m1 => m1.Model1OO).HasForeignKey<Model2OO>(m1 => m1.Model1Id);



            //Many to Many

            modelBuilder.Entity<ModelsRelation>().HasKey(key => new
            {
                key.Model1MMId,
                key.Model2MMId
            });

            modelBuilder.Entity<ModelsRelation>()
                .HasOne<Model1MM>(mr => mr.Model1MM).WithMany(m1 => m1.ModelsRelations)
                .HasForeignKey(mr => mr.Model1MMId);

            modelBuilder.Entity<ModelsRelation>()
                        .HasOne<Model2MM>(mr => mr.Model2MM).WithMany(m1 => m1.ModelsRelations)
                        .HasForeignKey(mr => mr.Model2MMId);


            base.OnModelCreating(modelBuilder);
        }
    }
}
