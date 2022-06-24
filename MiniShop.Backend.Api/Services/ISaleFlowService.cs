using MiniShop.Backend.Model.Dto;
using MiniShop.Backend.Model;
using Weick.Orm.Core.Result;
using System;
using System.Threading.Tasks;

namespace MiniShop.Backend.Api.Services
{
    public interface ISaleFlowService : IBaseService<SaleFlow, SaleFlowDto, int>
    {
        Task<IResultModel> GetPageByShopIdAsync(int pageIndex, int pageSize, Guid shopId);
        Task<IResultModel> GetPageByShopIdWhereQueryAsync(int pageIndex, int pageSize, Guid shopId, DateTime? startTime, DateTime? endTime);
    }

    public interface ICreateSaleFlowService : IBaseService<SaleFlow, SaleFlowCreateDto, int>
    {
    }

}
