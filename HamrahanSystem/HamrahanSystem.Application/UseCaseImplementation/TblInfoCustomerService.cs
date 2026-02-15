using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HamrahanSystem.Application.UseCaseInterface;
using HamrahanSystem.Domain.Repository;
using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Application.MappingImplementation;

namespace HamrahanSystem.Application.UseCaseImplementation
{
    public class TblInfoCustomerService(ITblInfoCustomerRepository TblInfoCustomerRepository) :ITblInfoCustomerService
    {
        public Task Add(TblInfoCustomerDto dto)
        {
            throw new NotImplementedException();
        }

        public Task Add(List<TblInfoCustomerDto> dto)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task Delete(List<TblInfoCustomerDto> dto)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TblInfoCustomerDto>> GetAll()
        {
            var TblInfoCustomers=TblInfoCustomerRepository.GetAll();
            
            return TblInfoCustomerConverter.ToDtos(TblInfoCustomers);

        }

        public async Task<TblInfoCustomerDto> GetById(int id)
        {
            var TblInfoCustomer = TblInfoCustomerRepository.GetByKey(0, id);
            return TblInfoCustomerConverter.ToDto(TblInfoCustomer);
        }

        public Task Update(TblInfoCustomerDto dto)
        {
            throw new NotImplementedException();
        }

        public Task Update(List<TblInfoCustomerDto> dto)
        {
            throw new NotImplementedException();
        }
    }
}
