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
    public class PurchaseReceiveOderItemService : BaseService<PurchaseReceiveOderItem, PurchaseReceiveOderItemDto, int>, IPurchaseReceiveOderItemService, IDependency
    {
        public PurchaseReceiveOderItemService(Lazy<IMapper> mapper, IUnitOfWork unitOfWork, ILogger<PurchaseReceiveOderItemService> logger,
            Lazy<IRepository<PurchaseReceiveOderItem>> _repository) : base(mapper, unitOfWork, logger, _repository)
        {

        }


        public async Task<IResultModel> GetPageByShopIdPurchaseReceiveOderIdAsync(int pageIndex, int pageSize, Guid shopId, int purchaseReceiveOderId)
        {
            var data = _repository.Value.TableNoTracking;
            data = data.Where(p => p.ShopId == shopId && p.PurchaseReceiveOderId == purchaseReceiveOderId);
            var list = await data.ProjectTo<PurchaseReceiveOderItemDto>(_mapper.Value.ConfigurationProvider).ToPagedListAsync(pageIndex, pageSize);
            return ResultModel.Success(list);
        }

        public async Task<IResultModel> GetSumAmountByPurchaseReceiveOderIdAsync(int purchaseReceiveOderId)
        {
            var data = _repository.Value.TableNoTracking;
            data = data.Where(p => p.PurchaseReceiveOderId == purchaseReceiveOderId);
            var sumAmount = await data.SumAsync(p => p.Amount);
            return ResultModel.Success(sumAmount);
        }

        public async Task<IResultModel> GetListAllByShopIdPurchaseReceiveOderIdAsync(Guid shopId, int purchaseReceiveOderId, bool isDescending = false)
        {
            var data = _repository.Value.TableNoTracking;
            data = data.Where(p => p.ShopId == shopId && p.PurchaseReceiveOderId == purchaseReceiveOderId);
            if (isDescending)
            {
                var Descendinglist = await data.OrderByDescending(k => k.Id).ProjectTo<PurchaseReceiveOderItemDto>(_mapper.Value.ConfigurationProvider).ToListAsync();
                return ResultModel.Success(Descendinglist);
            }
            var list = await data.OrderBy(k => k.Id).ProjectTo<PurchaseReceiveOderItemDto>(_mapper.Value.ConfigurationProvider).ToListAsync();
            return ResultModel.Success(list);
        }
    }

    public class CreatePurchaseReceiveOderItemService : BaseService<PurchaseReceiveOderItem, PurchaseReceiveOderItemCreateDto, int>, ICreatePurchaseReceiveOderItemService, IDependency
    {
        public CreatePurchaseReceiveOderItemService(Lazy<IMapper> mapper, IUnitOfWork unitOfWork, ILogger<CreatePurchaseReceiveOderItemService> logger,
            Lazy<IRepository<PurchaseReceiveOderItem>> repository) : base(mapper, unitOfWork, logger, repository)
        {

        }
    }


    public class UpdatePurchaseReceiveOderItemService : BaseService<PurchaseReceiveOderItem, PurchaseReceiveOderItemUpdateDto, int>, IUpdatePurchaseReceiveOderItemService, IDependency
    {
        public UpdatePurchaseReceiveOderItemService(Lazy<IMapper> mapper, IUnitOfWork unitOfWork, ILogger<UpdatePurchaseReceiveOderItemService> logger, Lazy<IRepository<PurchaseReceiveOderItem>> repository)
        : base(mapper, unitOfWork, logger, repository)
        {

        }
    }
}
