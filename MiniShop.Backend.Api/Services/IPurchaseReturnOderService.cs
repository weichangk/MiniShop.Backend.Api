using MiniShop.Backend.Model.Dto;
using MiniShop.Backend.Model;
using Weick.Orm.Core.Result;
using System;
using System.Threading.Tasks;

namespace MiniShop.Backend.Api.Services
{
    public interface IPurchaseReturnOderService : IBaseService<PurchaseReturnOder, PurchaseReturnOderDto, int>
    {
        Task<IResultModel> GetByShopIdOderNoAsync(Guid shopId, string oderNo);

        Task<IResultModel> GetPageByShopIdAsync(int pageIndex, int pageSize, Guid shopId);

        Task<IResultModel> GetPageByShopIdWhereQueryAsync(int pageIndex, int pageSize, Guid shopId, string oderNo);
    }

    public interface ICreatePurchaseReturnOderService : IBaseService<PurchaseReturnOder, PurchaseReturnOderCreateDto, int>
    {

    }

    public interface IUpdatePurchaseReturnOderService : IBaseService<PurchaseReturnOder, PurchaseReturnOderUpdateDto, int>
    {
        Task<IResultModel> UpdateReturnOderAmountAsync(int id, decimal oderAmount);
    }
}
