using Microsoft.EntityFrameworkCore;
   using ReportsAPI.Models;

   namespace ReportsAPI.Data
   {
       public class ReportsContext : DbContext
       {
           public DbSet<Sales> Sales { get; set; }
           public DbSet<Invoices> Invoices { get; set; }

           protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
           {
               optionsBuilder.UseSqlite(@"Data Source=c:\users\adeany\Documents\repos\Copilot-Lab-Files\db\Chinook.db");
           }
       }
   }
