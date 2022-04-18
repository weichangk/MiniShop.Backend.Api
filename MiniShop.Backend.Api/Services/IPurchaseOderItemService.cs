using MiniShop.Backend.Model.Dto;
using MiniShop.Backend.Model;
using Weick.Orm.Core.Result;
using System;
using System.Threading.Tasks;

namespace MiniShop.Backend.Api.Services
{
    public interface IPurchaseOderItemService : IBaseService<PurchaseOderItem, PurchaseOderItemDto, int>
    {
        Task<IResultModel> GetByIdOnShopAsync(Guid shopId, int id);

        Task<IResultModel> GetPageByShopIdAsync(int pageIndex, int pageSize, Guid shopId);

    }

    public interface ICreatePurchaseOderItemService : IBaseService<PurchaseOderItem, PurchaseOderItemCreateDto, int>
    {

    }

    public interface IUpdatePurchaseOderItemService : IBaseService<PurchaseOderItem, PurchaseOderItemUpdateDto, int>
    {

    }
}
