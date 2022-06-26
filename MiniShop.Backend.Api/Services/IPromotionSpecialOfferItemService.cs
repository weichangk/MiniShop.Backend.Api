using MiniShop.Backend.Model.Dto;
using MiniShop.Backend.Model;
using Weick.Orm.Core.Result;
using System;
using System.Threading.Tasks;

namespace MiniShop.Backend.Api.Services
{
    public interface IPromotionSpecialOfferItemService : IBaseService<PromotionSpecialOfferItem, PromotionSpecialOfferItemDto, int>
    {
        Task<IResultModel> GetPageByShopIdAsync(int pageIndex, int pageSize, Guid shopId);

        Task<IResultModel> GetPageByShopIdPromotionOderIdAsync(int pageIndex, int pageSize, Guid shopId, int promotionOderId);

        Task<IResultModel>  GetListAllByShopIdPromotionOderIdAsync(Guid shopId, int promotionOderId, bool isDescending = false);
    }

    public interface ICreatePromotionSpecialOfferItemService : IBaseService<PromotionSpecialOfferItem, PromotionSpecialOfferItemCreateDto, int>
    {

    }

    public interface IUpdatePromotionSpecialOfferItemService : IBaseService<PromotionSpecialOfferItem, PromotionSpecialOfferItemUpdateDto, int>
    {

    }
}
