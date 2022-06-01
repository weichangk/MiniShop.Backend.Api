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
using MiniShop.Backend.Model.Enums;

namespace MiniShop.Backend.Api.Services
{
    public class PurchaseReceiveOderService : BaseService<PurchaseReceiveOder, PurchaseReceiveOderDto, int>, IPurchaseReceiveOderService, IDependency
    {
        public PurchaseReceiveOderService(Lazy<IMapper> mapper, IUnitOfWork unitOfWork, ILogger<PurchaseReceiveOderService> logger,
            Lazy<IRepository<PurchaseReceiveOder>> _repository) : base(mapper, unitOfWork, logger, _repository)
        {

        }

        public async Task<IResultModel> GetByShopIdOderNoAsync(Guid shopId, string oderNo)
        {
            var data = _repository.Value.TableNoTracking.Where(s => s.ShopId == shopId && s.OderNo == oderNo);
            var dto = await data.ProjectTo<PurchaseReceiveOderDto>(_mapper.Value.ConfigurationProvider).FirstOrDefaultAsync();
            return ResultModel.Success(dto);
        }


        public async Task<IResultModel> GetPageByShopIdAsync(int pageIndex, int pageSize, Guid shopId)
        {
            var data = _repository.Value.TableNoTracking;
            data = data.Where(s => s.ShopId == shopId);
            var list = await data.ProjectTo<PurchaseReceiveOderDto>(_mapper.Value.ConfigurationProvider).ToPagedListAsync(pageIndex, pageSize);
            return ResultModel.Success(list);
        }

        public async Task<IResultModel> GetPageByShopIdWhereQueryAsync(int pageIndex, int pageSize , Guid shopId, string oderNo)
        {
            var data = _repository.Value.TableNoTracking;
            data = data.Where(s => s.ShopId == shopId);

            oderNo = System.Web.HttpUtility.UrlDecode(oderNo);
            if (!string.IsNullOrEmpty(oderNo))
            {
                data = data.Where(s => s.OderNo != null && s.OderNo.Contains(oderNo));
            }
            var list = await data.ProjectTo<PurchaseReceiveOderDto>(_mapper.Value.ConfigurationProvider).ToPagedListAsync(pageIndex, pageSize);
            return ResultModel.Success(list);
        }

        public async Task<IResultModel> GetAuditedUnReturnedPageByShopIdAsync(int pageIndex, int pageSize, Guid shopId)
        {
            var data = _repository.Value.TableNoTracking;
            data = data.Where(p => p.ShopId == shopId && p.AuditState == EnumAuditStatus.Audited && (p.OrderState == EnumPurchaseOrderStatus.UnReturned || p.OrderState == EnumPurchaseOrderStatus.PartReturned));
            var list = await data.ProjectTo<PurchaseReceiveOderDto>(_mapper.Value.ConfigurationProvider).ToPagedListAsync(pageIndex, pageSize);
            return ResultModel.Success(list);
        }

        public async Task<IResultModel> GetAuditedUnReturnedPageByShopIdWhereQueryAsync(int pageIndex, int pageSize , Guid shopId, string oderNo)
        {
            var data = _repository.Value.TableNoTracking;
            data = data.Where(p => p.ShopId == shopId && p.AuditState == EnumAuditStatus.Audited && (p.OrderState == EnumPurchaseOrderStatus.UnReturned || p.OrderState == EnumPurchaseOrderStatus.PartReturned));

            oderNo = System.Web.HttpUtility.UrlDecode(oderNo);
            if (!string.IsNullOrEmpty(oderNo))
            {
                data = data.Where(s => s.OderNo != null && s.OderNo.Contains(oderNo));
            }
            var list = await data.ProjectTo<PurchaseReceiveOderDto>(_mapper.Value.ConfigurationProvider).ToPagedListAsync(pageIndex, pageSize);
            return ResultModel.Success(list);
        }
    }

    public class CreatePurchaseReceiveOderService : BaseService<PurchaseReceiveOder, PurchaseReceiveOderCreateDto, int>, ICreatePurchaseReceiveOderService, IDependency
    {
        public CreatePurchaseReceiveOderService(Lazy<IMapper> mapper, IUnitOfWork unitOfWork, ILogger<CreatePurchaseReceiveOderService> logger,
            Lazy<IRepository<PurchaseReceiveOder>> repository) : base(mapper, unitOfWork, logger, repository)
        {

        }
    }

    public class UpdatePurchaseReceiveOderService : BaseService<PurchaseReceiveOder, PurchaseReceiveOderUpdateDto, int>, IUpdatePurchaseReceiveOderService, IDependency
    {
        public UpdatePurchaseReceiveOderService(Lazy<IMapper> mapper, IUnitOfWork unitOfWork, ILogger<UpdatePurchaseReceiveOderService> logger, Lazy<IRepository<PurchaseReceiveOder>> repository)
        : base(mapper, unitOfWork, logger, repository)
        {

        }

        public async Task<IResultModel> UpdateReceiveOderAmountAsync(int id, decimal oderAmount)
        {
            var entity = await _repository.Value.GetByIdAsync(id);
            if (entity == null)
            {
                _logger.LogError($"error：entity Id {id} does not exist");
                return ResultModel.NotExists;
            }
            entity.OderAmount = oderAmount;
            _repository.Value.Update(entity);

            if (await UnitOfWork.SaveChangesAsync() > 0)
            {
                return ResultModel.Success(entity.OderAmount);
            }
            _logger.LogError($"error：Update Save failed");
            return ResultModel.Failed("error：Update Save failed", 500);
        }
    }

}
