using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NSA1.Core.Dto.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSA1.Core.Data
{
    public class ApplicationDbContext : IdentityDbContext<Register, Role, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<ClubAvaliabilityTime> ClubAvaliabilityTimes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<CommentUser> CommentUsers { get; set; }
        public DbSet<MemberProfile> MemberProfiles { get; set; }
        public DbSet<ModelProfile> ModelProfiles { get; set; }
        public DbSet<Update> Updates { get; set; }
        public DbSet<ProblemReport> ProblemReports { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
