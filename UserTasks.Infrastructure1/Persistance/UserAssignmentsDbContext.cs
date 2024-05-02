using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserTasks.Domain.Models;

namespace UserTasks.Infrastructure.Persistance;

public class UserAssignmentsDbContext : DbContext
{
    public UserAssignmentsDbContext()
    {
    }

    public UserAssignmentsDbContext(DbContextOptions<UserAssignmentsDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    public DbSet<Assignment> Assignments { get; set; }

    public DbSet<UserAssigmentHistory> UserAssigmentHistories { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connectionString = "Server=127.0.0.1;Port=3332;Database=UserAssignments;Uid=root;Pwd=root;";//todo
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Assignment>()
                   .HasOne<User>(a => a.User)
                   .WithMany(u => u.Assignments)
                   .HasForeignKey(a => a.UserId);

        modelBuilder.Entity<UserAssigmentHistory>()
           .HasOne<User>(uah => uah.User)
           .WithMany(u => u.UserAssigmentHistories)
           .HasForeignKey(uah => uah.UserId);

        modelBuilder.Entity<UserAssigmentHistory>()
           .HasOne<Assignment>(uah => uah.Assigment)
           .WithMany(a => a.UserAssigmentHistories)
           .HasForeignKey(uah => uah.AssigmentId);

        modelBuilder.Entity<UserAssigmentHistory>().HasIndex(uah => new { uah.UserId, uah.AssigmentId }).IsUnique();

        modelBuilder.Entity<User>().HasIndex(u => u.Name).IsUnique();


        var data = SeedData.Get();

        modelBuilder.Entity<User>().HasData(data.Users);
        modelBuilder.Entity<Assignment>().HasData(data.Assignments);

    }
}

