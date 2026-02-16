using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Application.MappingImplementation;
using HamrahanSystem.Application.UseCaseInterface;
using HamrahanSystem.Domain.Entity;
using HamrahanSystem.Domain.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamrahanSystem.Application.UseCaseImplementation
{
    public class TblLnsOrderService(ITblLnsOrderRepository TblLnsOrderRepository, ITblWfwProcessRepository tblWfwProcessRepository, ITblWfwOrderProcessRepository tblWfwOrderProcessRepository) : ITblLnsOrderService
    {
        private const int CustomLensIndexDocument = 5001;
        private static readonly object CustomFactorLock = new();

        public Task Add(TblLnsOrderDto dto, bool isSend)
        {
            var item = dto.ToEntity();
            item.StatusId = 1;
            item.TblLnsOrderItems = TblLnsOrderItemConverter.ToEntities(dto.TblLnsOrderItems);

            if (item.IndexDocument == CustomLensIndexDocument)
            {
                lock (CustomFactorLock)
                {
                    item.FactorNo = BuildNextCustomFactorNo();
                    item = TblLnsOrderRepository.Add(item).Result;
                }
            }
            else
            {
                item = TblLnsOrderRepository.Add(item).Result;
                item.FactorNo = "ST" + item.OrderId.ToString().PadLeft(8, '0');
                TblLnsOrderRepository.Update(item);
            }
            if (isSend)
            {
                var itemProcess = tblWfwProcessRepository.GetFileter(item.IndexDocument);
                if (itemProcess.Any())
                {
                    if (itemProcess.First().TblWfwOrderProcesses != null)
                    {
                        var itemOrderProcess = new TblWfwOrderProcess
                        {
                            DateCreate = DateTime.Now,
                            OrderId = item.OrderId,
                            ProcessId = itemProcess.First().ProcessId,
                            StatusId = 2,
                        };
                        itemOrderProcess.TblWfwOrderProcessSteps.Add(new TblWfwOrderProcessStep
                        {
                            StatusId = 1,
                            ProcessStepId = itemProcess.First().TblWfwProcessSteps.Where(x=>x.IsActive.HasValue && x.IsActive==1).OrderBy(x => x.OrderId).First().ProcessStepId,
                            DateCreate = DateTime.Now,

                        });
                        tblWfwOrderProcessRepository.Add(itemOrderProcess);
                        item.StatusId = 2;
                        TblLnsOrderRepository.Update(item);
                    }
                }
            }

            dto.OrderId = item.OrderId;
            dto.FactorNo = item.FactorNo;
            dto.StatusId = item.StatusId;

            return Task.CompletedTask;
        }

        private string BuildNextCustomFactorNo()
        {
            var maxNumber = TblLnsOrderRepository.GetAll()
                .Where(x => x.IndexDocument == CustomLensIndexDocument && !string.IsNullOrWhiteSpace(x.FactorNo))
                .Select(x => ParseCustomFactorNo(x.FactorNo!))
                .Where(x => x.HasValue)
                .Select(x => x!.Value)
                .DefaultIfEmpty(0)
                .Max();

            return "RX" + (maxNumber + 1).ToString().PadLeft(8, '0');
        }

        private static int? ParseCustomFactorNo(string factorNo)
        {
            const string prefix = "RX";
            if (!factorNo.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            var numberPart = factorNo.Substring(prefix.Length);
            return int.TryParse(numberPart, out var parsed) ? parsed : null;
        }

        public Task Add(List<TblLnsOrderDto> dto)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task Delete(List<TblLnsOrderDto> dto)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TblLnsOrderDto>> GetAll()
        {
            var TblLnsOrders = TblLnsOrderRepository.GetAll();

            return TblLnsOrderConverter.ToDtos(TblLnsOrders);

        }
        public Task<(List<TblLnsOrderDto>, int)> GetAllByFilter(int? OrderStatusId, int? IndexDocument, int? CustomerId, int? UserId, int? maxResult, int? page, int? rowInPage, string sort, string sidx, string search = "", string? fromDate = null, string? toDate = null)
        {
            var TblLnsOrders = TblLnsOrderRepository.GetAllByFilter(OrderStatusId, IndexDocument, CustomerId, UserId, maxResult, page, rowInPage, sort, sidx, search, fromDate, toDate);
            return Task.FromResult((TblLnsOrderConverter.ToDtosWithRelated(TblLnsOrders.Result.Item1, 1), TblLnsOrders.Result.Item2));

        }

        public async Task<TblLnsOrderDto> GetById(int id)
        {
            var TblLnsOrder = TblLnsOrderRepository.GetByKey(id);
            return TblLnsOrderConverter.ToDtoWithRelated(TblLnsOrder, 1);
        }

        public Task Update(TblLnsOrderDto dto)
        {
            var item = dto.ToEntity();
            return TblLnsOrderRepository.Update(item);
        }
        public Task UpdateStatus(int id, int statusOrder)
        {
            var item = TblLnsOrderRepository.GetByKey(id);
            item.StatusId = statusOrder;
            if (statusOrder == 2)
            {
                var itemProcess = tblWfwProcessRepository.GetFileter(item.IndexDocument);
                if (itemProcess.Any())
                {
                    if (itemProcess.First().TblWfwOrderProcesses != null)
                    {
                        var itemOrderProcess = new TblWfwOrderProcess
                        {
                            DateCreate = DateTime.Now,
                            OrderId = item.OrderId,
                            ProcessId = itemProcess.First().ProcessId,
                            StatusId = 2,
                        };
                        itemOrderProcess.TblWfwOrderProcessSteps.Add(new TblWfwOrderProcessStep
                        {
                            StatusId = 1,
                            ProcessStepId = itemProcess.First().TblWfwProcessSteps.Where(x=>x.IsActive.HasValue&&x.IsActive==1).OrderBy(x => x.OrderId).First().ProcessStepId,
                            DateCreate = DateTime.Now,

                        });
                        tblWfwOrderProcessRepository.Add(itemOrderProcess);
                        item.StatusId = 2;

                    }

                }
            }



            return TblLnsOrderRepository.Update(item); ;
        }

        public Task Update(List<TblLnsOrderDto> dto)
        {
            throw new NotImplementedException();
        }
    }
}
