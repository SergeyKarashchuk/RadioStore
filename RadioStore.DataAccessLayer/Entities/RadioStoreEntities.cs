namespace RadioStore.DataAccessLayer.Entities
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class RadioStoreEntities : DbContext
    {
        public RadioStoreEntities()
            : base("name=RadioStoreEntities")
        {
        }

        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<PriceCount> PriceCounts { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<CartItem> CartItems { get; set; }
        public virtual DbSet<PriceProduct> PriceProducts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cart>()
                .HasMany(e => e.CartItems)
                .WithRequired(e => e.Carts)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PriceCount>()
                .HasMany(e => e.PriceProduct)
                .WithRequired(e => e.PriceCount)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.CartItems)
                .WithRequired(e => e.Products)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.PriceProducts)
                .WithRequired(e => e.Products)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PriceProduct>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);
        }
    }
}
