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
    public class PromotionOderService : BaseService<PromotionOder, PromotionOderDto, int>, IPromotionOderService, IDependency
    {
        public PromotionOderService(Lazy<IMapper> mapper, IUnitOfWork unitOfWork, ILogger<PromotionOderService> logger,
            Lazy<IRepository<PromotionOder>> _repository) : base(mapper, unitOfWork, logger, _repository)
        {

        }

        public async Task<IResultModel> GetByShopIdOderNoAsync(Guid shopId, string oderNo)
        {
            var data = _repository.Value.TableNoTracking.Where(s => s.ShopId == shopId && s.OderNo == oderNo);
            var dto = await data.ProjectTo<PromotionOderDto>(_mapper.Value.ConfigurationProvider).FirstOrDefaultAsync();
            return ResultModel.Success(dto);
        }


        public async Task<IResultModel> GetPageByShopIdAsync(int pageIndex, int pageSize, Guid shopId)
        {
            var data = _repository.Value.TableNoTracking;
            data = data.Where(s => s.ShopId == shopId);
            var list = await data.ProjectTo<PromotionOderDto>(_mapper.Value.ConfigurationProvider).ToPagedListAsync(pageIndex, pageSize);
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
            var list = await data.ProjectTo<PromotionOderDto>(_mapper.Value.ConfigurationProvider).ToPagedListAsync(pageIndex, pageSize);
            return ResultModel.Success(list);
        }

    }

    public class CreatePromotionOderService : BaseService<PromotionOder, PromotionOderCreateDto, int>, ICreatePromotionOderService, IDependency
    {
        public CreatePromotionOderService(Lazy<IMapper> mapper, IUnitOfWork unitOfWork, ILogger<CreatePromotionOderService> logger,
            Lazy<IRepository<PromotionOder>> repository) : base(mapper, unitOfWork, logger, repository)
        {

        }
    }

    public class AuditPromotionOderService : BaseService<PromotionOder, PromotionOderAuditDto, int>, IAuditPromotionOderService, IDependency
    {
        public AuditPromotionOderService(Lazy<IMapper> mapper, IUnitOfWork unitOfWork, ILogger<AuditPromotionOderService> logger, Lazy<IRepository<PromotionOder>> repository)
        : base(mapper, unitOfWork, logger, repository)
        {

        }

        public async Task<IResultModel> AuditPromotionOderStatusAsync(int id, EnumAuditStatus state)
        {
            var entity = await _repository.Value.GetByIdAsync(id);
            if (entity == null)
            {
                _logger.LogError($"error：entity Id {id} does not exist");
                return ResultModel.NotExists;
            }
            entity.AuditState = state;
            _repository.Value.Update(entity);

            if (await UnitOfWork.SaveChangesAsync() > 0)
            {
                return ResultModel.Success(true);
            }
            _logger.LogError($"error：Update Save failed");
            return ResultModel.Failed("error：Update Save failed", 500);
        }
    }
}
