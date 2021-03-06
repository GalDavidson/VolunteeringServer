using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace VolunteeringServerBL.Models
{
    public partial class VolunteeringDBContext : DbContext
    {
        public VolunteeringDBContext()
        {
        }

        public VolunteeringDBContext(DbContextOptions<VolunteeringDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AppAdmin> AppAdmins { get; set; }
        public virtual DbSet<Area> Areas { get; set; }
        public virtual DbSet<Association> Associations { get; set; }
        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<BranchesOfAssociation> BranchesOfAssociations { get; set; }
        public virtual DbSet<DailyEvent> DailyEvents { get; set; }
        public virtual DbSet<Gender> Genders { get; set; }
        public virtual DbSet<OccupationalArea> OccupationalAreas { get; set; }
        public virtual DbSet<OccupationalAreasOfAssociation> OccupationalAreasOfAssociations { get; set; }
        public virtual DbSet<OccupationalAreasOfEvent> OccupationalAreasOfEvents { get; set; }
        public virtual DbSet<Rank> Ranks { get; set; }
        public virtual DbSet<Volunteer> Volunteers { get; set; }
        public virtual DbSet<VolunteersInEvent> VolunteersInEvents { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost\\sqlexpress;Database=VolunteeringDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Hebrew_CI_AS");

            modelBuilder.Entity<AppAdmin>(entity =>
            {
                entity.HasKey(e => e.AdminId)
                    .HasName("PK__AppAdmin__719FE4E8B9AA749F");

                entity.ToTable("AppAdmin");

                entity.HasIndex(e => e.Email, "UQ__AppAdmin__A9D1053479A08822")
                    .IsUnique();

                entity.HasIndex(e => e.UserName, "UQ__AppAdmin__C9F28456AE4A88EA")
                    .IsUnique();

                entity.Property(e => e.AdminId).HasColumnName("AdminID");

                entity.Property(e => e.AdminName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Pass)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Area>(entity =>
            {
                entity.Property(e => e.AreaId).HasColumnName("AreaID");

                entity.Property(e => e.AreaName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Association>(entity =>
            {
                entity.HasIndex(e => e.Email, "UQ__Associat__A9D10534DA55BDB1")
                    .IsUnique();

                entity.HasIndex(e => e.UserName, "UQ__Associat__C9F28456D09E86A7")
                    .IsUnique();

                entity.Property(e => e.AssociationId).HasColumnName("AssociationID");

                entity.Property(e => e.ActionDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.InformationAbout)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Pass)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.PhoneNum)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Branch>(entity =>
            {
                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.BranchLocation)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<BranchesOfAssociation>(entity =>
            {
                entity.HasKey(e => new { e.AssociationId, e.BranchId })
                    .HasName("PK_BranchesOfAss");

                entity.ToTable("BranchesOfAssociation");

                entity.Property(e => e.AssociationId).HasColumnName("AssociationID");

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.HasOne(d => d.Association)
                    .WithMany(p => p.BranchesOfAssociations)
                    .HasForeignKey(d => d.AssociationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BranchesO__Assoc__4F7CD00D");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.BranchesOfAssociations)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BranchesO__Branc__5070F446");
            });

            modelBuilder.Entity<DailyEvent>(entity =>
            {
                entity.HasKey(e => e.EventId)
                    .HasName("PK__DailyEve__7944C870C4B2D2B7");

                entity.Property(e => e.EventId).HasColumnName("EventID");

                entity.Property(e => e.ActionDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AreaId).HasColumnName("AreaID");

                entity.Property(e => e.AssociationId).HasColumnName("AssociationID");

                entity.Property(e => e.Caption)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.EventLocation)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.EventName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.StartTime).HasColumnType("datetime");

                entity.HasOne(d => d.Area)
                    .WithMany(p => p.DailyEvents)
                    .HasForeignKey(d => d.AreaId)
                    .HasConstraintName("FK__DailyEven__AreaI__571DF1D5");

                entity.HasOne(d => d.Association)
                    .WithMany(p => p.DailyEvents)
                    .HasForeignKey(d => d.AssociationId)
                    .HasConstraintName("FK__DailyEven__Assoc__5535A963");
            });

            modelBuilder.Entity<Gender>(entity =>
            {
                entity.ToTable("Gender");

                entity.Property(e => e.GenderId).HasColumnName("GenderID");

                entity.Property(e => e.GenderType)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<OccupationalArea>(entity =>
            {
                entity.Property(e => e.OccupationalAreaId).HasColumnName("OccupationalAreaID");

                entity.Property(e => e.OccupationName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<OccupationalAreasOfAssociation>(entity =>
            {
                entity.HasKey(e => new { e.AssociationId, e.OccupationalAreaId })
                    .HasName("PK_OcAreasOfAssociation");

                entity.ToTable("OccupationalAreasOfAssociation");

                entity.Property(e => e.AssociationId).HasColumnName("AssociationID");

                entity.Property(e => e.OccupationalAreaId).HasColumnName("OccupationalAreaID");

                entity.HasOne(d => d.Association)
                    .WithMany(p => p.OccupationalAreasOfAssociations)
                    .HasForeignKey(d => d.AssociationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Occupatio__Assoc__49C3F6B7");

                entity.HasOne(d => d.OccupationalArea)
                    .WithMany(p => p.OccupationalAreasOfAssociations)
                    .HasForeignKey(d => d.OccupationalAreaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Occupatio__Occup__4AB81AF0");
            });

            modelBuilder.Entity<OccupationalAreasOfEvent>(entity =>
            {
                entity.HasKey(e => new { e.EventId, e.OccupationalAreaId })
                    .HasName("PK_OccuAreasEvents");

                entity.Property(e => e.EventId).HasColumnName("EventID");

                entity.Property(e => e.OccupationalAreaId).HasColumnName("OccupationalAreaID");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.OccupationalAreasOfEvents)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Occupatio__Event__59FA5E80");

                entity.HasOne(d => d.OccupationalArea)
                    .WithMany(p => p.OccupationalAreasOfEvents)
                    .HasForeignKey(d => d.OccupationalAreaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Occupatio__Occup__5AEE82B9");
            });

            modelBuilder.Entity<Rank>(entity =>
            {
                entity.Property(e => e.RankId).HasColumnName("RankID");

                entity.Property(e => e.RankName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Volunteer>(entity =>
            {
                entity.HasIndex(e => e.Email, "UQ__Voluntee__A9D10534DD7D0DDC")
                    .IsUnique();

                entity.HasIndex(e => e.UserName, "UQ__Voluntee__C9F284560F111F92")
                    .IsUnique();

                entity.Property(e => e.VolunteerId).HasColumnName("VolunteerID");

                entity.Property(e => e.ActionDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AvgRating).HasDefaultValueSql("('1')");

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.FName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("fName");

                entity.Property(e => e.GenderId).HasColumnName("GenderID");

                entity.Property(e => e.LName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("lName");

                entity.Property(e => e.Pass)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.Gender)
                    .WithMany(p => p.Volunteers)
                    .HasForeignKey(d => d.GenderId)
                    .HasConstraintName("FK__Volunteer__Gende__3A81B327");
            });

            modelBuilder.Entity<VolunteersInEvent>(entity =>
            {
                entity.HasKey(e => new { e.EventId, e.VolunteerId })
                    .HasName("PK_VolInEvents");

                entity.Property(e => e.EventId).HasColumnName("EventID");

                entity.Property(e => e.VolunteerId).HasColumnName("VolunteerID");

                entity.Property(e => e.ActionDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.WrittenRating).HasMaxLength(255);

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.VolunteersInEvents)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Volunteer__Event__5DCAEF64");

                entity.HasOne(d => d.Volunteer)
                    .WithMany(p => p.VolunteersInEvents)
                    .HasForeignKey(d => d.VolunteerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Volunteer__Volun__5EBF139D");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
