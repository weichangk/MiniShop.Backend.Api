using MiniShop.Backend.Model.Dto;
using MiniShop.Backend.Model;
using Weick.Orm.Core.Result;
using System;
using System.Threading.Tasks;

namespace MiniShop.Backend.Api.Services
{
    public interface IPromotionDiscountCategorieService : IBaseService<PromotionDiscountCategorie, PromotionDiscountCategorieDto, int>
    {
        Task<IResultModel> GetPageByShopIdAsync(int pageIndex, int pageSize, Guid shopId);

        Task<IResultModel> GetPageByShopIdPromotionOderIdAsync(int pageIndex, int pageSize, Guid shopId, int promotionOderId);

        Task<IResultModel>  GetListAllByShopIdPromotionOderIdAsync(Guid shopId, int promotionOderId, bool isDescending = false);
    }

    public interface ICreatePromotionDiscountCategorieService : IBaseService<PromotionDiscountCategorie, PromotionDiscountCategorieCreateDto, int>
    {

    }

    public interface IUpdatePromotionDiscountCategorieService : IBaseService<PromotionDiscountCategorie, PromotionDiscountCategorieUpdateDto, int>
    {

    }
}
