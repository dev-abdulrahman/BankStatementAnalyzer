using BankStatementAnalyzer.Models;
using Action = BankStatementAnalyzer.Models.Action;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;

namespace BankStatementAnalyzer.Repository.Common
{
    public class BankStatementAnalyzerContext : DbContext
    {
        public BankStatementAnalyzerContext() : base("BankStatementAnalyzerContext")
        {
            //this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Role> Role { get; set; }

        public DbSet<Permission> Permission { get; set; }

        public DbSet<Action> Action { get; set; }

        public DbSet<User> User { get; set; }

        public DbSet<SystemSetting> SystemSettings { get; set; }

        public DbSet<File> Files { get; set; }

        public DbSet<Menu> Menus { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<UserCompanyMapping> UserCompanyMappings { get; set; }

        public DbSet<SupportAndFAQ> SupportAndFAQ { get; set; }

        public DbSet<AreaManager> AreaManager { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<City> City { get; set; }

        public DbSet<Complaint> Complaints { get; set; }

        public DbSet<Email> Emails { get; set; }

        public DbSet<Gallery> Galleries { get; set; }

        public DbSet<ImageCategory> ImageCategories { get; set; }

        public DbSet<PageManager> PageManagers { get; set; }

        public DbSet<UndeliveryReason> UndeliveryReasons { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Street> Streets { get; set; }

        public DbSet<SubCategory> SubCategory { get; set; }

        public DbSet<StyleClass> StyleClass { get; set; }

        public DbSet<StyleTrait> StyleTrait { get; set; }

        public DbSet<StyleTraitValue> StyleTraitValue { get; set; }

        public DbSet<StyleTraitValueProduct> StyleTraitValueProduct { get; set; }

        public DbSet<Product> Product { get; set; }

        public DbSet<UnitOfMeasure> UnitOfMeasure { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Store> Stores { get; set; }

        public DbSet<Coupon> Coupons { get; set; }

        public DbSet<TimeSlot> TimeSlots { get; set; }

        public DbSet<DeliveryBoy> DeliveryBoys { get; set; }

        public DbSet<PushNotification> PushNotifications { get; set; }

        public DbSet<Wallet> Wallets { get; set; }

        public DbSet<AppMessage> AppMessages { get; set; }

        public DbSet<CustomerRefferalCodeMapping> CustomerRefferalCodeMappings { get; set; }

        public DbSet<ReturnRequest> ReturnRequests { get; set; }

        public DbSet<OrderDelvieryBoyMapping> OrderDelvieryBoyMappings { get; set; }

        public DbSet<ReturnRequestDeliveryBoyMapping> ReturnRequestDeliveryBoyMapping { get; set; }

        public DbSet<OrderStatusMapping> OrderStatusMapping { get; set; }

        public DbSet<CustomerCreditMapping> CustomerCreditMapping { get; set; }

        public DbSet<CustomerCreditHistory> CustomerCreditHistory { get; set; }

        public DbSet<RateCard> RateCard { get; set; }

        public DbSet<ServiceType> ServiceType { get; set; }

        public DbSet<Cloths> Cloths { get; set; }

        public DbSet<Package> Package { get; set; }

        public DbSet<PackageCouponMapping> PackageCouponMapping { get; set; }

        public DbSet<Vendor> Vendor { get; set; }

        public DbSet<OrderVendorMapping> OrderVendorMapping { get; set; }

        public DbSet<Banners> Banners { get; set; }

        public DbSet<BannerImages> BannerImages { get; set; }

        public DbSet<CustomerLikes> CustomerLikes { get; set; }

        public DbSet<CustomerNotification> CustomerNotification { get; set; }

        public DbSet<Icons> Icons { get; set; }

        public DbSet<HomeScreenLayout> HomeScreenLayout { get; set; }

        public DbSet<Colors> Colors { get; set; }

        public DbSet<Classified> Classified { get; set; }

        public DbSet<ClassifiedCategory> ClassifiedCategory { get; set; }

        public DbSet<ClassifiedContacts> ClassifiedContacts { get; set; }

        public DbSet<ClassifiedImages> ClassifiedImages { get; set; }

        public DbSet<ClassifiedKeywords> ClassifiedKeywords { get; set; }

        public DbSet<ClassifiedRating> ClassifiedRating { get; set; }

        public DbSet<BestDeal> BestDeal { get; set; }

        public DbSet<Carousel> Carousel { get; set; }

        public DbSet<ClassifiedArticle> ClassifiedArticle { get; set; }

        public DbSet<ClassifiedArticleCategory> ClassifiedArticleCategory { get; set; }

        public DbSet<ClassifiedArticleImages> ClassifiedArticleImages { get; set; }

        public DbSet<ClassifiedArticleKeywords> ClassifiedArticleKeywords { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }
    }
}