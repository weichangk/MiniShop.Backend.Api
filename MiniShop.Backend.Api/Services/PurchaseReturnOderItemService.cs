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
    public class PurchaseReturnOderItemService : BaseService<PurchaseReturnOderItem, PurchaseReturnOderItemDto, int>, IPurchaseReturnOderItemService, IDependency
    {
        public PurchaseReturnOderItemService(Lazy<IMapper> mapper, IUnitOfWork unitOfWork, ILogger<PurchaseReturnOderItemService> logger,
            Lazy<IRepository<PurchaseReturnOderItem>> _repository) : base(mapper, unitOfWork, logger, _repository)
        {

        }


        public async Task<IResultModel> GetPageByShopIdPurchaseReturnOderIdAsync(int pageIndex, int pageSize, Guid shopId, int purchaseReturnOderId)
        {
            var data = _repository.Value.TableNoTracking;
            data = data.Where(p => p.ShopId == shopId && p.PurchaseReturnOderId == purchaseReturnOderId);
            var list = await data.ProjectTo<PurchaseReturnOderItemDto>(_mapper.Value.ConfigurationProvider).ToPagedListAsync(pageIndex, pageSize);
            return ResultModel.Success(list);
        }

        public async Task<IResultModel> GetSumAmountByPurchaseReturnOderIdAsync(int purchaseReturnOderId)
        {
            var data = _repository.Value.TableNoTracking;
            data = data.Where(p => p.PurchaseReturnOderId == purchaseReturnOderId);
            var sumAmount = await data.SumAsync(p => p.Amount);
            return ResultModel.Success(sumAmount);
        }

        public async Task<IResultModel> GetListAllByShopIdPurchaseReturnOderIdAsync(Guid shopId, int purchaseReturnOderId, bool isDescending = false)
        {
            var data = _repository.Value.TableNoTracking;
            data = data.Where(p => p.ShopId == shopId && p.PurchaseReturnOderId == purchaseReturnOderId);
            if (isDescending)
            {
                var Descendinglist = await data.OrderByDescending(k => k.Id).ProjectTo<PurchaseReturnOderItemDto>(_mapper.Value.ConfigurationProvider).ToListAsync();
                return ResultModel.Success(Descendinglist);
            }
            var list = await data.OrderBy(k => k.Id).ProjectTo<PurchaseReturnOderItemDto>(_mapper.Value.ConfigurationProvider).ToListAsync();
            return ResultModel.Success(list);
        }
    }

    public class CreatePurchaseReturnOderItemService : BaseService<PurchaseReturnOderItem, PurchaseReturnOderItemCreateDto, int>, ICreatePurchaseReturnOderItemService, IDependency
    {
        public CreatePurchaseReturnOderItemService(Lazy<IMapper> mapper, IUnitOfWork unitOfWork, ILogger<CreatePurchaseReturnOderItemService> logger,
            Lazy<IRepository<PurchaseReturnOderItem>> repository) : base(mapper, unitOfWork, logger, repository)
        {

        }
    }


    public class UpdatePurchaseReturnOderItemService : BaseService<PurchaseReturnOderItem, PurchaseReturnOderItemUpdateDto, int>, IUpdatePurchaseReturnOderItemService, IDependency
    {
        public UpdatePurchaseReturnOderItemService(Lazy<IMapper> mapper, IUnitOfWork unitOfWork, ILogger<UpdatePurchaseReturnOderItemService> logger, Lazy<IRepository<PurchaseReturnOderItem>> repository)
        : base(mapper, unitOfWork, logger, repository)
        {

        }
    }
}
