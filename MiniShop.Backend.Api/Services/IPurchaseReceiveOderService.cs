using MiniShop.Backend.Model.Dto;
using MiniShop.Backend.Model;
using Weick.Orm.Core.Result;
using System;
using System.Threading.Tasks;

namespace MiniShop.Backend.Api.Services
{
    public interface IPurchaseReceiveOderService : IBaseService<PurchaseReceiveOder, PurchaseReceiveOderDto, int>
    {
        Task<IResultModel> GetByShopIdOderNoAsync(Guid shopId, string oderNo);

        Task<IResultModel> GetPageByShopIdAsync(int pageIndex, int pageSize, Guid shopId);

        Task<IResultModel> GetPageByShopIdWhereQueryAsync(int pageIndex, int pageSize, Guid shopId, string oderNo);

        Task<IResultModel> GetAuditedUnReturnedPageByShopIdAsync(int pageIndex, int pageSize, Guid shopId);

        Task<IResultModel> GetAuditedUnReturnedPageByShopIdWhereQueryAsync(int pageIndex, int pageSize, Guid shopId, string oderNo);
    }

    public interface ICreatePurchaseReceiveOderService : IBaseService<PurchaseReceiveOder, PurchaseReceiveOderCreateDto, int>
    {

    }

    public interface IUpdatePurchaseReceiveOderService : IBaseService<PurchaseReceiveOder, PurchaseReceiveOderUpdateDto, int>
    {
        Task<IResultModel> UpdateReceiveOderAmountAsync(int id, decimal oderAmount);
    }
}
