using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Domain.Data;

public partial class VezeetaDbContext : IdentityDbContext<ApplicationUser>
{
    public VezeetaDbContext(DbContextOptions<VezeetaDbContext> options) : base(options)
    {
    }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }
    public virtual DbSet<Discount> Discounts { get; set; }

    public virtual DbSet<Specialization> Specializations { get; set; }

    public virtual DbSet<Time> Times { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=Vezeeta;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {


        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.DoctorId).HasName("PK__Doctors__722485961CB15009");

            entity.HasIndex(e => e.EmailDoctor, "UQ__Doctors__3312FF80F1E878FB").IsUnique();

            entity.HasIndex(e => e.Phone, "UQ__Doctors__B43B145FAA4B98DE").IsUnique();

            entity.Property(e => e.DoctorId).HasColumnName("doctorID");
            entity.Property(e => e.EmailDoctor)
                .HasMaxLength(50)
                .HasColumnName("emailDoctor");
            entity.Property(e => e.FullName)
                .HasMaxLength(50)
                .HasColumnName("fullName");
            entity.Property(e => e.ImagePath)
                .HasMaxLength(50)
                .HasColumnName("imagePath");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .HasColumnName("phone");
            entity.Property(e => e.Specialization)
                .HasMaxLength(50)
                .HasColumnName("specialization");

            entity.HasOne(d => d.SpecializationNavigation).WithMany(p => p.Doctors)
                .HasForeignKey(d => d.Specialization)
                .HasConstraintName("FK__Doctors__special__2DB1C7EE");
        });


        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.PatientId).HasName("PK__Patients__A17005CC264C0535");

            entity.HasIndex(e => e.EmailPatient, "UQ__Patients__5D273FB79A120DF9").IsUnique();

            entity.Property(e => e.PatientId).HasColumnName("patientID");
            entity.Property(e => e.DateOfBirth).HasColumnName("dateOfBirth");
            entity.Property(e => e.EmailPatient)
                .HasMaxLength(50)
                .HasColumnName("emailPatient");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("firstName");
            entity.Property(e => e.Gender).HasColumnName("gender");
            entity.Property(e => e.ImagePath)
                .HasMaxLength(50)
                .HasColumnName("imagePath");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("lastName");
            entity.Property(e => e.PatientPhone)
                .HasMaxLength(50)
                .HasColumnName("patientPhone");
        });


        modelBuilder.Entity<Specialization>(entity =>
        {
            entity.HasKey(e => e.SpecializationName).HasName("PK__Speciali__E8C2CCDE07D62EF6");

            entity.Property(e => e.SpecializationName)
                .HasMaxLength(50)
                .HasColumnName("specializationName");
            entity.Property(e => e.CreatedBy).HasColumnName("createdBy");
        });


        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__Appointm__C6D03BED1A6DA134");

            entity.Property(e => e.BookingId).HasColumnName("bookingID");
            entity.Property(e => e.AppointmentDay).HasColumnName("appointmentDay");
            entity.Property(e => e.EmailDoctor)
                .HasMaxLength(50)
                .HasColumnName("emailDoctor");
            entity.Property(e => e.EmailPatient)
                .HasMaxLength(50)
                .HasColumnName("emailPatient");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .HasColumnName("phone");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.AppointmentDate).HasColumnName("appointmentDate");
            entity.Property(e => e.Specialization)
                .HasMaxLength(50)
                .HasColumnName("specialization");
            entity.Property(e => e.StatusAppointment)
                .HasDefaultValue(0)
                .HasColumnName("statusAppointment");
        });


        modelBuilder.Entity<Discount>(entity =>
        {
            entity.HasKey(e => e.DiscountCode).HasName("PK__Discount__3D87979B1AA57117");

            entity.Property(e => e.DiscountCode)
                .ValueGeneratedNever()
                .HasColumnName("discountCode");
            entity.Property(e => e.DiscountType).HasColumnName("discountType");
            entity.Property(e => e.Valid).HasColumnName("valid");
            entity.Property(e => e.Price).HasColumnName("price");
        });

        modelBuilder.Entity<Time>(entity =>
        {
            entity.HasKey(e => e.DiscountId).HasName("PK__Times__D2130A06851B1C6A");

            entity.Property(e => e.DiscountId)
                .ValueGeneratedNever()
                .HasColumnName("discountID");
            entity.Property(e => e.AppointmentTime).HasColumnName("appointmentTime");
            entity.Property(e => e.DiscountDoctorCode).HasColumnName("discountDoctorCode");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.TimesId).HasColumnName("timesID");

            entity.HasOne(d => d.DiscountDoctorCodeNavigation).WithMany(p => p.Times)
                .HasForeignKey(d => d.DiscountDoctorCode)
                .HasConstraintName("FK__Times__discountD__019E3B86");
        });

        base.OnModelCreating(modelBuilder);
    }

}
