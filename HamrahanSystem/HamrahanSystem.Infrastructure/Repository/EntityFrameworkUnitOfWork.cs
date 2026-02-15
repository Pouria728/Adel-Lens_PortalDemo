
using System;
using System.Collections.Generic;
using HamrahanSystem.Domain.Entity;
using HamrahanSystem.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace HamrahanSystem.Infrastructure.Repository
{
	public partial class EntityFrameworkUnitOfWork : IUnitOfWork
	{
		protected DbContext context = null;

		IRolePermissionRepository _RolePermissions;
		IRoleRepository _Roles;
		IRoleToRoleRepository _RoleToRoles;
		ITblClrDefineObjectRepository _TblClrDefineObjectes;
		ITblDefineServiceRepository _TblDefineService;
		ITblInfoCustomerRepository _TblInfoCustomers;
		ITblLnsBrandCoatingRepository _TblLnsBrandCoatings;
		ITblLnsBrandDesignTypeRepository _TblLnsBrandDesignTypes;
		ITblLnsBrandLensTypeRRepository _TblLnsBrandLensTypeRs;
		ITblLnsBrandLensTypeRepository _TblLnsBrandLensTypes;
		ITblLnsBrandRepository _TblLnsBrands;
		ITblLnsCoatingRepository _TblLnsCoatings;
		ITblLnsColoringTypeRepository _TblLnsColoringTypes;
		ITblLnsCylRepository _TblLnsCyls;
		ITblLnsDesignTypeLensIndexRepository _TblLnsDesignTypeLensIndexs;
		ITblLnsDesignTypeRepository _TblLnsDesignTypes;
		ITblLnsFrameTypeRepository _TblLnsFrameTypes;
		ITblLnsLensIndexMaterialTypeRepository _TblLnsLensIndexMaterials;
		ITblLnsLensIndexRRepository _TblLnsLensIndexRs;
		ITblLnsLensIndexRSphRepository _TblLnsLensIndexRSphs;
		ITblLnsLensIndexRepository _TblLnsLensIndexs;
		ITblLnsLensTypeRLensIndexRRepository _TblLnsLensTypeRLensIndexRs;
		ITblLnsLensTypeRRepository _TblLnsLensTypeRs;
		ITblLnsLensTypeRepository _TblLnsLensTypes;
		ITblLnsMaterialTypeRepository _TblLnsMaterialTypes;
		ITblLnsOrderItemRepository _TblLnsOrderItems;
		ITblLnsOrderRepository _TblLnsOrders;
		ITblLnsOrderserviceRepository _TblLnsOrderservices;
		ITblLnsSphCylRepository _TblLnsSphCyls;
		ITblLnsSphRepository _TblLnsSphs;
		ITblLnsTempRepository _TblLnsTemps;
		ITblWfwAttachRepository _TblWfwAttachs;
		ITblWfwCartableRepository _TblWfwCartables;
		ITblWfwConditionRepository _TblWfwConditions;
		ITblWfwProcessActionRepository _TblWfwProcessActions;
		ITblWfwProcessRepository _TblWfwProcesses;
		ITblWfwProcessStepRepository _TblWfwProcessSteps;
		ITblWfwRelationStepConditionRepository _TblWfwRelationStepConditions;
		ITblWfwRelationStepRepository _TblWfwRelationSteps;
		ITblWfwResultStepRepository _TblWfwResultSteps;
		ITblWfwRoleStepRepository _TblWfwRoleSteps;
		ITblWfwStatusRepository _TblWfwStatuss;
		ITblWfwStepActionRepository _TblWfwStepActions;
		ITblWfwUserCartableRepository _TblWfwUserCartables;
		IUserPermissionRepository _UserPermissions;
		IUserRoleRepository _UserRoles;
		IUserRepository _Users;
		IViewSalListCustomerRepository _ViewSalListCustomer;
		ITblWfwOrderProcessRepository _TblWfwOrderProcess;
		ITblWfwOrderProcessStepRepository _TblWfwOrderProcessStep;

		public EntityFrameworkUnitOfWork(DbContextOptions<AdelModel> options) : this(new AdelModel(options))
		{
		}

		public EntityFrameworkUnitOfWork(DbContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			this.context = context;
		}

		public DbContext Context
		{
			get
			{
				return context;
			}
		}

		protected virtual void CloseContext()
		{
			if (context != null)
			{
				context.Dispose();
				context = null;
			}
		}

		#region IDisposable Methods

		private bool disposed = false;

		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					CloseContext();
				}
			}
			this.disposed = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		#endregion

		#region IUnitOfWork Members

		public virtual void Save()
		{
			if (context == null)
				throw new InvalidOperationException("Context has not been initialized.");
			context.SaveChanges();
		}
		public IRolePermissionRepository RolePermissions	
		{
			get
			{
				if (_RolePermissions == null)
					_RolePermissions = new RolePermissionRepository((AdelModel)context);
				return _RolePermissions;
			}
		}
		



		#endregion
	}
}
