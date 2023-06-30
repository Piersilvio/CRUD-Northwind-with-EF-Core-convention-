using DaoDbNorthwind.contract.enities;
using DaoDbNorthwind.entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using static DaoDbNorthwind.entities.OrderDetails;

namespace DaoDbNorthwind.data
{
    public class NorthwindContext : DbContext
    {
        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Supliers> Supliers { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<OrderDetails> OrderDetails { get; set; }

        private  IConfiguration Configuration { get; }

        private readonly string? connString;
                            
        public NorthwindContext()
        {
            var configurationBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, false);

            Configuration = configurationBuilder.Build();
            connString = Configuration.GetConnectionString("MyConn");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connString);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //fk Supliers
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasIndex(e => e.CategoryName, "CategoryName");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
                entity.Property(e => e.CategoryName).HasMaxLength(15);
                entity.Property(e => e.Description).HasColumnType("ntext");
                entity.Property(e => e.Picture).HasColumnType("image");
            });

            modelBuilder.Entity<Customers>(entity =>
            {
                entity.HasIndex(e => e.City, "City");

                entity.HasIndex(e => e.CompanyName, "CompanyName");

                entity.HasIndex(e => e.PostalCode, "PostalCode");

                entity.HasIndex(e => e.Region, "Region");

                entity.Property(e => e.CustomerId)
                    .HasMaxLength(5)
                    .IsFixedLength()
                    .HasColumnName("CustomerID");

                entity.Property(e => e.Address).HasMaxLength(60);
                entity.Property(e => e.City).HasMaxLength(15);
                entity.Property(e => e.CompanyName).HasMaxLength(40);
                entity.Property(e => e.ContactName).HasMaxLength(30);
                entity.Property(e => e.ContactTitle).HasMaxLength(30);
                entity.Property(e => e.Country).HasMaxLength(15);
                entity.Property(e => e.Fax).HasMaxLength(24);
                entity.Property(e => e.Phone).HasMaxLength(24);
                entity.Property(e => e.PostalCode).HasMaxLength(10);
                entity.Property(e => e.Region).HasMaxLength(15);
            });

            modelBuilder.Entity<Employees>(entity =>
            {
                entity.HasIndex(e => e.LastName, "LastName");

                entity.HasIndex(e => e.PostalCode, "PostalCode");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
                entity.Property(e => e.Address).HasMaxLength(60);
                entity.Property(e => e.BirthDate).HasColumnType("datetime");
                entity.Property(e => e.City).HasMaxLength(15);
                entity.Property(e => e.Country).HasMaxLength(15);
                entity.Property(e => e.FirstName).HasMaxLength(10);
                entity.Property(e => e.HireDate).HasColumnType("datetime");
                entity.Property(e => e.LastName).HasMaxLength(20);
                entity.Property(e => e.PostalCode).HasMaxLength(10);
                entity.Property(e => e.Region).HasMaxLength(15);
                entity.Property(e => e.Title).HasMaxLength(30);
                entity.Property(e => e.TitleOfCourtesy).HasMaxLength(25);
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasIndex(e => e.CustomerId, "CustomerID");

                entity.HasIndex(e => e.CustomerId, "CustomersOrders");

                entity.HasIndex(e => e.EmployeeId, "EmployeeID");

                entity.HasIndex(e => e.EmployeeId, "EmployeesOrders");

                entity.HasIndex(e => e.OrderDate, "OrderDate");

                entity.HasIndex(e => e.ShipPostalCode, "ShipPostalCode");

                entity.HasIndex(e => e.ShippedDate, "ShippedDate");

                entity.HasIndex(e => e.ShipVia, "ShippersOrders");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");
                entity.Property(e => e.CustomerId)
                    .HasMaxLength(5)
                    .IsFixedLength()
                    .HasColumnName("CustomerID");
                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
                entity.Property(e => e.Freight)
                    .HasDefaultValueSql("((0))")
                    .HasColumnType("money");
                entity.Property(e => e.OrderDate).HasColumnType("datetime");
                entity.Property(e => e.RequiredDate).HasColumnType("datetime");
                entity.Property(e => e.ShipAddress).HasMaxLength(60);
                entity.Property(e => e.ShipCity).HasMaxLength(15);
                entity.Property(e => e.ShipCountry).HasMaxLength(15);
                entity.Property(e => e.ShipName).HasMaxLength(40);
                entity.Property(e => e.ShipPostalCode).HasMaxLength(10);
                entity.Property(e => e.ShipRegion).HasMaxLength(15);
                entity.Property(e => e.ShippedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Orders_Customers");

                entity.HasOne(d => d.Employee).WithMany(p => p.Orders)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_Orders_Employees");

                entity.HasOne(d => d.ShipViaNavigation).WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ShipVia)
                    .HasConstraintName("FK_Orders_Shippers");
            });

            modelBuilder.Entity<OrderDetails>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.ProductId }).HasName("PK_Order_Details");

                entity.ToTable("Order Details");

                entity.HasIndex(e => e.OrderId, "OrderID");

                entity.HasIndex(e => e.OrderId, "OrdersOrder_Details");

                entity.HasIndex(e => e.ProductId, "ProductID");

                entity.HasIndex(e => e.ProductId, "ProductsOrder_Details");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");
                entity.Property(e => e.ProductId).HasColumnName("ProductID");
                entity.Property(e => e.Quantity).HasDefaultValueSql("((1))");
                entity.Property(e => e.UnitPrice).HasColumnType("money");

                entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Details_Orders").OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Details_Products");
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.HasIndex(e => e.CategoryId, "CategoriesProducts");

                entity.HasIndex(e => e.CategoryId, "CategoryID");

                entity.HasIndex(e => e.ProductName, "ProductName");

                entity.HasIndex(e => e.SupplierId, "SupplierID");

                entity.HasIndex(e => e.SupplierId, "SuppliersProducts");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");
                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
                entity.Property(e => e.ProductName).HasMaxLength(40);
                entity.Property(e => e.QuantityPerUnit).HasMaxLength(20);
                entity.Property(e => e.ReorderLevel).HasDefaultValueSql("((0))");
                entity.Property(e => e.SupplierId).HasColumnName("SupplierID");
                entity.Property(e => e.UnitPrice)
                    .HasDefaultValueSql("((0))")
                    .HasColumnType("money");
                entity.Property(e => e.UnitsInStock).HasDefaultValueSql("((0))");
                entity.Property(e => e.UnitsOnOrder).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Category).WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Products_Categories");

                entity.HasOne(d => d.Supplier).WithMany(p => p.Products)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("FK_Products_Suppliers");
            });

            modelBuilder.Entity<Shippers>(entity =>
            {
                entity.Property(e => e.ShipperId).HasColumnName("ShipperID");
                entity.Property(e => e.CompanyName).HasMaxLength(40);
                entity.Property(e => e.Phone).HasMaxLength(24);
            });

            modelBuilder.Entity<Supliers>(entity =>
            {
                entity.HasIndex(e => e.CompanyName, "CompanyName");

                entity.HasIndex(e => e.PostalCode, "PostalCode");

                entity.Property(e => e.SupplierId).HasColumnName("SupplierID");
                entity.Property(e => e.Address).HasMaxLength(60);
                entity.Property(e => e.City).HasMaxLength(15);
                entity.Property(e => e.CompanyName).HasMaxLength(40);
                entity.Property(e => e.ContactName).HasMaxLength(30);
                entity.Property(e => e.ContactTitle).HasMaxLength(30);
                entity.Property(e => e.Country).HasMaxLength(15);
                entity.Property(e => e.Fax).HasMaxLength(24);
                entity.Property(e => e.HomePage).HasColumnType("ntext");
                entity.Property(e => e.Phone).HasMaxLength(24);
                entity.Property(e => e.PostalCode).HasMaxLength(10);
                entity.Property(e => e.Region).HasMaxLength(15);
            });
        }
    }
}
