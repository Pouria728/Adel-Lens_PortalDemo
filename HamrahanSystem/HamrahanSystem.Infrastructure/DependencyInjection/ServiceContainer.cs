using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HamrahanSystem.Domain.Repository;
using HamrahanSystem.Infrastructure.Repository;
using HamrahanSystem.Application.UseCaseInterface;
using HamrahanSystem.Application.UseCaseImplementation;

namespace HamrahanSystem.Infrastructure.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services,IConfiguration config)
        {
            services.AddScoped<IBaseRequesteRepository, BaseRequesteRepository>();
            services.AddScoped<IBaseRequestFieldRepository, BaseRequestFieldRepository>();
            services.AddScoped<IBaseRequestStepRepository, BaseRequestStepRepository>();
            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<IRequestRepository, RequestRepository>();
            services.AddScoped<IRequestFieldeRepository, RequestFieldeRepository>();
            services.AddScoped<IRequestProcessRepository, RequestProcessRepository>();
            services.AddScoped<IRequestProcessStepRepository, RequestProcessStepRepository>();
            services.AddScoped<IRolePermissionRepository, RolePermissionRepository>();
			services.AddScoped<IRoleRepository, RoleRepository>();
			services.AddScoped<IRoleToRoleRepository, RoleToRoleRepository>();
			services.AddScoped<ITblClrDefineObjectRepository, TblClrDefineObjectRepository>();
			services.AddScoped<ITblDefineServiceRepository, TblDefineServiceRepository>();
			services.AddScoped<ITblInfoCustomerRepository, TblInfoCustomerRepository>();
			services.AddScoped<ITblLnsBrandCoatingRepository, TblLnsBrandCoatingRepository>();
			services.AddScoped<ITblLnsBrandDesignTypeRepository, TblLnsBrandDesignTypeRepository>();
			services.AddScoped<ITblLnsBrandLensTypeRRepository, TblLnsBrandLensTypeRRepository>();
			services.AddScoped<ITblLnsBrandLensTypeRepository, TblLnsBrandLensTypeRepository>();
			services.AddScoped<ITblLnsBrandRepository, TblLnsBrandRepository>();
			services.AddScoped<ITblLnsCoatingRepository, TblLnsCoatingRepository>();
			services.AddScoped<ITblLnsColoringTypeRepository, TblLnsColoringTypeRepository>();
			services.AddScoped<ITblLnsCylRepository, TblLnsCylRepository>();
			services.AddScoped<ITblLnsCustomLensTypeCoatingRepository, TblLnsCustomLensTypeCoatingRepository>();
			services.AddScoped<ITblLnsCustomLensTypeMaterialRepository, TblLnsCustomLensTypeMaterialRepository>();
			services.AddScoped<ITblLnsCustomLensIndexRepository, TblLnsCustomLensIndexRepository>();
			services.AddScoped<ITblLnsCustomDesignTypeAdditionRepository, TblLnsCustomDesignTypeAdditionRepository>();
			services.AddScoped<ITblLnsCustomSphRepository, TblLnsCustomSphRepository>();
			services.AddScoped<ITblLnsCustomCylRepository, TblLnsCustomCylRepository>();
			services.AddScoped<ITblLnsDesignTypeLensIndexRepository, TblLnsDesignTypeLensIndexRepository>();
			services.AddScoped<ITblLnsDesignTypeRepository, TblLnsDesignTypeRepository>();
			services.AddScoped<ITblLnsFrameTypeRepository, TblLnsFrameTypeRepository>();
			services.AddScoped<ITblLnsLensIndexMaterialTypeRepository, TblLnsLensIndexMaterialTypeRepository>();
			services.AddScoped<ITblLnsLensIndexRRepository, TblLnsLensIndexRRepository>();
			services.AddScoped<ITblLnsLensIndexRSphRepository, TblLnsLensIndexRSphRepository>();
			services.AddScoped<ITblLnsLensIndexRepository, TblLnsLensIndexRepository>();
			services.AddScoped<ITblLnsLensTypeRLensIndexRRepository, TblLnsLensTypeRLensIndexRRepository>();
			services.AddScoped<ITblLnsLensTypeRRepository, TblLnsLensTypeRRepository>();
			services.AddScoped<ITblLnsLensTypeRepository, TblLnsLensTypeRepository>();
			services.AddScoped<ITblLnsMaterialTypeRepository, TblLnsMaterialTypeRepository>();
			services.AddScoped<ITblLnsOrderItemRepository, TblLnsOrderItemRepository>();
			services.AddScoped<ITblLnsOrderRepository, TblLnsOrderRepository>();
			services.AddScoped<ITblLnsOrderserviceRepository, TblLnsOrderserviceRepository>();
			services.AddScoped<ITblLnsSphCylRepository, TblLnsSphCylRepository>();
			services.AddScoped<ITblLnsSphRepository, TblLnsSphRepository>();
			services.AddScoped<ITblLnsTempRepository, TblLnsTempRepository>();
			services.AddScoped<ITblWfwAttachRepository, TblWfwAttachRepository>();
			services.AddScoped<ITblWfwCartableRepository, TblWfwCartableRepository>();
			services.AddScoped<ITblWfwConditionRepository, TblWfwConditionRepository>();
			services.AddScoped<ITblWfwProcessActionRepository, TblWfwProcessActionRepository>();
			services.AddScoped<ITblWfwProcessRepository, TblWfwProcessRepository>();
			services.AddScoped<ITblWfwProcessStepRepository, TblWfwProcessStepRepository>();
			services.AddScoped<ITblWfwRelationStepConditionRepository, TblWfwRelationStepConditionRepository>();
			services.AddScoped<ITblWfwRelationStepRepository, TblWfwRelationStepRepository>();
			services.AddScoped<ITblWfwResultStepRepository, TblWfwResultStepRepository>();
			services.AddScoped<ITblWfwRoleStepRepository, TblWfwRoleStepRepository>();
			services.AddScoped<ITblWfwStatusRepository, TblWfwStatusRepository>();
			services.AddScoped<ITblWfwStepActionRepository, TblWfwStepActionRepository>();
			services.AddScoped<ITblWfwUserCartableRepository, TblWfwUserCartableRepository>();
			services.AddScoped<IUserPermissionRepository, UserPermissionRepository>();
			services.AddScoped<IUserRoleRepository, UserRoleRepository>();
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<IUserActivityLogRepository, UserActivityLogRepository>();
			services.AddScoped<IUserActivityLogDetailRepository, UserActivityLogDetailRepository>();
			services.AddScoped<IViewSalListCustomerRepository, ViewSalListCustomerRepository>();
			services.AddScoped<ITblWfwOrderProcessRepository, TblWfwOrderProcessRepository>();
			services.AddScoped<ITblWfwOrderProcessStepRepository, TblWfwOrderProcessStepRepository>();
			services.AddScoped<IUnitOfWork, EntityFrameworkUnitOfWork>();
			

			return services;
        }
    }
}
