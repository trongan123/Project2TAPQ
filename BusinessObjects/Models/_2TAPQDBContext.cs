using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BusinessObjects.Models
{
    public partial class _2TAPQDBContext : DbContext
    {
        public _2TAPQDBContext()
        {
        }

        public _2TAPQDBContext(DbContextOptions<_2TAPQDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<CooperativeRoom> CooperativeRooms { get; set; } = null!;
        public virtual DbSet<DetailReceiptsPayment> DetailReceiptsPayments { get; set; } = null!;
        public virtual DbSet<District> Districts { get; set; } = null!;
        public virtual DbSet<FishCategory> FishCategories { get; set; } = null!;
        public virtual DbSet<HistoryStoreHouse> HistoryStoreHouses { get; set; } = null!;
        public virtual DbSet<ItemCategory> ItemCategories { get; set; } = null!;
        public virtual DbSet<ItemStoreHouse> ItemStoreHouses { get; set; } = null!;
        public virtual DbSet<Member> Members { get; set; } = null!;
        public virtual DbSet<Notify> Notifies { get; set; } = null!;
        public virtual DbSet<Pond> Ponds { get; set; } = null!;
        public virtual DbSet<PondDiary> PondDiaries { get; set; } = null!;
        public virtual DbSet<Province> Provinces { get; set; } = null!;
        public virtual DbSet<QuantityHouse> QuantityHouses { get; set; } = null!;
        public virtual DbSet<ReceiptsPayment> ReceiptsPayments { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<RoleStaff> RoleStaffs { get; set; } = null!;
        public virtual DbSet<StoreHouse> StoreHouses { get; set; } = null!;
        public virtual DbSet<Ward> Wards { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-O3NQBKB;Database=2TAPQDB;uid=sa;pwd=123456;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.IdAcc)
                    .HasName("PK__Account__227DBBF295A81F8A");

                entity.ToTable("Account");

                entity.Property(e => e.IdAcc)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ID_acc");

                entity.Property(e => e.Address).HasMaxLength(300);

                entity.Property(e => e.Birthday).HasColumnType("datetime");

                entity.Property(e => e.DateJoin)
                    .HasColumnType("datetime")
                    .HasColumnName("Date_join");

                entity.Property(e => e.Email)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Fullname).HasMaxLength(100);

                entity.Property(e => e.IdRoleStaff)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ID_role_Staff");

                entity.Property(e => e.IdWard)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ID_ward");

                entity.Property(e => e.Image).HasColumnType("text");

                entity.Property(e => e.Password)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdRoleStaffNavigation)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.IdRoleStaff)
                    .HasConstraintName("FK_Account_Role_Staff");

                entity.HasOne(d => d.IdWardNavigation)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.IdWard)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Account_Ward");
            });

            modelBuilder.Entity<CooperativeRoom>(entity =>
            {
                entity.HasKey(e => e.IdRoom)
                    .HasName("PK__Cooperat__45DC5D2BC83D1EC2");

                entity.ToTable("Cooperative_Room");

                entity.Property(e => e.IdRoom)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ID_room");

                entity.Property(e => e.IdCoo)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ID_coo");

                entity.Property(e => e.JoinCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Join_code");

                entity.Property(e => e.PondArea).HasColumnName("Pond_area");

                entity.HasOne(d => d.IdCooNavigation)
                    .WithMany(p => p.CooperativeRooms)
                    .HasForeignKey(d => d.IdCoo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cooperative_Room_Account");
            });

            modelBuilder.Entity<DetailReceiptsPayment>(entity =>
            {
                entity.HasKey(e => e.IdDetailReceiptsPayments)
                    .HasName("PK__Detail_R__BE30AFEF0A7F8035");

                entity.ToTable("Detail_Receipts_payments");

                entity.Property(e => e.IdDetailReceiptsPayments)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ID_detail_receipts_payments");

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.IdInvoice)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ID_Invoice");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.NameStaff).HasMaxLength(80);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.IdInvoiceNavigation)
                    .WithMany(p => p.DetailReceiptsPayments)
                    .HasForeignKey(d => d.IdInvoice)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Detail_Receipts_payments_Receipts_payments");
            });

            modelBuilder.Entity<District>(entity =>
            {
                entity.HasKey(e => e.IdDistrict)
                    .HasName("PK__District__CE4EE172FB32D706");

                entity.ToTable("District");

                entity.Property(e => e.IdDistrict)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ID_district");

                entity.Property(e => e.IdProvince)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ID_province");

                entity.Property(e => e.Name).HasMaxLength(80);

                entity.Property(e => e.Type)
                    .HasMaxLength(80)
                    .HasColumnName("type");

                entity.HasOne(d => d.IdProvinceNavigation)
                    .WithMany(p => p.Districts)
                    .HasForeignKey(d => d.IdProvince)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_District_Province");
            });

            modelBuilder.Entity<FishCategory>(entity =>
            {
                entity.HasKey(e => e.IdFcategory)
                    .HasName("PK__Fish_cat__43F2C1F3D245B94F");

                entity.ToTable("Fish_category");

                entity.Property(e => e.IdFcategory)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ID_fcategory");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(100)
                    .HasColumnName("Category_name");

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.HarvestTime).HasColumnName("Harvest_time");

                entity.Property(e => e.Image).HasColumnType("text");

                entity.Property(e => e.Ph).HasColumnName("PH");

                entity.Property(e => e.WaterLevel).HasColumnName("Water_level");
            });

            modelBuilder.Entity<HistoryStoreHouse>(entity =>
            {
                entity.HasKey(e => e.IdHistoryStoreHouse)
                    .HasName("PK__History___E330FF85923AC8FB");

                entity.ToTable("History_store_house");

                entity.Property(e => e.IdHistoryStoreHouse)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ID_history_store_house");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.IdItemCategory)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ID_item_category");

                entity.Property(e => e.IdSHouse)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ID_s_house");

                entity.Property(e => e.IdStaff)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ID_staff");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Note).HasMaxLength(200);

                entity.HasOne(d => d.IdItemCategoryNavigation)
                    .WithMany(p => p.HistoryStoreHouses)
                    .HasForeignKey(d => d.IdItemCategory)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_History_store_house_Item_category");

                entity.HasOne(d => d.IdSHouseNavigation)
                    .WithMany(p => p.HistoryStoreHouses)
                    .HasForeignKey(d => d.IdSHouse)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_History_store_house_Store_house");

                entity.HasOne(d => d.IdStaffNavigation)
                    .WithMany(p => p.HistoryStoreHouses)
                    .HasForeignKey(d => d.IdStaff)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_History_store_house_Account");
            });

            modelBuilder.Entity<ItemCategory>(entity =>
            {
                entity.HasKey(e => e.IdItemCategory)
                    .HasName("PK__Item_cat__E6958D311EBCE630");

                entity.ToTable("Item_category");

                entity.Property(e => e.IdItemCategory)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ID_item_category");

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<ItemStoreHouse>(entity =>
            {
                entity.HasKey(e => e.IdItemStoreHouse)
                    .HasName("PK__Item_sto__C85D59D317C64140");

                entity.ToTable("Item_store_house");

                entity.Property(e => e.IdItemStoreHouse)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ID_item_store_house");

                entity.Property(e => e.IdItemCategory)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ID_item_category");

                entity.Property(e => e.IdSHouse)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ID_s_house");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Note).HasMaxLength(200);

                entity.HasOne(d => d.IdItemCategoryNavigation)
                    .WithMany(p => p.ItemStoreHouses)
                    .HasForeignKey(d => d.IdItemCategory)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Item_store_house_Item_category");

                entity.HasOne(d => d.IdSHouseNavigation)
                    .WithMany(p => p.ItemStoreHouses)
                    .HasForeignKey(d => d.IdSHouse)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Item_store_house_Store_house");
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.HasKey(e => e.IdMember)
                    .HasName("PK__Member__F068FD4DABE81337");

                entity.ToTable("Member");

                entity.Property(e => e.IdMember)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ID_member");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasColumnName("date");

                entity.Property(e => e.IdRoom)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ID_room");

                entity.Property(e => e.IdUser)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ID_user");

                entity.HasOne(d => d.IdRoomNavigation)
                    .WithMany(p => p.Members)
                    .HasForeignKey(d => d.IdRoom)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Member_Cooperative_Room");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Members)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Member_Account");
            });

            modelBuilder.Entity<Notify>(entity =>
            {
                entity.HasKey(e => e.IdNotify)
                    .HasName("PK__Notify__ED3D8CB25910990F");

                entity.ToTable("Notify");

                entity.Property(e => e.IdNotify)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ID_Notify");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.IdAcc)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ID_acc");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdAccNavigation)
                    .WithMany(p => p.Notifies)
                    .HasForeignKey(d => d.IdAcc)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Notify_Account");
            });

            modelBuilder.Entity<Pond>(entity =>
            {
                entity.HasKey(e => e.IdPond)
                    .HasName("PK__Pond__961CF96197BF6B95");

                entity.ToTable("Pond");

                entity.Property(e => e.IdPond)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ID_pond");

                entity.Property(e => e.EndDay)
                    .HasColumnType("datetime")
                    .HasColumnName("End_day");

                entity.Property(e => e.IdAcc)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ID_acc");

                entity.Property(e => e.IdFcategory)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ID_fcategory");

                entity.Property(e => e.Image).HasColumnType("text");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.PondArea).HasColumnName("Pond_area");

                entity.Property(e => e.QuanlityOfEnd).HasColumnName("Quanlity_of_end");

                entity.Property(e => e.QuantityOfFingerlings).HasColumnName("Quantity_of_fingerlings");

                entity.Property(e => e.Session)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.StartDay)
                    .HasColumnType("datetime")
                    .HasColumnName("Start_day");

                entity.HasOne(d => d.IdAccNavigation)
                    .WithMany(p => p.Ponds)
                    .HasForeignKey(d => d.IdAcc)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pond_Account");

                entity.HasOne(d => d.IdFcategoryNavigation)
                    .WithMany(p => p.Ponds)
                    .HasForeignKey(d => d.IdFcategory)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pond_Fish_category");
            });

            modelBuilder.Entity<PondDiary>(entity =>
            {
                entity.HasKey(e => e.IdDiary)
                    .HasName("PK__Pond_dia__F8DD69931D4165DE");

                entity.ToTable("Pond_diary");

                entity.Property(e => e.IdDiary)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ID_diary");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.FishStatus)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Fish_status");

                entity.Property(e => e.IdPond)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ID_pond");

                entity.Property(e => e.Ph).HasColumnName("PH");

                entity.Property(e => e.WaterLevel).HasColumnName("Water_level");

                entity.HasOne(d => d.IdPondNavigation)
                    .WithMany(p => p.PondDiaries)
                    .HasForeignKey(d => d.IdPond)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pond_diary_Pond");
            });

            modelBuilder.Entity<Province>(entity =>
            {
                entity.HasKey(e => e.IdProvince)
                    .HasName("PK__Province__95EC1955B890F170");

                entity.ToTable("Province");

                entity.Property(e => e.IdProvince)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ID_province");

                entity.Property(e => e.Name).HasMaxLength(80);

                entity.Property(e => e.Type)
                    .HasMaxLength(80)
                    .HasColumnName("type");
            });

            modelBuilder.Entity<QuantityHouse>(entity =>
            {
                entity.HasKey(e => e.IdQuantity)
                    .HasName("PK__Quantity__7AB24AC37DBEFAFE");

                entity.ToTable("Quantity_House");

                entity.Property(e => e.IdQuantity)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ID_quantity");

                entity.Property(e => e.AddedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Added_date");

                entity.Property(e => e.IdAcc)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ID_acc");

                entity.HasOne(d => d.IdAccNavigation)
                    .WithMany(p => p.QuantityHouses)
                    .HasForeignKey(d => d.IdAcc)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Quantity_House_Account");
            });

            modelBuilder.Entity<ReceiptsPayment>(entity =>
            {
                entity.HasKey(e => e.IdInvoice)
                    .HasName("PK__Receipts__0540CA60532882A8");

                entity.ToTable("Receipts_payments");

                entity.Property(e => e.IdInvoice)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ID_Invoice");

                entity.Property(e => e.AddedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Added_date");

                entity.Property(e => e.IdUser)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ID_user");

                entity.Property(e => e.Total).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.ReceiptsPayments)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Receipts_payments_Account");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.IdRole)
                    .HasName("PK__Role__45DFFBDB2F649330");

                entity.ToTable("Role");

                entity.Property(e => e.IdRole)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ID_role");

                entity.Property(e => e.Type)
                    .HasMaxLength(100)
                    .HasColumnName("type");
            });

            modelBuilder.Entity<RoleStaff>(entity =>
            {
                entity.HasKey(e => e.IdRoleStaff)
                    .HasName("PK__Role_Sta__890C70A570590612");

                entity.ToTable("Role_Staff");

                entity.Property(e => e.IdRoleStaff)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ID_role_Staff");

                entity.Property(e => e.IdAcc)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ID_acc");

                entity.Property(e => e.IdRole)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ID_role");

                entity.Property(e => e.Salary)
                    .HasColumnType("money")
                    .HasColumnName("salary");

                entity.HasOne(d => d.IdRoleNavigation)
                    .WithMany(p => p.RoleStaffs)
                    .HasForeignKey(d => d.IdRole)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Role_Staff_Role");
            });

            modelBuilder.Entity<StoreHouse>(entity =>
            {
                entity.HasKey(e => e.IdSHouse)
                    .HasName("PK__Store_ho__EECD611FD9FE9D6F");

                entity.ToTable("Store_house");

                entity.Property(e => e.IdSHouse)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ID_s_house");

                entity.Property(e => e.IdUser)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ID_user");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.StoreHouses)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Store_house_Account");
            });

            modelBuilder.Entity<Ward>(entity =>
            {
                entity.HasKey(e => e.IdWard)
                    .HasName("PK__Ward__F1F989FF84A63602");

                entity.ToTable("Ward");

                entity.Property(e => e.IdWard)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ID_ward");

                entity.Property(e => e.IdDistrict)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ID_district");

                entity.Property(e => e.Name).HasMaxLength(80);

                entity.Property(e => e.Type)
                    .HasMaxLength(80)
                    .HasColumnName("type");

                entity.HasOne(d => d.IdDistrictNavigation)
                    .WithMany(p => p.Wards)
                    .HasForeignKey(d => d.IdDistrict)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ward_District");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
