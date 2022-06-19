using MiniShop.Backend.Model.Dto;
using MiniShop.Backend.Model;
using Weick.Orm.Core.Result;
using System;
using System.Threading.Tasks;
using MiniShop.Backend.Model.Enums;

namespace MiniShop.Backend.Api.Services
{
    public interface IPurchaseOderService : IBaseService<PurchaseOder, PurchaseOderDto, int>
    {
        Task<IResultModel> GetByShopIdOderNoAsync(Guid shopId, string oderNo);

        Task<IResultModel> GetPageByShopIdAsync(int pageIndex, int pageSize, Guid shopId);

        Task<IResultModel> GetAuditedUnReceivedPageByShopIdAsync(int pageIndex, int pageSize, Guid shopId);

        Task<IResultModel> GetPageByShopIdWhereQueryAsync(int pageIndex, int pageSize, Guid shopId, string oderNo);

        Task<IResultModel> GetAuditedUnReceivedPageByShopIdWhereQueryAsync(int pageIndex, int pageSize, Guid shopId, string oderNo);

        Task<IResultModel> GetAuditedUnReturnPageByShopIdAsync(int pageIndex, int pageSize, Guid shopId);

        Task<IResultModel> GetAuditedUnReturnPageByShopIdWhereQueryAsync(int pageIndex, int pageSize, Guid shopId, string oderNo);
    }

    public interface ICreatePurchaseOderService : IBaseService<PurchaseOder, PurchaseOderCreateDto, int>
    {

    }

    public interface IUpdatePurchaseOderService : IBaseService<PurchaseOder, PurchaseOderUpdateDto, int>
    {
        Task<IResultModel> UpdateOderAmountAsync(int id, decimal oderAmount);
        Task<IResultModel> UpdatePurchaseOderStatusAsync(int id, EnumPurchaseOrderStatus state);
    }

    public interface IAuditPurchaseOderService : IBaseService<PurchaseOder, PurchaseOderAuditDto, int>
    {

    }
}
