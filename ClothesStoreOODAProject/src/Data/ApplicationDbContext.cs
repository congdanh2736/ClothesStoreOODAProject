using ClothesStoreOODAProject.src.Models;
using Microsoft.EntityFrameworkCore;

namespace ClothesStoreOODAProject.src.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<MembershipTier> MembershipTiers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<CollectionTech> CollectionTeches { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCollection> ProductCollections { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductVariant> ProductVariants { get; set; }

        public DbSet<Store> Stores { get; set; }
        public DbSet<StoreStock> StoreStocks { get; set; }
        public DbSet<StoreDailyStat> StoreDailyStats { get; set; }
        public DbSet<StoreItemStat> StoreItemStats { get; set; }

        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<Review> Reviews { get; set; }

        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<PaymentTransaction> PaymentTransactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // =========================================================
            // 1. COMPOSITE KEY: ProductCollection (bảng trung gian)
            // =========================================================
            modelBuilder.Entity<ProductCollection>()
                .HasKey(pc => new { pc.ProductId, pc.CollectionId });

            modelBuilder.Entity<ProductCollection>()
                .HasOne(pc => pc.Product)
                .WithMany(p => p.ProductCollections)
                .HasForeignKey(pc => pc.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProductCollection>()
                .HasOne(pc => pc.CollectionTech)
                .WithMany(c => c.ProductCollections)
                .HasForeignKey(pc => pc.CollectionId)
                .OnDelete(DeleteBehavior.Cascade);

            // =========================================================
            // 2. CATEGORY - tự tham chiếu (parent/child)
            //    Bắt buộc Restrict, nếu không SQL Server sẽ báo lỗi
            //    "may cause cycles or multiple cascade paths"
            // =========================================================
            modelBuilder.Entity<Category>()
                .HasOne(c => c.Parent)
                .WithMany(c => c.Children)
                .HasForeignKey(c => c.ParentId)
                .OnDelete(DeleteBehavior.Restrict);

            // =========================================================
            // 3. CUSTOMER -> MEMBERSHIP_TIER
            //    Không muốn xóa Tier kéo theo xóa Customer
            // =========================================================
            modelBuilder.Entity<Customer>()
                .HasOne(c => c.MembershipTier)
                .WithMany(t => t.Customers)
                .HasForeignKey(c => c.TierId)
                .OnDelete(DeleteBehavior.Restrict);

            // =========================================================
            // 4. ORDER -> CUSTOMER / ADDRESS / PROMOTION
            //    Order có 2 đường tới Customer (trực tiếp qua CustomerId,
            //    và gián tiếp qua Address -> Customer) => multiple cascade
            //    paths nếu để Cascade cả 2. Chỉ để Cascade 1 đường, còn
            //    lại Restrict.
            // =========================================================
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Address)
                .WithMany(a => a.Orders)
                .HasForeignKey(o => o.AddressId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Promotion)
                .WithMany(p => p.Orders)
                .HasForeignKey(o => o.PromotionId)
                .OnDelete(DeleteBehavior.Restrict);

            // =========================================================
            // 5. ORDER_ITEM -> ORDER (cascade) / PRODUCT_VARIANT (restrict)
            // =========================================================
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.ProductVariant)
                .WithMany(v => v.OrderItems)
                .HasForeignKey(oi => oi.VariantId)
                .OnDelete(DeleteBehavior.Restrict);

            // =========================================================
            // 6. PAYMENT_TRANSACTION -> ORDER (1-1)
            // =========================================================
            modelBuilder.Entity<PaymentTransaction>()
                .HasOne(pt => pt.Order)
                .WithOne(o => o.PaymentTransaction)
                .HasForeignKey<PaymentTransaction>(pt => pt.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // =========================================================
            // 7. CART -> CUSTOMER (1-1)
            // =========================================================
            modelBuilder.Entity<Cart>()
                .HasOne(c => c.Customer)
                .WithOne(cu => cu.Cart)
                .HasForeignKey<Cart>(c => c.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            // =========================================================
            // 8. CART_ITEM -> CART (cascade) / PRODUCT_VARIANT (restrict)
            // =========================================================
            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Cart)
                .WithMany(c => c.CartItems)
                .HasForeignKey(ci => ci.CartId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.ProductVariant)
                .WithMany(v => v.CartItems)
                .HasForeignKey(ci => ci.VariantId)
                .OnDelete(DeleteBehavior.Restrict);

            // =========================================================
            // 9. WISHLIST -> CUSTOMER (cascade) / PRODUCT (restrict)
            //    + ràng buộc: 1 customer không wishlist trùng 1 product
            // =========================================================
            modelBuilder.Entity<Wishlist>()
                .HasOne(w => w.Customer)
                .WithMany(c => c.Wishlists)
                .HasForeignKey(w => w.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Wishlist>()
                .HasOne(w => w.Product)
                .WithMany(p => p.Wishlists)
                .HasForeignKey(w => w.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Wishlist>()
                .HasIndex(w => new { w.CustomerId, w.ProductId })
                .IsUnique();

            // =========================================================
            // 10. REVIEW -> CUSTOMER (cascade) / PRODUCT (restrict)
            // =========================================================
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Customer)
                .WithMany(c => c.Reviews)
                .HasForeignKey(r => r.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Product)
                .WithMany(p => p.Reviews)
                .HasForeignKey(r => r.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            // =========================================================
            // 11. STORE_STOCK -> STORE (cascade) / PRODUCT_VARIANT (restrict)
            //     + ràng buộc: 1 store không có 2 dòng tồn kho cho cùng
            //     1 variant
            // =========================================================
            modelBuilder.Entity<StoreStock>()
                .HasOne(s => s.Store)
                .WithMany(st => st.StoreStocks)
                .HasForeignKey(s => s.StoreId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<StoreStock>()
                .HasOne(s => s.ProductVariant)
                .WithMany(v => v.StoreStocks)
                .HasForeignKey(s => s.VariantId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StoreStock>()
                .HasIndex(s => new { s.StoreId, s.VariantId })
                .IsUnique();

            // =========================================================
            // 12. STORE_ITEM_STAT -> STORE (cascade) / PRODUCT_VARIANT (restrict)
            // =========================================================
            modelBuilder.Entity<StoreItemStat>()
                .HasOne(s => s.Store)
                .WithMany(st => st.StoreItemStats)
                .HasForeignKey(s => s.StoreId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<StoreItemStat>()
                .HasOne(s => s.ProductVariant)
                .WithMany(v => v.StoreItemStats)
                .HasForeignKey(s => s.VariantId)
                .OnDelete(DeleteBehavior.Restrict);

            // =========================================================
            // 13. PRODUCT -> CATEGORY (restrict, tránh xóa nhầm hàng loạt SP)
            // =========================================================
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // =========================================================
            // 14. UNIQUE CONSTRAINT: Promotion.Code
            // =========================================================
            modelBuilder.Entity<Promotion>()
                .HasIndex(p => p.Code)
                .IsUnique();

            // =========================================================
            // 15. DECIMAL PRECISION (tránh warning + đảm bảo đúng tiền tệ)
            // =========================================================
            modelBuilder.Entity<ProductVariant>()
                .Property(v => v.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Promotion>()
                .Property(p => p.DiscountValue)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Order>()
                .Property(o => o.TotalAmount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<OrderItem>()
                .Property(oi => oi.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<StoreDailyStat>()
                .Property(s => s.TotalRevenue)
                .HasPrecision(18, 2);

            modelBuilder.Entity<StoreItemStat>()
                .Property(s => s.TotalRevenue)
                .HasPrecision(18, 2);
        }
    }
}
