using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Smart300.Models;

namespace Smart300.Context;

public partial class SmartServiceContext : DbContext
{
    public SmartServiceContext()
    {
    }

    public SmartServiceContext(DbContextOptions<SmartServiceContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AuditLog> AuditLogs { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Equipment> Equipment { get; set; }

    public virtual DbSet<ObjectType> ObjectTypes { get; set; }

    public virtual DbSet<Priority> Priorities { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<TicketStage> TicketStages { get; set; }

    public virtual DbSet<TicketStatus> TicketStatuses { get; set; }

    public virtual DbSet<TicketStatusHistory> TicketStatusHistories { get; set; }

    public virtual DbSet<TicketType> TicketTypes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=26.192.39.184;Port=5434;Database=SmartService;Username=postgres;Password=123");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuditLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("audit_log_pkey");

            entity.ToTable("audit_log");

            entity.HasIndex(e => e.ChangedBy, "idx_audit_changed_by");

            entity.HasIndex(e => new { e.EntityType, e.EntityId }, "idx_audit_entity");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Action)
                .HasMaxLength(50)
                .HasColumnName("action");
            entity.Property(e => e.ChangedBy).HasColumnName("changed_by");
            entity.Property(e => e.Comment).HasColumnName("comment");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.EntityId).HasColumnName("entity_id");
            entity.Property(e => e.EntityType)
                .HasMaxLength(50)
                .HasColumnName("entity_type");
            entity.Property(e => e.NewData)
                .HasColumnType("jsonb")
                .HasColumnName("new_data");
            entity.Property(e => e.OldData)
                .HasColumnType("jsonb")
                .HasColumnName("old_data");

            entity.HasOne(d => d.ChangedByNavigation).WithMany(p => p.AuditLogs)
                .HasForeignKey(d => d.ChangedBy)
                .HasConstraintName("audit_log_changed_by_fkey");
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("companies_pkey");

            entity.ToTable("companies");

            entity.HasIndex(e => e.IsActive, "idx_companies_active");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Inn)
                .HasMaxLength(20)
                .HasColumnName("inn");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("customers_pkey");

            entity.ToTable("customers");

            entity.HasIndex(e => e.IsActive, "idx_customers_active");

            entity.HasIndex(e => e.UserId, "idx_customers_user");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Inn)
                .HasMaxLength(20)
                .HasColumnName("inn");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .HasColumnName("phone");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Customers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("customers_user_id_fkey");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("employees_pkey");

            entity.ToTable("employees");

            entity.HasIndex(e => e.IsActive, "idx_employees_active");

            entity.HasIndex(e => e.UserId, "idx_employees_user");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .HasColumnName("phone");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Employees)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("employees_user_id_fkey");
        });

        modelBuilder.Entity<Equipment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("equipment_pkey");

            entity.ToTable("equipment");

            entity.HasIndex(e => e.IsActive, "idx_equipment_active");

            entity.HasIndex(e => e.CompanyId, "idx_equipment_company");

            entity.HasIndex(e => e.ObjectTypeId, "idx_equipment_object_type");

            entity.HasIndex(e => e.WarrantyUntil, "idx_equipment_warranty");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.CommissioningDate).HasColumnName("commissioning_date");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(500)
                .HasColumnName("image_url");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.ObjectTypeId).HasColumnName("object_type_id");
            entity.Property(e => e.SerialNumber)
                .HasMaxLength(100)
                .HasColumnName("serial_number");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.WarrantyUntil).HasColumnName("warranty_until");

            entity.HasOne(d => d.Company).WithMany(p => p.Equipment)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("equipment_company_id_fkey");

            entity.HasOne(d => d.ObjectType).WithMany(p => p.Equipment)
                .HasForeignKey(d => d.ObjectTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("equipment_object_type_id_fkey");
        });

        modelBuilder.Entity<ObjectType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("object_types_pkey");

            entity.ToTable("object_types");

            entity.HasIndex(e => e.Code, "object_types_code_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .HasColumnName("code");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Priority>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("priorities_pkey");

            entity.ToTable("priorities");

            entity.HasIndex(e => e.Code, "priorities_code_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .HasColumnName("code");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("roles_pkey");

            entity.ToTable("roles");

            entity.HasIndex(e => e.Code, "roles_code_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .HasColumnName("code");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tickets_pkey");

            entity.ToTable("tickets");

            entity.HasIndex(e => e.AssigneeId, "idx_tickets_assignee");

            entity.HasIndex(e => e.CompanyId, "idx_tickets_company");

            entity.HasIndex(e => e.CreatedAt, "idx_tickets_created_at");

            entity.HasIndex(e => e.CreatedBy, "idx_tickets_created_by");

            entity.HasIndex(e => e.CustomerId, "idx_tickets_customer");

            entity.HasIndex(e => e.Deadline, "idx_tickets_deadline");

            entity.HasIndex(e => e.DetectedAt, "idx_tickets_detected_at");

            entity.HasIndex(e => e.EquipmentId, "idx_tickets_equipment");

            entity.HasIndex(e => e.IsDeleted, "idx_tickets_is_deleted").HasFilter("(is_deleted = false)");

            entity.HasIndex(e => e.PriorityId, "idx_tickets_priority");

            entity.HasIndex(e => e.StatusId, "idx_tickets_status");

            entity.HasIndex(e => e.TypeId, "idx_tickets_type");

            entity.HasIndex(e => e.TicketNumber, "tickets_ticket_number_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AssigneeId).HasColumnName("assignee_id");
            entity.Property(e => e.CancellationReason).HasColumnName("cancellation_reason");
            entity.Property(e => e.ClosedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("closed_at");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.Deadline)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("deadline");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.DetectedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("detected_at");
            entity.Property(e => e.EquipmentId).HasColumnName("equipment_id");
            entity.Property(e => e.EstimatedCost)
                .HasPrecision(12, 2)
                .HasColumnName("estimated_cost");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.PriorityId).HasColumnName("priority_id");
            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.Subject)
                .HasMaxLength(255)
                .HasColumnName("subject");
            entity.Property(e => e.TicketNumber)
                .HasMaxLength(20)
                .HasColumnName("ticket_number");
            entity.Property(e => e.TypeId).HasColumnName("type_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Assignee).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.AssigneeId)
                .HasConstraintName("tickets_assignee_id_fkey");

            entity.HasOne(d => d.Company).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tickets_company_id_fkey");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tickets_created_by_fkey");

            entity.HasOne(d => d.Customer).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tickets_customer_id_fkey");

            entity.HasOne(d => d.Equipment).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.EquipmentId)
                .HasConstraintName("tickets_equipment_id_fkey");

            entity.HasOne(d => d.Priority).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.PriorityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tickets_priority_id_fkey");

            entity.HasOne(d => d.Status).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tickets_status_id_fkey");

            entity.HasOne(d => d.Type).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tickets_type_id_fkey");
        });

        modelBuilder.Entity<TicketStage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ticket_stages_pkey");

            entity.ToTable("ticket_stages");

            entity.HasIndex(e => e.Code, "ticket_stages_code_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .HasColumnName("code");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.SortOrder).HasColumnName("sort_order");
        });

        modelBuilder.Entity<TicketStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ticket_statuses_pkey");

            entity.ToTable("ticket_statuses");

            entity.HasIndex(e => e.StageId, "idx_ticket_statuses_stage");

            entity.HasIndex(e => e.Code, "ticket_statuses_code_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .HasColumnName("code");
            entity.Property(e => e.IsCancelled)
                .HasDefaultValue(false)
                .HasColumnName("is_cancelled");
            entity.Property(e => e.IsFinal)
                .HasDefaultValue(false)
                .HasColumnName("is_final");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.StageId).HasColumnName("stage_id");

            entity.HasOne(d => d.Stage).WithMany(p => p.TicketStatuses)
                .HasForeignKey(d => d.StageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ticket_statuses_stage_id_fkey");
        });

        modelBuilder.Entity<TicketStatusHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ticket_status_history_pkey");

            entity.ToTable("ticket_status_history");

            entity.HasIndex(e => e.TicketId, "idx_ticket_hist_ticket");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ChangedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("changed_at");
            entity.Property(e => e.ChangedBy).HasColumnName("changed_by");
            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.TicketId).HasColumnName("ticket_id");

            entity.HasOne(d => d.ChangedByNavigation).WithMany(p => p.TicketStatusHistories)
                .HasForeignKey(d => d.ChangedBy)
                .HasConstraintName("ticket_status_history_changed_by_fkey");

            entity.HasOne(d => d.Status).WithMany(p => p.TicketStatusHistories)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ticket_status_history_status_id_fkey");

            entity.HasOne(d => d.Ticket).WithMany(p => p.TicketStatusHistories)
                .HasForeignKey(d => d.TicketId)
                .HasConstraintName("ticket_status_history_ticket_id_fkey");
        });

        modelBuilder.Entity<TicketType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ticket_types_pkey");

            entity.ToTable("ticket_types");

            entity.HasIndex(e => e.Code, "ticket_types_code_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .HasColumnName("code");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.IsActive, "idx_users_active");

            entity.HasIndex(e => e.Email, "idx_users_email");

            entity.HasIndex(e => e.RoleId, "idx_users_role");

            entity.HasIndex(e => e.Email, "users_email_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasMaxLength(255)
                .HasColumnName("full_name");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .HasColumnName("phone");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("users_role_id_fkey");
        });
        modelBuilder.HasSequence("ticket_number_seq");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
