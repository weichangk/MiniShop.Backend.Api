using MiniShop.Backend.Model.Dto;
using MiniShop.Backend.Model;
using Weick.Orm.Core.Result;
using System;
using System.Threading.Tasks;

namespace MiniShop.Backend.Api.Services
{
    public interface IPurchaseReceiveOderItemService : IBaseService<PurchaseReceiveOderItem, PurchaseReceiveOderItemDto, int>
    {
        Task<IResultModel> GetPageByShopIdPurchaseReceiveOderIdAsync(int pageIndex, int pageSize, Guid shopId, int purchaseReceiveOderId);

        Task<IResultModel> GetSumAmountByPurchaseReceiveOderIdAsync(int purchaseReceiveOderId);

        Task<IResultModel>  GetListAllByShopIdPurchaseReceiveOderIdAsync(Guid shopId, int purchaseReceiveOderId, bool isDescending = false);
    }

    public interface ICreatePurchaseReceiveOderItemService : IBaseService<PurchaseReceiveOderItem, PurchaseReceiveOderItemCreateDto, int>
    {

    }

    public interface IUpdatePurchaseReceiveOderItemService : IBaseService<PurchaseReceiveOderItem, PurchaseReceiveOderItemUpdateDto, int>
    {

    }
}
