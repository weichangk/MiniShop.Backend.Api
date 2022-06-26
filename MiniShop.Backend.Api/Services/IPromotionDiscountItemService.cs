using MiniShop.Backend.Model.Dto;
using MiniShop.Backend.Model;
using Weick.Orm.Core.Result;
using System;
using System.Threading.Tasks;

namespace MiniShop.Backend.Api.Services
{
    public interface IPromotionDiscountItemService : IBaseService<PromotionDiscountItem, PromotionDiscountItemDto, int>
    {
        Task<IResultModel> GetPageByShopIdAsync(int pageIndex, int pageSize, Guid shopId);

        Task<IResultModel> GetPageByShopIdPromotionOderIdAsync(int pageIndex, int pageSize, Guid shopId, int promotionOderId);

        Task<IResultModel>  GetListAllByShopIdPromotionOderIdAsync(Guid shopId, int promotionOderId, bool isDescending = false);
    }

    public interface ICreatePromotionDiscountItemService : IBaseService<PromotionDiscountItem, PromotionDiscountItemCreateDto, int>
    {

    }

    public interface IUpdatePromotionDiscountItemService : IBaseService<PromotionDiscountItem, PromotionDiscountItemUpdateDto, int>
    {

    }
}
