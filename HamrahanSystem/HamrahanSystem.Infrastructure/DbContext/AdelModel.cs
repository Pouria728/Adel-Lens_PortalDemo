

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using HamrahanSystem.Application.UseCaseInterface;
using HamrahanSystem.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HamrahanSystem.Infrastructure.Repository
{

    public partial class AdelModel : DbContext
    {
		private static readonly HashSet<string> SensitiveProperties = new(StringComparer.OrdinalIgnoreCase)
		{
			"Password",
			"PasswordHash",
			"PasswordSalt",
			"Token",
			"AccessToken",
			"RefreshToken",
			"Secret",
			"ApiKey"
		};

		private readonly ICurrentUserService? _currentUserService;
		private readonly IAuditContext? _auditContext;

        public AdelModel() :
            base()
        {
            OnCreated();
        }

        public AdelModel(DbContextOptions<AdelModel> options) :
            base(options)
        {
            OnCreated();
        }

		public AdelModel(DbContextOptions<AdelModel> options, ICurrentUserService currentUserService, IAuditContext auditContext) :
			base(options)
		{
			_currentUserService = currentUserService;
			_auditContext = auditContext;
			OnCreated();
		}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured ||
                (!optionsBuilder.Options.Extensions.OfType<RelationalOptionsExtension>().Any(ext => !string.IsNullOrEmpty(ext.ConnectionString) || ext.Connection != null) &&
                 !optionsBuilder.Options.Extensions.Any(ext => !(ext is RelationalOptionsExtension) && !(ext is CoreOptionsExtension))))
            {
                throw new InvalidOperationException("AdelModel requires an explicit SQL Server connection string.");
            }
            CustomizeConfiguration(ref optionsBuilder);
            base.OnConfiguring(optionsBuilder);
        }

        partial void CustomizeConfiguration(ref DbContextOptionsBuilder optionsBuilder);

		public virtual DbSet<Permission> Permission
		{
			get;
			set;
		}
		public virtual DbSet<RolePermission> RolePermissions
        {
            get;
            set;
        }

        public virtual DbSet<Role> Roles
        {
            get;
            set;
        }

        public virtual DbSet<RoleToRole> RoleToRoles
        {
            get;
            set;
        }

        public virtual DbSet<TblClrDefineObject> TblClrDefineObjects
        {
            get;
            set;
        }

        public virtual DbSet<TblLnsBrand> TblLnsBrands
        {
            get;
            set;
        }

        public virtual DbSet<TblLnsBrandCoating> TblLnsBrandCoatings
        {
            get;
            set;
        }

        public virtual DbSet<TblLnsBrandDesignType> TblLnsBrandDesignTypes
        {
            get;
            set;
        }

        public virtual DbSet<TblLnsBrandLensType> TblLnsBrandLensTypes
        {
            get;
            set;
        }

        public virtual DbSet<TblLnsBrandLensTypeR> TblLnsBrandLensTypeRs
        {
            get;
            set;
        }

        public virtual DbSet<TblLnsCoating> TblLnsCoatings
        {
            get;
            set;
        }

        public virtual DbSet<TblLnsColoringType> TblLnsColoringTypes
        {
            get;
            set;
        }

        public virtual DbSet<TblLnsCyl> TblLnsCyls
        {
            get;
            set;
        }

        public virtual DbSet<TblLnsDesignType> TblLnsDesignTypes
        {
            get;
            set;
        }

        public virtual DbSet<TblLnsCustomLensTypeCoating> TblLnsCustomLensTypeCoatings
        {
            get;
            set;
        }

        public virtual DbSet<TblLnsCustomDesignTypeAddition> TblLnsCustomDesignTypeAdditions
        {
            get;
            set;
        }

        public virtual DbSet<TblLnsCustomLensTypeMaterial> TblLnsCustomLensTypeMaterials
        {
            get;
            set;
        }

        public virtual DbSet<TblLnsCustomLensIndex> TblLnsCustomLensIndices
        {
            get;
            set;
        }

        public virtual DbSet<TblLnsCustomSph> TblLnsCustomSphs
        {
            get;
            set;
        }

        public virtual DbSet<TblLnsCustomCyl> TblLnsCustomCyls
        {
            get;
            set;
        }

        public virtual DbSet<TblLnsDesignTypeLensIndex> TblLnsDesignTypeLensIndices
        {
            get;
            set;
        }

        public virtual DbSet<TblLnsFrameType> TblLnsFrameTypes
        {
            get;
            set;
        }

        public virtual DbSet<TblLnsLensIndex> TblLnsLensIndices
        {
            get;
            set;
        }

        public virtual DbSet<TblLnsLensIndexMaterialType> TblLnsLensIndexMaterialTypes
        {
            get;
            set;
        }

        public virtual DbSet<TblLnsLensIndexR> TblLnsLensIndexRs
        {
            get;
            set;
        }

        public virtual DbSet<TblLnsLensIndexRSph> TblLnsLensIndexRSphs
        {
            get;
            set;
        }

        public virtual DbSet<TblLnsLensType> TblLnsLensTypes
        {
            get;
            set;
        }

        public virtual DbSet<TblLnsLensTypeR> TblLnsLensTypeRs
        {
            get;
            set;
        }

        public virtual DbSet<TblLnsLensTypeRLensIndexR> TblLnsLensTypeRLensIndexRs
        {
            get;
            set;
        }

        public virtual DbSet<TblLnsMaterialType> TblLnsMaterialTypes
        {
            get;
            set;
        }

        public virtual DbSet<TblLnsOrder> TblLnsOrders
        {
            get;
            set;
        }

        public virtual DbSet<TblLnsOrderItem> TblLnsOrderItems
        {
            get;
            set;
        }

        public virtual DbSet<TblLnsOrderservice> TblLnsOrderservices
        {
            get;
            set;
        }

        public virtual DbSet<TblLnsSph> TblLnsSphs
        {
            get;
            set;
        }

        public virtual DbSet<TblLnsSphCyl> TblLnsSphCyls
        {
            get;
            set;
        }

        public virtual DbSet<TblLnsTemp> TblLnsTemps
        {
            get;
            set;
        }

        public virtual DbSet<TblWfwAttach> TblWfwAttaches
        {
            get;
            set;
        }

        public virtual DbSet<TblWfwCartable> TblWfwCartables
        {
            get;
            set;
        }

        public virtual DbSet<TblWfwCondition> TblWfwConditions
        {
            get;
            set;
        }

        public virtual DbSet<TblWfwProcess> TblWfwProcesses
        {
            get;
            set;
        }

        public virtual DbSet<TblWfwProcessAction> TblWfwProcessActions
        {
            get;
            set;
        }

        public virtual DbSet<TblWfwProcessStep> TblWfwProcessSteps
        {
            get;
            set;
        }

        public virtual DbSet<TblWfwRelationStep> TblWfwRelationSteps
        {
            get;
            set;
        }

        public virtual DbSet<TblWfwRelationStepCondition> TblWfwRelationStepConditions
        {
            get;
            set;
        }

        public virtual DbSet<TblWfwResultStep> TblWfwResultSteps
        {
            get;
            set;
        }

        public virtual DbSet<TblWfwRoleStep> TblWfwRoleSteps
        {
            get;
            set;
        }

        public virtual DbSet<TblWfwStatus> TblWfwStatuses
        {
            get;
            set;
        }

        public virtual DbSet<TblWfwStepAction> TblWfwStepActions
        {
            get;
            set;
        }

        public virtual DbSet<TblWfwUserCartable> TblWfwUserCartables
        {
            get;
            set;
        }

        public virtual DbSet<UserPermission> UserPermissions
        {
            get;
            set;
        }

        public virtual DbSet<UserRole> UserRoles
        {
            get;
            set;
        }

		public virtual DbSet<User> Users
		{
			get;
			set;
		}
		public virtual DbSet<UserActivityLog> UserActivityLogs
		{
			get;
			set;
		}
		public virtual DbSet<UserActivityLogDetail> UserActivityLogDetails
		{
			get;
			set;
		}

        public virtual DbSet<TblDefineService> TblDefineServices
        {
            get;
            set;
        }

        public virtual DbSet<TblInfoCustomer> TblInfoCustomers
        {
            get;
            set;
        }
		public virtual DbSet<ViewSalListCustomer> ViewSalListCustomers
		{
			get;
			set;
		}
		public virtual DbSet<TblWfwOrderProcess> TblWfwOrderProcesses
		{
			get;
			set;
		}

		public virtual DbSet<TblWfwOrderProcessStep> TblWfwOrderProcessSteps
		{
			get;
			set;
		}
		public virtual DbSet<TblWfwOrderRawMaterial> TblWfwOrderRawMaterials
		{
			get;
			set;
		}
        public virtual DbSet<TblAccDefineCostCenter> TblAccDefineCostCenters
        {
            get;
            set;
        }

        public virtual DbSet<TblMainDefineUser> TblMainDefineUsers
        {
            get;
            set;
        }
        public virtual DbSet<BaseRequeste> BaseRequestes
        {
            get;
            set;
        }

        public virtual DbSet<BaseRequestField> BaseRequestFieldeds
        {
            get;
            set;
        }

        public virtual DbSet<BaseRequestStep> BaseRequestSteps
        {
            get;
            set;
        }

        public virtual DbSet<RequestFielde> RequestFieldes
        {
            get;
            set;
        }

        public virtual DbSet<RequestProcess> RequestProcesses
        {
            get;
            set;
        }

        public virtual DbSet<RequestProcessStep> RequestProcessSteps
        {
            get;
            set;
        }

        public virtual DbSet<Request> Requests
        {
            get;
            set;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfiguration<Permission>(new PermissionConfiguration());
			modelBuilder.ApplyConfiguration<RolePermission>(new RolePermissionConfiguration());
            modelBuilder.ApplyConfiguration<Role>(new RoleConfiguration());
            modelBuilder.ApplyConfiguration<RoleToRole>(new RoleToRoleConfiguration());
            modelBuilder.ApplyConfiguration<TblClrDefineObject>(new TblClrDefineObjectConfiguration());
            modelBuilder.ApplyConfiguration<TblLnsBrand>(new TblLnsBrandConfiguration());
            modelBuilder.ApplyConfiguration<TblLnsBrandCoating>(new TblLnsBrandCoatingConfiguration());
            modelBuilder.ApplyConfiguration<TblLnsBrandDesignType>(new TblLnsBrandDesignTypeConfiguration());
            modelBuilder.ApplyConfiguration<TblLnsBrandLensType>(new TblLnsBrandLensTypeConfiguration());
            modelBuilder.ApplyConfiguration<TblLnsBrandLensTypeR>(new TblLnsBrandLensTypeRConfiguration());
            modelBuilder.ApplyConfiguration<TblLnsCoating>(new TblLnsCoatingConfiguration());
            modelBuilder.ApplyConfiguration<TblLnsColoringType>(new TblLnsColoringTypeConfiguration());
            modelBuilder.ApplyConfiguration<TblLnsCyl>(new TblLnsCylConfiguration());
            modelBuilder.ApplyConfiguration<TblLnsDesignType>(new TblLnsDesignTypeConfiguration());
            modelBuilder.ApplyConfiguration<TblLnsCustomLensTypeCoating>(new TblLnsCustomLensTypeCoatingConfiguration());
            modelBuilder.ApplyConfiguration<TblLnsCustomDesignTypeAddition>(new TblLnsCustomDesignTypeAdditionConfiguration());
            modelBuilder.ApplyConfiguration<TblLnsCustomLensTypeMaterial>(new TblLnsCustomLensTypeMaterialConfiguration());
            modelBuilder.ApplyConfiguration<TblLnsCustomLensIndex>(new TblLnsCustomLensIndexConfiguration());
            modelBuilder.ApplyConfiguration<TblLnsCustomSph>(new TblLnsCustomSphConfiguration());
            modelBuilder.ApplyConfiguration<TblLnsCustomCyl>(new TblLnsCustomCylConfiguration());
            modelBuilder.ApplyConfiguration<TblLnsDesignTypeLensIndex>(new TblLnsDesignTypeLensIndexConfiguration());
            modelBuilder.ApplyConfiguration<TblLnsFrameType>(new TblLnsFrameTypeConfiguration());
            modelBuilder.ApplyConfiguration<TblLnsLensIndex>(new TblLnsLensIndexConfiguration());
            modelBuilder.ApplyConfiguration<TblLnsLensIndexMaterialType>(new TblLnsLensIndexMaterialTypeConfiguration());
            modelBuilder.ApplyConfiguration<TblLnsLensIndexR>(new TblLnsLensIndexRConfiguration());
            modelBuilder.ApplyConfiguration<TblLnsLensIndexRSph>(new TblLnsLensIndexRSphConfiguration());
            modelBuilder.ApplyConfiguration<TblLnsLensType>(new TblLnsLensTypeConfiguration());
            modelBuilder.ApplyConfiguration<TblLnsLensTypeR>(new TblLnsLensTypeRConfiguration());
            modelBuilder.ApplyConfiguration<TblLnsLensTypeRLensIndexR>(new TblLnsLensTypeRLensIndexRConfiguration());
            modelBuilder.ApplyConfiguration<TblLnsMaterialType>(new TblLnsMaterialTypeConfiguration());
            modelBuilder.ApplyConfiguration<TblLnsOrder>(new TblLnsOrderConfiguration());
            modelBuilder.ApplyConfiguration<TblLnsOrderItem>(new TblLnsOrderItemConfiguration());
            modelBuilder.ApplyConfiguration<TblLnsOrderservice>(new TblLnsOrderserviceConfiguration());
            modelBuilder.ApplyConfiguration<TblLnsSph>(new TblLnsSphConfiguration());
            modelBuilder.ApplyConfiguration<TblLnsSphCyl>(new TblLnsSphCylConfiguration());
            modelBuilder.ApplyConfiguration<TblLnsTemp>(new TblLnsTempConfiguration());
            modelBuilder.ApplyConfiguration<TblWfwAttach>(new TblWfwAttachConfiguration());
            modelBuilder.ApplyConfiguration<TblWfwCartable>(new TblWfwCartableConfiguration());
            modelBuilder.ApplyConfiguration<TblWfwCondition>(new TblWfwConditionConfiguration());
            modelBuilder.ApplyConfiguration<TblWfwProcess>(new TblWfwProcessConfiguration());
            modelBuilder.ApplyConfiguration<TblWfwProcessAction>(new TblWfwProcessActionConfiguration());
            modelBuilder.ApplyConfiguration<TblWfwProcessStep>(new TblWfwProcessStepConfiguration());
            modelBuilder.ApplyConfiguration<TblWfwRelationStep>(new TblWfwRelationStepConfiguration());
            modelBuilder.ApplyConfiguration<TblWfwRelationStepCondition>(new TblWfwRelationStepConditionConfiguration());
            modelBuilder.ApplyConfiguration<TblWfwResultStep>(new TblWfwResultStepConfiguration());
            modelBuilder.ApplyConfiguration<TblWfwRoleStep>(new TblWfwRoleStepConfiguration());
            modelBuilder.ApplyConfiguration<TblWfwStatus>(new TblWfwStatusConfiguration());
            modelBuilder.ApplyConfiguration<TblWfwStepAction>(new TblWfwStepActionConfiguration());
            modelBuilder.ApplyConfiguration<TblWfwUserCartable>(new TblWfwUserCartableConfiguration());
            modelBuilder.ApplyConfiguration<UserPermission>(new UserPermissionConfiguration());
            modelBuilder.ApplyConfiguration<UserRole>(new UserRoleConfiguration());
            modelBuilder.ApplyConfiguration<User>(new UserConfiguration());
			modelBuilder.ApplyConfiguration<UserActivityLog>(new UserActivityLogConfiguration());
			modelBuilder.ApplyConfiguration<UserActivityLogDetail>(new UserActivityLogDetailConfiguration());
            modelBuilder.ApplyConfiguration<TblDefineService>(new TblDefineServiceConfiguration());
			modelBuilder.ApplyConfiguration<TblInfoCustomer>(new TblInfoCustomerConfiguration());
			modelBuilder.ApplyConfiguration<ViewSalListCustomer>(new ViewSalListCustomerConfiguration());
			modelBuilder.ApplyConfiguration<TblWfwOrderProcess>(new TblWfwOrderProcessConfiguration());
			modelBuilder.ApplyConfiguration<TblWfwOrderProcessStep>(new TblWfwOrderProcessStepConfiguration());
			modelBuilder.ApplyConfiguration<TblWfwOrderRawMaterial>(new TblWfwOrderRawMaterialConfiguration());
            modelBuilder.ApplyConfiguration<TblAccDefineCostCenter>(new TblAccDefineCostCenterConfiguration());
            modelBuilder.ApplyConfiguration<TblMainDefineUser>(new TblMainDefineUserConfiguration());
            modelBuilder.ApplyConfiguration<BaseRequeste>(new BaseRequesteConfiguration());
            modelBuilder.ApplyConfiguration<BaseRequestField>(new BaseRequestFieldConfiguration());
            modelBuilder.ApplyConfiguration<BaseRequestStep>(new BaseRequestStepConfiguration());
            modelBuilder.ApplyConfiguration<RequestFielde>(new RequestFieldeConfiguration());
            modelBuilder.ApplyConfiguration<RequestProcess>(new RequestProcessConfiguration());
            modelBuilder.ApplyConfiguration<RequestProcessStep>(new RequestProcessStepConfiguration());
            modelBuilder.ApplyConfiguration<Request>(new RequestConfiguration());



            CustomizeMapping(ref modelBuilder);
        }

        partial void CustomizeMapping(ref ModelBuilder modelBuilder);

		public override int SaveChanges()
		{
			var auditEntries = PrepareAuditEntries();
			var result = base.SaveChanges();
			SaveAuditEntries(auditEntries);
			return result;
		}

		public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			var auditEntries = PrepareAuditEntries();
			var result = await base.SaveChangesAsync(cancellationToken);
			await SaveAuditEntriesAsync(auditEntries, cancellationToken);
			return result;
		}

		private List<AuditEntry> PrepareAuditEntries()
		{
			if (_auditContext?.CorrelationId == null)
			{
				return new List<AuditEntry>();
			}

			ChangeTracker.DetectChanges();

			var auditEntries = new List<AuditEntry>();

			foreach (var entry in ChangeTracker.Entries())
			{
				if (entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
				{
					continue;
				}

				if (entry.Entity is UserActivityLog || entry.Entity is UserActivityLogDetail)
				{
					continue;
				}

				if (entry.State == EntityState.Modified && !entry.Properties.Any(p => p.IsModified))
				{
					continue;
				}

				var auditEntry = new AuditEntry
				{
					EntityName = entry.Metadata.ClrType.Name,
					Operation = entry.State switch
					{
						EntityState.Added => "Insert",
						EntityState.Modified => "Update",
						EntityState.Deleted => "Delete",
						_ => entry.State.ToString()
					}
				};

				foreach (var property in entry.Properties)
				{
					var propertyName = property.Metadata.Name;

					if (property.Metadata.IsPrimaryKey())
					{
						if (property.IsTemporary)
						{
							auditEntry.TemporaryProperties.Add(property);
						}
						else
						{
							auditEntry.KeyValues[propertyName] = NormalizeValue(propertyName, property.CurrentValue);
						}

						continue;
					}

					if (entry.State == EntityState.Added)
					{
						auditEntry.NewValues[propertyName] = NormalizeValue(propertyName, property.CurrentValue);
					}
					else if (entry.State == EntityState.Deleted)
					{
						auditEntry.OldValues[propertyName] = NormalizeValue(propertyName, property.OriginalValue);
					}
					else if (entry.State == EntityState.Modified && property.IsModified)
					{
						auditEntry.OldValues[propertyName] = NormalizeValue(propertyName, property.OriginalValue);
						auditEntry.NewValues[propertyName] = NormalizeValue(propertyName, property.CurrentValue);
					}
				}

				auditEntries.Add(auditEntry);
			}

			return auditEntries;
		}

		private void SaveAuditEntries(List<AuditEntry> auditEntries)
		{
			if (auditEntries.Count == 0 || _auditContext?.CorrelationId == null)
			{
				return;
			}

			var correlationId = _auditContext.CorrelationId.Value;
			var userId = _currentUserService?.UserId;
			var userName = _currentUserService?.UserName;

			foreach (var auditEntry in auditEntries)
			{
				foreach (var property in auditEntry.TemporaryProperties)
				{
					var propertyName = property.Metadata.Name;
					auditEntry.KeyValues[propertyName] = NormalizeValue(propertyName, property.CurrentValue);
					if (auditEntry.NewValues.ContainsKey(propertyName))
					{
						auditEntry.NewValues[propertyName] = NormalizeValue(propertyName, property.CurrentValue);
					}
				}

				UserActivityLogDetails.Add(auditEntry.ToLogDetail(correlationId, userId, userName));
			}

			base.SaveChanges();
		}

		private async Task SaveAuditEntriesAsync(List<AuditEntry> auditEntries, CancellationToken cancellationToken)
		{
			if (auditEntries.Count == 0 || _auditContext?.CorrelationId == null)
			{
				return;
			}

			var correlationId = _auditContext.CorrelationId.Value;
			var userId = _currentUserService?.UserId;
			var userName = _currentUserService?.UserName;

			foreach (var auditEntry in auditEntries)
			{
				foreach (var property in auditEntry.TemporaryProperties)
				{
					var propertyName = property.Metadata.Name;
					auditEntry.KeyValues[propertyName] = NormalizeValue(propertyName, property.CurrentValue);
					if (auditEntry.NewValues.ContainsKey(propertyName))
					{
						auditEntry.NewValues[propertyName] = NormalizeValue(propertyName, property.CurrentValue);
					}
				}

				UserActivityLogDetails.Add(auditEntry.ToLogDetail(correlationId, userId, userName));
			}

			await base.SaveChangesAsync(cancellationToken);
		}

		private static object? NormalizeValue(string propertyName, object? value)
		{
			if (SensitiveProperties.Contains(propertyName))
			{
				return "***";
			}

			if (value is byte[])
			{
				return "[binary]";
			}

			if (value is DateTime dateTime)
			{
				return dateTime.ToString("O");
			}

			if (value is DateTimeOffset dateTimeOffset)
			{
				return dateTimeOffset.ToString("O");
			}

			return value;
		}

		private sealed class AuditEntry
		{
			public string EntityName { get; set; } = string.Empty;
			public string Operation { get; set; } = string.Empty;
			public Dictionary<string, object?> KeyValues { get; } = new();
			public Dictionary<string, object?> OldValues { get; } = new();
			public Dictionary<string, object?> NewValues { get; } = new();
			public List<PropertyEntry> TemporaryProperties { get; } = new();

			public UserActivityLogDetail ToLogDetail(Guid correlationId, int? userId, string? userName)
			{
				var fields = new Dictionary<string, object?>();
				foreach (var key in OldValues.Keys.Union(NewValues.Keys))
				{
					var field = new Dictionary<string, object?>();
					if (OldValues.TryGetValue(key, out var oldValue))
					{
						field["old"] = oldValue;
					}
					if (NewValues.TryGetValue(key, out var newValue))
					{
						field["new"] = newValue;
					}
					fields[key] = field;
				}

				return new UserActivityLogDetail
				{
					CorrelationId = correlationId,
					UserId = userId,
					UserName = userName,
					EntityName = EntityName,
					EntityId = KeyValues.Count > 0 ? string.Join(",", KeyValues.Select(kv => $"{kv.Key}:{kv.Value}")) : null,
					Operation = Operation,
					Changes = fields.Count > 0 ? JsonSerializer.Serialize(fields) : null,
					CreatedAt = DateTime.Now
				};
			}
		}

        public bool HasChanges()
        {
            return ChangeTracker.Entries().Any(e => e.State == Microsoft.EntityFrameworkCore.EntityState.Added || e.State == Microsoft.EntityFrameworkCore.EntityState.Modified || e.State == Microsoft.EntityFrameworkCore.EntityState.Deleted);
        }

        partial void OnCreated();
    }
}
