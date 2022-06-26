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
    public class PromotionDiscountCategorieService : BaseService<PromotionDiscountCategorie, PromotionDiscountCategorieDto, int>, IPromotionDiscountCategorieService, IDependency
    {
        public PromotionDiscountCategorieService(Lazy<IMapper> mapper, IUnitOfWork unitOfWork, ILogger<PromotionDiscountCategorieService> logger,
            Lazy<IRepository<PromotionDiscountCategorie>> _repository) : base(mapper, unitOfWork, logger, _repository)
        {

        }

        public async Task<IResultModel> GetPageByShopIdAsync(int pageIndex, int pageSize, Guid shopId)
        {
            var data = _repository.Value.TableNoTracking;
            data = data.Where(p => p.ShopId == shopId);
            var list = await data.ProjectTo<PromotionDiscountCategorieDto>(_mapper.Value.ConfigurationProvider).ToPagedListAsync(pageIndex, pageSize);
            return ResultModel.Success(list);
        }

        public async Task<IResultModel> GetPageByShopIdPromotionOderIdAsync(int pageIndex, int pageSize, Guid shopId, int promotionId)
        {
            var data = _repository.Value.TableNoTracking;
            data = data.Where(p => p.ShopId == shopId && p.PromotionOderId == promotionId);
            var list = await data.ProjectTo<PromotionDiscountCategorieDto>(_mapper.Value.ConfigurationProvider).ToPagedListAsync(pageIndex, pageSize);
            return ResultModel.Success(list);
        }

        public async Task<IResultModel> GetListAllByShopIdPromotionOderIdAsync(Guid shopId, int promotionOderId, bool isDescending = false)
        {
            var data = _repository.Value.TableNoTracking;
            data = data.Where(p => p.ShopId == shopId && p.PromotionOderId == promotionOderId);
            if (isDescending)
            {
                var Descendinglist = await data.OrderByDescending(k => k.Id).ProjectTo<PromotionDiscountCategorieDto>(_mapper.Value.ConfigurationProvider).ToListAsync();
                return ResultModel.Success(Descendinglist);
            }
            var list = await data.OrderBy(k => k.Id).ProjectTo<PromotionDiscountCategorieDto>(_mapper.Value.ConfigurationProvider).ToListAsync();
            return ResultModel.Success(list);
        }
    }

    public class CreatePromotionDiscountCategorieService : BaseService<PromotionDiscountCategorie, PromotionDiscountCategorieCreateDto, int>, ICreatePromotionDiscountCategorieService, IDependency
    {
        public CreatePromotionDiscountCategorieService(Lazy<IMapper> mapper, IUnitOfWork unitOfWork, ILogger<CreatePromotionDiscountCategorieService> logger,
            Lazy<IRepository<PromotionDiscountCategorie>> repository) : base(mapper, unitOfWork, logger, repository)
        {

        }
    }


    public class UpdatePromotionDiscountCategorieService : BaseService<PromotionDiscountCategorie, PromotionDiscountCategorieUpdateDto, int>, IUpdatePromotionDiscountCategorieService, IDependency
    {
        public UpdatePromotionDiscountCategorieService(Lazy<IMapper> mapper, IUnitOfWork unitOfWork, ILogger<UpdatePromotionDiscountCategorieService> logger, Lazy<IRepository<PromotionDiscountCategorie>> repository)
        : base(mapper, unitOfWork, logger, repository)
        {

        }
    }
}
