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
    public class PurchaseOderService : BaseService<PurchaseOder, PurchaseOderDto, int>, IPurchaseOderService, IDependency
    {
        public PurchaseOderService(Lazy<IMapper> mapper, IUnitOfWork unitOfWork, ILogger<PurchaseOderService> logger,
            Lazy<IRepository<PurchaseOder>> _repository) : base(mapper, unitOfWork, logger, _repository)
        {

        }

        public async Task<IResultModel> GetByShopIdOderNoAsync(Guid shopId, string oderNo)
        {
            var data = _repository.Value.TableNoTracking.Where(s => s.ShopId == shopId && s.OderNo == oderNo);
            var dto = await data.ProjectTo<PurchaseOderDto>(_mapper.Value.ConfigurationProvider).FirstOrDefaultAsync();
            return ResultModel.Success(dto);
        }


        public async Task<IResultModel> GetPageByShopIdAsync(int pageIndex, int pageSize, Guid shopId)
        {
            var data = _repository.Value.TableNoTracking;
            data = data.Where(s => s.ShopId == shopId);
            var list = await data.ProjectTo<PurchaseOderDto>(_mapper.Value.ConfigurationProvider).ToPagedListAsync(pageIndex, pageSize);
            return ResultModel.Success(list);
        }

        public async Task<IResultModel> GetAuditedUnReceivedPageByShopIdAsync(int pageIndex, int pageSize, Guid shopId)
        {
            var data = _repository.Value.TableNoTracking;
            data = data.Where(p => p.ShopId == shopId && p.AuditState == EnumAuditStatus.Audited && (p.OrderState == EnumPurchaseOrderStatus.PartReturned || p.OrderState == EnumPurchaseOrderStatus.UnReceived));
            var list = await data.ProjectTo<PurchaseOderDto>(_mapper.Value.ConfigurationProvider).ToPagedListAsync(pageIndex, pageSize);
            return ResultModel.Success(list);
        }

        public async Task<IResultModel> GetAuditedUnReturnPageByShopIdAsync(int pageIndex, int pageSize, Guid shopId)
        {
            var data = _repository.Value.TableNoTracking;
            data = data.Where(p => p.ShopId == shopId && p.AuditState == EnumAuditStatus.Audited && (p.OrderState == EnumPurchaseOrderStatus.Received));
            var list = await data.ProjectTo<PurchaseOderDto>(_mapper.Value.ConfigurationProvider).ToPagedListAsync(pageIndex, pageSize);
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
            var list = await data.ProjectTo<PurchaseOderDto>(_mapper.Value.ConfigurationProvider).ToPagedListAsync(pageIndex, pageSize);
            return ResultModel.Success(list);
        }

        public async Task<IResultModel> GetAuditedUnReceivedPageByShopIdWhereQueryAsync(int pageIndex, int pageSize , Guid shopId, string oderNo)
        {
            var data = _repository.Value.TableNoTracking;
            data = data.Where(p => p.ShopId == shopId && p.AuditState == EnumAuditStatus.Audited && (p.OrderState == EnumPurchaseOrderStatus.PartReturned || p.OrderState == EnumPurchaseOrderStatus.UnReceived));

            oderNo = System.Web.HttpUtility.UrlDecode(oderNo);
            if (!string.IsNullOrEmpty(oderNo))
            {
                data = data.Where(s => s.OderNo != null && s.OderNo.Contains(oderNo));
            }
            var list = await data.ProjectTo<PurchaseOderDto>(_mapper.Value.ConfigurationProvider).ToPagedListAsync(pageIndex, pageSize);
            return ResultModel.Success(list);
        }

        public async Task<IResultModel> GetAuditedUnReturnPageByShopIdWhereQueryAsync(int pageIndex, int pageSize , Guid shopId, string oderNo)
        {
            var data = _repository.Value.TableNoTracking;
            data = data.Where(p => p.ShopId == shopId && p.AuditState == EnumAuditStatus.Audited && (p.OrderState == EnumPurchaseOrderStatus.Received));

            oderNo = System.Web.HttpUtility.UrlDecode(oderNo);
            if (!string.IsNullOrEmpty(oderNo))
            {
                data = data.Where(s => s.OderNo != null && s.OderNo.Contains(oderNo));
            }
            var list = await data.ProjectTo<PurchaseOderDto>(_mapper.Value.ConfigurationProvider).ToPagedListAsync(pageIndex, pageSize);
            return ResultModel.Success(list);
        }
    }

    public class CreatePurchaseOderService : BaseService<PurchaseOder, PurchaseOderCreateDto, int>, ICreatePurchaseOderService, IDependency
    {
        public CreatePurchaseOderService(Lazy<IMapper> mapper, IUnitOfWork unitOfWork, ILogger<CreatePurchaseOderService> logger,
            Lazy<IRepository<PurchaseOder>> repository) : base(mapper, unitOfWork, logger, repository)
        {

        }
    }


    public class UpdatePurchaseOderService : BaseService<PurchaseOder, PurchaseOderUpdateDto, int>, IUpdatePurchaseOderService, IDependency
    {
        public UpdatePurchaseOderService(Lazy<IMapper> mapper, IUnitOfWork unitOfWork, ILogger<UpdatePurchaseOderService> logger, Lazy<IRepository<PurchaseOder>> repository)
        : base(mapper, unitOfWork, logger, repository)
        {

        }

        public async Task<IResultModel> UpdateOderAmountAsync(int id, decimal oderAmount)
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

        public async Task<IResultModel> UpdatePurchaseOderStatusAsync(int id, EnumPurchaseOrderStatus state)
        {
            var entity = await _repository.Value.GetByIdAsync(id);
            if (entity == null)
            {
                _logger.LogError($"error：entity Id {id} does not exist");
                return ResultModel.NotExists;
            }
            entity.OrderState = state;
            _repository.Value.Update(entity);

            if (await UnitOfWork.SaveChangesAsync() > 0)
            {
                return ResultModel.Success(true);
            }
            _logger.LogError($"error：Update Save failed");
            return ResultModel.Failed("error：Update Save failed", 500);
        }
    }

    public class AuditPurchaseOderService : BaseService<PurchaseOder, PurchaseOderAuditDto, int>, IAuditPurchaseOderService, IDependency
    {
        public AuditPurchaseOderService(Lazy<IMapper> mapper, IUnitOfWork unitOfWork, ILogger<AuditPurchaseOderService> logger, Lazy<IRepository<PurchaseOder>> repository)
        : base(mapper, unitOfWork, logger, repository)
        {

        }
    }
}
