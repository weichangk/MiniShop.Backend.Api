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
    public class PurchaseOderItemService : BaseService<PurchaseOderItem, PurchaseOderItemDto, int>, IPurchaseOderItemService, IDependency
    {
        public PurchaseOderItemService(Lazy<IMapper> mapper, IUnitOfWork unitOfWork, ILogger<PurchaseOderItemService> logger,
            Lazy<IRepository<PurchaseOderItem>> _repository) : base(mapper, unitOfWork, logger, _repository)
        {

        }

        public async Task<IResultModel> GetByIdOnShopAsync(Guid shopId, int id)
        {
            var data = _repository.Value.TableNoTracking.Where(s => s.ShopId == shopId && s.Id == id);
            var dto = await data.ProjectTo<PurchaseOderItemDto>(_mapper.Value.ConfigurationProvider).FirstOrDefaultAsync();
            return ResultModel.Success(dto);
        }


        public async Task<IResultModel> GetPageByShopIdAsync(int pageIndex, int pageSize, Guid shopId)
        {
            var data = _repository.Value.TableNoTracking;
            data = data.Where(s => s.ShopId == shopId);
            var list = await data.ProjectTo<PurchaseOderItemDto>(_mapper.Value.ConfigurationProvider).ToPagedListAsync(pageIndex, pageSize);
            return ResultModel.Success(list);
        }

    }

    public class CreatePurchaseOderItemService : BaseService<PurchaseOderItem, PurchaseOderItemCreateDto, int>, ICreatePurchaseOderItemService, IDependency
    {
        public CreatePurchaseOderItemService(Lazy<IMapper> mapper, IUnitOfWork unitOfWork, ILogger<CreatePurchaseOderItemService> logger,
            Lazy<IRepository<PurchaseOderItem>> repository) : base(mapper, unitOfWork, logger, repository)
        {

        }
    }


    public class UpdatePurchaseOderItemService : BaseService<PurchaseOderItem, PurchaseOderItemUpdateDto, int>, IUpdatePurchaseOderItemService, IDependency
    {
        public UpdatePurchaseOderItemService(Lazy<IMapper> mapper, IUnitOfWork unitOfWork, ILogger<UpdatePurchaseOderItemService> logger, Lazy<IRepository<PurchaseOderItem>> repository)
        : base(mapper, unitOfWork, logger, repository)
        {

        }
    }
}
