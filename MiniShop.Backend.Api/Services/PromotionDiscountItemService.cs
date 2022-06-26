using AutoMapper;
using Microsoft.Extensions.Logging;
using MiniShop.Backend.Model.Dto;
using MiniShop.Backend.Model;
using System;
using Weick.Orm.Core;
using System.Threading.Tasks;
using Weick.Orm.Core.Result;
using AutoMapper.QueryableExtensions;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MiniShop.Backend.Api.Services
{
    public class PromotionDiscountItemService : BaseService<PromotionDiscountItem, PromotionDiscountItemDto, int>, IPromotionDiscountItemService, IDependency
    {
        public PromotionDiscountItemService(Lazy<IMapper> mapper, IUnitOfWork unitOfWork, ILogger<PromotionDiscountItemService> logger,
            Lazy<IRepository<PromotionDiscountItem>> _repository) : base(mapper, unitOfWork, logger, _repository)
        {

        }

        public async Task<IResultModel> GetPageByShopIdAsync(int pageIndex, int pageSize, Guid shopId)
        {
            var data = _repository.Value.TableNoTracking;
            data = data.Where(p => p.ShopId == shopId);
            var list = await data.ProjectTo<PromotionDiscountItemDto>(_mapper.Value.ConfigurationProvider).ToPagedListAsync(pageIndex, pageSize);
            return ResultModel.Success(list);
        }

        public async Task<IResultModel> GetPageByShopIdPromotionOderIdAsync(int pageIndex, int pageSize, Guid shopId, int promotionId)
        {
            var data = _repository.Value.TableNoTracking;
            data = data.Where(p => p.ShopId == shopId && p.PromotionOderId == promotionId);
            var list = await data.ProjectTo<PromotionDiscountItemDto>(_mapper.Value.ConfigurationProvider).ToPagedListAsync(pageIndex, pageSize);
            return ResultModel.Success(list);
        }

        public async Task<IResultModel> GetListAllByShopIdPromotionOderIdAsync(Guid shopId, int promotionOderId, bool isDescending = false)
        {
            var data = _repository.Value.TableNoTracking;
            data = data.Where(p => p.ShopId == shopId && p.PromotionOderId == promotionOderId);
            if (isDescending)
            {
                var Descendinglist = await data.OrderByDescending(k => k.Id).ProjectTo<PromotionDiscountItemDto>(_mapper.Value.ConfigurationProvider).ToListAsync();
                return ResultModel.Success(Descendinglist);
            }
            var list = await data.OrderBy(k => k.Id).ProjectTo<PromotionDiscountItemDto>(_mapper.Value.ConfigurationProvider).ToListAsync();
            return ResultModel.Success(list);
        }
    }

    public class CreatePromotionDiscountItemService : BaseService<PromotionDiscountItem, PromotionDiscountItemCreateDto, int>, ICreatePromotionDiscountItemService, IDependency
    {
        public CreatePromotionDiscountItemService(Lazy<IMapper> mapper, IUnitOfWork unitOfWork, ILogger<CreatePromotionDiscountItemService> logger,
            Lazy<IRepository<PromotionDiscountItem>> repository) : base(mapper, unitOfWork, logger, repository)
        {

        }
    }


    public class UpdatePromotionDiscountItemService : BaseService<PromotionDiscountItem, PromotionDiscountItemUpdateDto, int>, IUpdatePromotionDiscountItemService, IDependency
    {
        public UpdatePromotionDiscountItemService(Lazy<IMapper> mapper, IUnitOfWork unitOfWork, ILogger<UpdatePromotionDiscountItemService> logger, Lazy<IRepository<PromotionDiscountItem>> repository)
        : base(mapper, unitOfWork, logger, repository)
        {

        }
    }
}
