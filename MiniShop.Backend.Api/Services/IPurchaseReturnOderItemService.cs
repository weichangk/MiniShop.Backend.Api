using MiniShop.Backend.Model.Dto;
using MiniShop.Backend.Model;
using Weick.Orm.Core.Result;
using System;
using System.Threading.Tasks;

namespace MiniShop.Backend.Api.Services
{
    public interface IPurchaseReturnOderItemService : IBaseService<PurchaseReturnOderItem, PurchaseReturnOderItemDto, int>
    {
        Task<IResultModel> GetPageByShopIdPurchaseReturnOderIdAsync(int pageIndex, int pageSize, Guid shopId, int purchaseReturnOderId);

        Task<IResultModel> GetSumAmountByPurchaseReturnOderIdAsync(int purchaseReturnOderId);

        Task<IResultModel>  GetListAllByShopIdPurchaseReturnOderIdAsync(Guid shopId, int purchaseReturnOderId, bool isDescending = false);
    }

    public interface ICreatePurchaseReturnOderItemService : IBaseService<PurchaseReturnOderItem, PurchaseReturnOderItemCreateDto, int>
    {

    }

    public interface IUpdatePurchaseReturnOderItemService : IBaseService<PurchaseReturnOderItem, PurchaseReturnOderItemUpdateDto, int>
    {

    }
}
