using MiniShop.Backend.Model.Dto;
using MiniShop.Backend.Model;
using Weick.Orm.Core.Result;
using System;
using System.Threading.Tasks;
using MiniShop.Backend.Model.Enums;

namespace MiniShop.Backend.Api.Services
{
    public interface IPromotionOderService : IBaseService<PromotionOder, PromotionOderDto, int>
    {
        Task<IResultModel> GetByShopIdOderNoAsync(Guid shopId, string oderNo);

        Task<IResultModel> GetPageByShopIdAsync(int pageIndex, int pageSize, Guid shopId);

        Task<IResultModel> GetPageByShopIdWhereQueryAsync(int pageIndex, int pageSize, Guid shopId, string oderNo);
    }

    public interface ICreatePromotionOderService : IBaseService<PromotionOder, PromotionOderCreateDto, int>
    {

    }
    public interface IUpdatePromotionOderService : IBaseService<PromotionOder, PromotionOderUpdateDto, int>
    {
        
    }
}
