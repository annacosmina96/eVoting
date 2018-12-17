using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistance
{ 
    public class EvotingDbContext : IdentityDbContext<User>
    {
       
        public EvotingDbContext(DbContextOptions<EvotingDbContext> options) : base(options)
        {


        }

        public DbSet<Ballot> Ballots { get; set; }
        public DbSet<BulletinBoard> BulletinBoards { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<TallyAuthority> TallyAuthorities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ballot>()
                        .HasKey(t => new { t.BulletinBoardId, t.VoterId });

            modelBuilder.Entity<BulletinBoard>()
                   .HasKey(t => new { t.BulletinBoardId });

            modelBuilder.Entity<TallyAuthority>()
                  .HasKey(t => new { t.TallyAuthorityId });

            modelBuilder.Entity<Candidate>().HasBaseType<User>();


            modelBuilder.Entity<Ballot>()
                .HasOne(pt => pt.Voter)
                .WithMany(t => t.Votes)
                .HasForeignKey(pt => pt.VoterId)
                .OnDelete(DeleteBehavior.Cascade);
            ;


            modelBuilder.Entity<Ballot>()
                .HasOne(pt => pt.BulletinBoard)
                .WithMany(t => t.Ballots)
                .HasForeignKey(pt => pt.BulletinBoardId)
                .OnDelete(DeleteBehavior.Cascade);
            ;


            modelBuilder.Entity<CandidatesBulletinBoards>()
                .HasKey(x => new { x.CandidateId, x.BulletinBoardId });

            modelBuilder.Entity<CandidatesBulletinBoards>()
                .HasOne(x => x.Candidate)
                .WithMany(y => y.CandidatesBulletinBoards)
                .HasForeignKey(y => y.CandidateId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<CandidatesBulletinBoards>()
                .HasOne(x => x.BulletinBoard)
                .WithMany(y => y.CandidatesBulletinBoards)
                .HasForeignKey(y => y.BulletinBoardId)
                .OnDelete(DeleteBehavior.Cascade);





            base.OnModelCreating(modelBuilder);
        }
    }
    }


