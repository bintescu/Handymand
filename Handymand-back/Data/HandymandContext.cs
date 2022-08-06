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

        public DbSet<User> Users { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Freelancer> Freelancers { get; set; }

        public DbSet<Skill> Skills { get; set; }
        public DbSet<FreelancersSkills> FreelancersSkills { get; set; }

        public DbSet<Contract> Contracts { get; set; }
        public DbSet<ContractsSkills> ContractsSkills { get; set; }

        public DbSet<JobOffer> JobOffer { get; set; }
        public DbSet<JobOffersSkills> JobOffersSkills { get; set; }

        public DbSet<City> Cities { get; set; }

/*        public DbSet<Feedback> Feedbacks { get; set; }*/

        public HandymandContext(DbContextOptions<HandymandContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //Properties

            modelBuilder.Entity<User>().Property(u => u.Amount).HasPrecision(12, 2);

            modelBuilder.Entity<Contract>().Property(u => u.PaymentAmount).HasPrecision(12, 2);
            //One to Many


            modelBuilder.Entity<Contract>()
                .HasOne(c => c.CreationUser).WithMany(u => u.CreatedContracts).HasForeignKey(c => c.IdCreationUser);


            modelBuilder.Entity<JobOffer>()
                .HasOne(j => j.City)
                .WithMany(c => c.JobOffers)
                .HasForeignKey(j => j.CityId);


/*            modelBuilder.Entity<Contract>()
                .HasOne(c => c.RefferedUser).WithMany(u => u.SubscribedContracts).HasForeignKey(c => c.IdRefferedUser);
*/

/*            modelBuilder.Entity<Feedback>()
                .HasOne(f => f.CreationUser).WithMany(u => u.SentFeedbacks).HasForeignKey(f => f.IdCreationUser);*/

            //One to One

            modelBuilder.Entity<User>()
                .HasOne(u => u.FreelancerAccount).WithOne(f => f.User).HasForeignKey<Freelancer>(f => f.IdUser);

            modelBuilder.Entity<User>()
                .HasOne(u => u.ClientAccount).WithOne(c => c.User).HasForeignKey<Client>(c => c.IdUser);

            //Many to Many

            modelBuilder.Entity<JobOffersSkills>().HasKey(key => new
            {
                key.IdSkill,
                key.IdJobOffer
            });

            modelBuilder.Entity<JobOffersSkills>()
                .HasOne<Skill>(js => js.Skill)
                .WithMany(s => s.JobOffersSkills)
                .HasForeignKey(js => js.IdSkill);

            modelBuilder.Entity<JobOffersSkills>()
                .HasOne<JobOffer>(js => js.JobOffer)
                .WithMany(j => j.JobOffersSkills)
                .HasForeignKey(js => js.IdJobOffer);

            modelBuilder.Entity<FreelancersSkills>().HasKey(key => new
            {
                key.IdFreelancer,
                key.IdSkill
            });

            modelBuilder.Entity<FreelancersSkills>()
                .HasOne<Freelancer>(fs => fs.Freelancer).WithMany(f => f.FreelancersSkills)
                .HasForeignKey(fs => fs.IdFreelancer);

            modelBuilder.Entity<FreelancersSkills>()
                .HasOne<Skill>(fs => fs.Skill).WithMany(s => s.FreelancersSkills)
                .HasForeignKey(fs => fs.IdSkill);


            modelBuilder.Entity<ContractsSkills>().HasKey(key => new 
            { 
                key.IdContract,
                key.IdSkill
            });

            modelBuilder.Entity<ContractsSkills>()
                .HasOne<Contract>(cs => cs.Contract).WithMany(c => c.ContractSkills)
                .HasForeignKey(cs => cs.IdContract);

            modelBuilder.Entity<ContractsSkills>()
                .HasOne<Skill>(cs => cs.Skill).WithMany(s => s.ContractSkills)
                .HasForeignKey(cs => cs.IdSkill);


            modelBuilder.Entity<Skill>(u =>
            {
                u.Property<DateTime>("DateCreated")
                    .HasDefaultValueSql("GETDATE()");

                u.Property<DateTime?>("DateModified")
                    .HasComputedColumnSql("GETDATE()");
            });


            //Seed Citites

            modelBuilder.Entity<City>().HasData
                (
                    new City() { Id = 1, Name = "Bucuresti"},
                    new City() { Id = 2, Name = "Iasi" },
                    new City() { Id = 3, Name = "Cluj" },
                    new City() { Id = 4, Name = "Timisoara" },
                    new City() { Id = 5, Name = "Constanta" },
                    new City() { Id = 6, Name = "Craiova" },
                    new City() { Id = 7, Name = "Brasov" },
                    new City() { Id = 8, Name = "Galati" },
                    new City() { Id = 9, Name = "Ploiesti" },
                    new City() { Id = 10, Name = "Oradea" },
                    new City() { Id = 11, Name = "Braila" },
                    new City() { Id = 12, Name = "Arad" },
                    new City() { Id = 13, Name = "Buzau"},
                    new City() { Id = 14, Name = "Botosani" },
                    new City() { Id = 15, Name = "Suceava" },
                    new City() { Id = 16, Name = "Drobeta-Turnu Severin"},
                    new City() { Id = 17, Name = "Slatina" },
                    new City() { Id = 18, Name = "Deva" }
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}
