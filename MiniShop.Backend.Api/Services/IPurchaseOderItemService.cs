using MiniShop.Backend.Model.Dto;
using MiniShop.Backend.Model;
using Weick.Orm.Core.Result;
using System;
using System.Threading.Tasks;

namespace MiniShop.Backend.Api.Services
{
    public interface IPurchaseOderItemService : IBaseService<PurchaseOderItem, PurchaseOderItemDto, int>
    {
        Task<IResultModel> GetPageByShopIdAsync(int pageIndex, int pageSize, Guid shopId);

        Task<IResultModel> GetPageByShopIdPurchaseOderIdAsync(int pageIndex, int pageSize, Guid shopId, int purchaseOderId);

    }

    public interface ICreatePurchaseOderItemService : IBaseService<PurchaseOderItem, PurchaseOderItemCreateDto, int>
    {

    }

    public interface IUpdatePurchaseOderItemService : IBaseService<PurchaseOderItem, PurchaseOderItemUpdateDto, int>
    {

    }
}
