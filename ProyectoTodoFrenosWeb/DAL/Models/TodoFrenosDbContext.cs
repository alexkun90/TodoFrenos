using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models;

public partial class TodoFrenosDbContext : DbContext
{
    public TodoFrenosDbContext()
    {
    }

    public TodoFrenosDbContext(DbContextOptions<TodoFrenosDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer();
    }

    public virtual DbSet<Appointment> Appointments { get; set; }
    public virtual DbSet<Category> Categories { get; set; }
    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<Vehicle> Vehicles { get; set; }
    public virtual DbSet<VehicleInspection>VehicleInspections { get; set; }
    public virtual DbSet<SupplierList> SupplierLists { get; set; }
    public virtual DbSet<SupplierAppointment> SupplierAppointments { get; set; }
    public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }
    public virtual DbSet<CartItem> CartItems { get; set; }
    public virtual DbSet<Order> Orders { get; set; }
    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    /* 
     protected override void OnModelCreating(ModelBuilder modelBuilder)
     {
         modelBuilder.Entity<Appointment>(entity =>
         {
             entity.HasKey(e => e.AppointId).HasName("PK__Appointm__DCC1C939FBF15791");

             entity.Property(e => e.AppointCreationDate).HasColumnType("datetime");
             entity.Property(e => e.AppointModifyDate).HasColumnType("datetime");
             entity.Property(e => e.AppointState).HasMaxLength(50);
             entity.Property(e => e.Reason).HasMaxLength(255);
             entity.Property(e => e.UserId).HasColumnName("UserID");

             entity.HasOne(d => d.Vehicle).WithMany(p => p.Appointments)
                 .HasForeignKey(d => d.VehicleId)
                 .HasConstraintName("Fk_Apponint_VehicleId");
         });

         modelBuilder.Entity<Category>(entity =>
         {
             entity.HasKey(e => e.CategoryId).HasName("PK__Categori__19093A0B6D47F42D");

             entity.Property(e => e.CategoryName).HasMaxLength(255);
         });

         modelBuilder.Entity<InvoiceDetail>(entity =>
         {
             entity.HasKey(e => e.DetailId).HasName("PK__InvoiceD__135C316D25B0313B");

             entity.ToTable("InvoiceDetail");

             entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
             entity.Property(e => e.SubTotal).HasColumnType("decimal(18, 2)");
             entity.Property(e => e.Taxes).HasColumnType("decimal(18, 2)");
             entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");

             entity.HasOne(d => d.Master).WithMany(p => p.InvoiceDetails)
                 .HasForeignKey(d => d.MasterId)
                 .OnDelete(DeleteBehavior.ClientSetNull)
                 .HasConstraintName("Fk_Details_MasterId");

             entity.HasOne(d => d.Product).WithMany(p => p.InvoiceDetails)
                 .HasForeignKey(d => d.ProductId)
                 .OnDelete(DeleteBehavior.ClientSetNull)
                 .HasConstraintName("Fk_Details_ProductId");
         });

         modelBuilder.Entity<InvoiceMaster>(entity =>
         {
             entity.HasKey(e => e.MasterId);

             entity.ToTable("InvoiceMaster");

             entity.Property(e => e.DatePurchase).HasColumnType("datetime");
             entity.Property(e => e.SubTotal).HasColumnType("decimal(18, 2)");
             entity.Property(e => e.Taxes).HasColumnType("decimal(18, 2)");
             entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");
         });

         modelBuilder.Entity<Product>(entity =>
         {
             entity.HasKey(e => e.ProductId).HasName("PK__Products__B40CC6CD5D85E39A");

             entity.Property(e => e.ImageProduct).HasColumnName("Image_Product");
             entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
             entity.Property(e => e.ProductName).HasMaxLength(100);

             entity.HasOne(d => d.Category).WithMany(p => p.Products)
                 .HasForeignKey(d => d.CategoryId)
                 .HasConstraintName("Fk_CategoryId");
         });

         modelBuilder.Entity<ShoppingCart>(entity =>
         {
             entity.HasKey(e => e.CartId).HasName("PK__Shopping__51BCD7B70845E51C");

             entity.ToTable("ShoppingCart");

             entity.Property(e => e.DateCart).HasColumnType("datetime");

             entity.HasOne(d => d.Product).WithMany(p => p.ShoppingCarts)
                 .HasForeignKey(d => d.ProductId)
                 .HasConstraintName("Fk_Cart_ProductId");
         });

         modelBuilder.Entity<Vehicle>(entity =>
         {
             entity.Property(e => e.Brand).HasMaxLength(50);
             entity.Property(e => e.CreationDate).HasColumnType("datetime");
             entity.Property(e => e.Plate).HasMaxLength(20);
         });
         
         OnModelCreatingPartial(modelBuilder);
     }

     partial void OnModelCreatingPartial(ModelBuilder modelBuilder);*/
}
