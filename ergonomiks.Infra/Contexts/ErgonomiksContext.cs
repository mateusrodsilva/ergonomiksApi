using ergonomiks.Common.Enum;
using ergonomiks.Domain.Entities;
using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ergonomiks.Infra.Contexts
{
    public class ErgonomiksContext : DbContext
    {
        public ErgonomiksContext(DbContextOptions<ErgonomiksContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<Alerts> Alerts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Notification>();

            #region User
            modelBuilder.Entity<User>().ToTable("Users");

            //Primary Key
            modelBuilder.Entity<User>().Property(x => x.Id);            

            //Email
            modelBuilder.Entity<User>().Property(x => x.Email).HasMaxLength(60);
            modelBuilder.Entity<User>().Property(x => x.Email).HasColumnType("varchar(60)");
            modelBuilder.Entity<User>().Property(x => x.Email).IsRequired();
            modelBuilder.Entity<User>().HasIndex(x => x.Email).IsUnique();

            //Password
            modelBuilder.Entity<User>().Property(x => x.Password).HasMaxLength(60);
            modelBuilder.Entity<User>().Property(x => x.Password).HasColumnType("varchar(60)");
            modelBuilder.Entity<User>().Property(x => x.Password).IsRequired();          
            
            //CreationDate
            modelBuilder.Entity<User>().Property(t => t.CreationDate).HasColumnType("DateTime");
            modelBuilder.Entity<User>().Property(t => t.CreationDate).HasDefaultValueSql("GetDate()");
            #endregion         

            #region Company
            modelBuilder.Entity<Company>().ToTable("Companies");

            //Primary Key
            modelBuilder.Entity<Company>().Property(x => x.Id);

            //Foreign Key                      
            modelBuilder.Entity<Company>().HasOne(x => x.User).WithOne(x => x.Company).HasForeignKey<Company>(u => u.IdUser);

            //CorporateName
            modelBuilder.Entity<Company>().Property(x => x.CorporateName).HasMaxLength(80);
            modelBuilder.Entity<Company>().Property(x => x.CorporateName).HasColumnType("varchar(80)");
            modelBuilder.Entity<Company>().Property(x => x.CorporateName).IsRequired();
            modelBuilder.Entity<Company>().HasIndex(x => x.CorporateName).IsUnique();

            //Cnpj
            modelBuilder.Entity<Company>().Property(x => x.Cnpj).HasMaxLength(14);
            modelBuilder.Entity<Company>().Property(x => x.Cnpj).HasColumnType("varchar(14)");
            modelBuilder.Entity<Company>().Property(x => x.Cnpj).IsRequired();
            modelBuilder.Entity<Company>().HasIndex(x => x.Cnpj).IsUnique();

            //CEP or Zip Code
            modelBuilder.Entity<Company>().Property(x => x.Cep).HasMaxLength(12);
            modelBuilder.Entity<Company>().Property(x => x.Cep).HasColumnType("varchar(12)");
            modelBuilder.Entity<Company>().Property(x => x.Cep).IsRequired();

            //Country
            modelBuilder.Entity<Company>().Property(x => x.Country).HasMaxLength(3);
            modelBuilder.Entity<Company>().Property(x => x.Country).HasColumnType("varchar(3)");
            modelBuilder.Entity<Company>().Property(x => x.Country).IsRequired();


            //CreationDate
            modelBuilder.Entity<User>().Property(t => t.CreationDate).HasColumnType("DateTime");
            modelBuilder.Entity<User>().Property(t => t.CreationDate).HasDefaultValueSql("GetDate()");

            #endregion

            #region Manager
            modelBuilder.Entity<Manager>().ToTable("Managers");

            //Primary Key
            modelBuilder.Entity<Manager>().HasKey(x => x.Id);

            //Foreign Key
            modelBuilder.Entity<Manager>().HasOne(x => x.User).WithMany(x => x.Manager).HasForeignKey(x => x.IdUser).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Manager>().HasOne(x => x.Company).WithMany(x => x.Manager).HasForeignKey(x => x.IdCompany).OnDelete(DeleteBehavior.NoAction);

            //FirstName
            modelBuilder.Entity<Manager>().Property(x => x.FirstName).HasMaxLength(80);
            modelBuilder.Entity<Manager>().Property(x => x.FirstName).HasColumnType("varchar(80)");
            modelBuilder.Entity<Manager>().Property(x => x.FirstName).IsRequired();           

            //LastName
            modelBuilder.Entity<Manager>().Property(x => x.LastName).HasMaxLength(80);
            modelBuilder.Entity<Manager>().Property(x => x.LastName).HasColumnType("varchar(80)");
            modelBuilder.Entity<Manager>().Property(x => x.LastName).IsRequired();

            //Phone
            modelBuilder.Entity<Manager>().Property(x => x.Phone).HasMaxLength(14);
            modelBuilder.Entity<Manager>().Property(x => x.Phone).HasColumnType("varchar(14)");
            modelBuilder.Entity<Manager>().Property(x => x.Phone).IsRequired();
            modelBuilder.Entity<Manager>().HasIndex(x => x.Phone).IsUnique();

            //Image
            modelBuilder.Entity<Manager>().Property(x => x.Image).HasMaxLength(255);
            modelBuilder.Entity<Manager>().Property(x => x.Image).HasColumnType("varchar(255)");
            modelBuilder.Entity<Manager>().Property(x => x.Image).IsRequired();

            //CreationDate
            modelBuilder.Entity<User>().Property(t => t.CreationDate).HasColumnType("DateTime");
            modelBuilder.Entity<User>().Property(t => t.CreationDate).HasDefaultValueSql("GetDate()");
            #endregion

            #region Employee
            modelBuilder.Entity<Employee>().ToTable("Employees");

            //Primary Key
            modelBuilder.Entity<Employee>().Property(x => x.Id);

            //Foreign Key
            modelBuilder.Entity<Employee>().HasOne(x => x.User).WithMany(x => x.Employee).HasForeignKey(x => x.IdUser).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Employee>().HasOne(x => x.Company).WithMany(x => x.Employee).HasForeignKey(x => x.IdCompany).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Employee>().HasOne(x => x.Manager).WithMany(x => x.Employee).HasForeignKey(x => x.IdManager).OnDelete(DeleteBehavior.NoAction);

            //FirstName
            modelBuilder.Entity<Employee>().Property(x => x.FirstName).HasMaxLength(80);
            modelBuilder.Entity<Employee>().Property(x => x.FirstName).HasColumnType("varchar(80)");
            modelBuilder.Entity<Employee>().Property(x => x.FirstName).IsRequired();

            //LastName
            modelBuilder.Entity<Employee>().Property(x => x.LastName).HasMaxLength(80);
            modelBuilder.Entity<Employee>().Property(x => x.LastName).HasColumnType("varchar(80)");
            modelBuilder.Entity<Employee>().Property(x => x.LastName).IsRequired();

            //Phone
            modelBuilder.Entity<Employee>().Property(x => x.Phone).HasMaxLength(14);
            modelBuilder.Entity<Employee>().Property(x => x.Phone).HasColumnType("varchar(14)");
            modelBuilder.Entity<Employee>().Property(x => x.Phone).IsRequired();
            modelBuilder.Entity<Employee>().HasIndex(x => x.Phone).IsUnique();

            //Image
            modelBuilder.Entity<Employee>().Property(x => x.Image).HasMaxLength(255);
            modelBuilder.Entity<Employee>().Property(x => x.Image).HasColumnType("varchar(255)");
            modelBuilder.Entity<Employee>().Property(x => x.Image).IsRequired();

            //CreationDate
            modelBuilder.Entity<User>().Property(t => t.CreationDate).HasColumnType("DateTime");
            modelBuilder.Entity<User>().Property(t => t.CreationDate).HasDefaultValueSql("GetDate()");
            #endregion

            #region Equipment
            modelBuilder.Entity<Equipment>().ToTable("Equipment");

            //Primary Key
            modelBuilder.Entity<Equipment>().Property(x => x.Id);

            //Foreign Key
            modelBuilder.Entity<Equipment>().HasOne(x => x.Employee).WithMany(x => x.Equipment).HasForeignKey(x => x.IdEmployee).OnDelete(DeleteBehavior.NoAction);

            //Temperature
            modelBuilder.Entity<Equipment>().Property(x => x.Temperature).HasMaxLength(5);
            modelBuilder.Entity<Equipment>().Property(x => x.Temperature).HasColumnType("varchar(5)");

            //LightLevel
            modelBuilder.Entity<Equipment>().Property(x => x.LightLevel).HasMaxLength(5);
            modelBuilder.Entity<Equipment>().Property(x => x.LightLevel).HasColumnType("varchar(5)");

            //Moisture
            modelBuilder.Entity<Equipment>().Property(x => x.Moisture).HasMaxLength(5);
            modelBuilder.Entity<Equipment>().Property(x => x.Moisture).HasColumnType("varchar(5)");

            //CreationDate
            modelBuilder.Entity<User>().Property(t => t.CreationDate).HasColumnType("DateTime");
            modelBuilder.Entity<User>().Property(t => t.CreationDate).HasDefaultValueSql("GetDate()");
            #endregion

            #region Alerts
            modelBuilder.Entity<Alerts>().ToTable("Alerts");

            //Primary Key
            modelBuilder.Entity<Alerts>().Property(x => x.Id);

            //Foreign Key
            modelBuilder.Entity<Alerts>().HasOne(x => x.Equipment).WithMany(x => x.Alerts).HasForeignKey(x => x.IdEquipment).OnDelete(DeleteBehavior.NoAction);

            //Title
            modelBuilder.Entity<Alerts>().Property(x => x.Title).HasMaxLength(60);
            modelBuilder.Entity<Alerts>().Property(x => x.Title).HasColumnType("varchar(60)");

            //Message
            modelBuilder.Entity<Alerts>().Property(x => x.Message).HasMaxLength(60);
            modelBuilder.Entity<Alerts>().Property(x => x.Message).HasColumnType("varchar(60)");

            //CreationDate
            modelBuilder.Entity<User>().Property(t => t.CreationDate).HasColumnType("DateTime");
            modelBuilder.Entity<User>().Property(t => t.CreationDate).HasDefaultValueSql("GetDate()");
            #endregion

        }
    }
}
