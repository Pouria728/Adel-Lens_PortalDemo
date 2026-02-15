using HamrahanSystem.Application.UseCaseImplementation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Application.UseCaseInterface;
using Microsoft.EntityFrameworkCore;
using HamrahanSystem.Domain.Repository;


namespace HamrahanSystem.Application.DependencyInjection
{
	public static class ServiceContainer
	{
		public static IServiceCollection AddApplicationService(this IServiceCollection services, IConfiguration config)
		{
			services.AddSingleton<ICacheService, CacheService>(options => new CacheService(config.GetConnectionString("ServerRedis"), TimeSpan.FromMinutes(int.Parse(config.GetConnectionString("CacheRedisMinute")))));
            //services.AddScoped<IMigratorService, MigratorService>();
            services.AddScoped<IBaseRequesteService, BaseRequesteService>();
            services.AddScoped<IBaseRequestFieldService, BaseRequestFieldService>();
            services.AddScoped<IBaseRequestStepService, BaseRequestStepService>();
            services.AddScoped<IPermissionService, PermissionService>();
			services.AddScoped<IRolePermissionService, RolePermissionService>();
			services.AddScoped<IRoleService, RoleService>();
			services.AddScoped<IRoleToRoleService, RoleToRoleService>();
			services.AddScoped<ITblClrDefineObjectService, TblClrDefineObjectService>();
			services.AddScoped<ITblDefineServiceService, TblDefineServiceService>();
			services.AddScoped<ITblInfoCustomerService, TblInfoCustomerService>();
			services.AddScoped<ITblLnsBrandCoatingService, TblLnsBrandCoatingService>();
			services.AddScoped<ITblLnsBrandDesignTypeService, TblLnsBrandDesignTypeService>();
			services.AddScoped<ITblLnsBrandLensTypeRService, TblLnsBrandLensTypeRService>();
			services.AddScoped<ITblLnsBrandLensTypeService, TblLnsBrandLensTypeService>();
			services.AddScoped<ITblLnsBrandService, TblLnsBrandService>();
			services.AddScoped<ITblLnsCoatingService, TblLnsCoatingService>();
			services.AddScoped<ITblLnsColoringTypeService, TblLnsColoringTypeService>();
			services.AddScoped<ITblLnsCylService, TblLnsCylService>();
			services.AddScoped<ITblLnsCustomLensTypeCoatingService, TblLnsCustomLensTypeCoatingService>();
			services.AddScoped<ITblLnsCustomLensTypeMaterialService, TblLnsCustomLensTypeMaterialService>();
			services.AddScoped<ITblLnsCustomLensIndexService, TblLnsCustomLensIndexService>();
			services.AddScoped<ITblLnsCustomDesignTypeAdditionService, TblLnsCustomDesignTypeAdditionService>();
			services.AddScoped<ITblLnsCustomSphService, TblLnsCustomSphService>();
			services.AddScoped<ITblLnsCustomCylService, TblLnsCustomCylService>();
			services.AddScoped<ITblLnsDesignTypeLensIndexService, TblLnsDesignTypeLensIndexService>();
			services.AddScoped<ITblLnsDesignTypeService, TblLnsDesignTypeService>();
			services.AddScoped<ITblLnsFrameTypeService, TblLnsFrameTypeService>();
			services.AddScoped<ITblLnsLensIndexMaterialTypeService, TblLnsLensIndexMaterialTypeService>();
			services.AddScoped<ITblLnsLensIndexRService, TblLnsLensIndexRService>();
			services.AddScoped<ITblLnsLensIndexRSphService, TblLnsLensIndexRSphService>();
			services.AddScoped<ITblLnsLensIndexService, TblLnsLensIndexService>();
			services.AddScoped<ITblLnsLensTypeRLensIndexRService, TblLnsLensTypeRLensIndexRService>();
			services.AddScoped<ITblLnsLensTypeRService, TblLnsLensTypeRService>();
			services.AddScoped<ITblLnsLensTypeService, TblLnsLensTypeService>();
			services.AddScoped<ITblLnsMaterialTypeService, TblLnsMaterialTypeService>();
			services.AddScoped<ITblLnsOrderItemService, TblLnsOrderItemService>();
			services.AddScoped<ITblLnsOrderService, TblLnsOrderService>();
			services.AddScoped<ITblLnsOrderserviceService, TblLnsOrderserviceService>();
			services.AddScoped<ITblLnsSphCylService, TblLnsSphCylService>();
			services.AddScoped<ITblLnsSphService, TblLnsSphService>();
			services.AddScoped<ITblLnsTempService, TblLnsTempService>();
			services.AddScoped<ITblWfwAttachService,TblWfwAttachService>();
			services.AddScoped<ITblWfwCartableService,TblWfwCartableService>();
			services.AddScoped<ITblWfwConditionService,TblWfwConditionService>();
			services.AddScoped<ITblWfwProcessActionService,TblWfwProcessActionService>();
			services.AddScoped<ITblWfwProcessService,TblWfwProcessService>();
			services.AddScoped<ITblWfwProcessStepService,TblWfwProcessStepService>();
			services.AddScoped<ITblWfwRelationStepConditionService,TblWfwRelationStepConditionService>();
			services.AddScoped<ITblWfwRelationStepService,TblWfwRelationStepService>();
			services.AddScoped<ITblWfwResultStepService,TblWfwResultStepService>();
			services.AddScoped<ITblWfwRoleStepService,TblWfwRoleStepService>(); 
			services.AddScoped<ITblWfwStatusService,TblWfwStatusService>(); 
			services.AddScoped<ITblWfwStepActionService,TblWfwStepActionService>();
			services.AddScoped<ITblWfwUserCartableService,TblWfwUserCartableService>();
			services.AddScoped<IUserPermissionService,UserPermissionService>();
			services.AddScoped<IUserRoleService,UserRoleService>();
			services.AddScoped<IUserService,UserService>();
			services.AddScoped<IUserActivityLogService, UserActivityLogService>();
			services.AddScoped<IViewSalListCustomerService, ViewSalListCustomerService>();
			services.AddScoped<ITblWfwOrderProcessService, TblWfwOrderProcessService>();
			services.AddScoped<ITblWfwOrderProcessStepService, TblWfwOrderProcessStepService>();


			return services;
		}
	}
}
