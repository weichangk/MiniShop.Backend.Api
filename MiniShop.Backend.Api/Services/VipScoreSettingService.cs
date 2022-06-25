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
    public class VipScoreSettingService : BaseService<VipScoreSetting, VipScoreSettingDto, int>, IVipScoreSettingService, IDependency
    {
        public VipScoreSettingService(Lazy<IMapper> mapper, IUnitOfWork unitOfWork, ILogger<VipScoreSettingService> logger,
            Lazy<IRepository<VipScoreSetting>> _repository) : base(mapper, unitOfWork, logger, _repository)
        {

        }

        public async Task<IResultModel> GetByShopIdVipTypeIdVipScoreWayAsync(Guid shopId, int vipTypeId, EnumVipScoreWay vipScoreWay)
        {
            var data = _repository.Value.TableNoTracking;
            data = data.Where(s => s.ShopId == shopId && s.VipTypeId == vipTypeId && s.VipScoreWay == vipScoreWay);
            var dto = await data.ProjectTo<VipScoreSettingDto>(_mapper.Value.ConfigurationProvider).FirstOrDefaultAsync();
            return ResultModel.Success(dto);
        }

        public async Task<IResultModel> GetPageByShopIdAsync(int pageIndex, int pageSize, Guid shopId)
        {
            var data = _repository.Value.TableNoTracking;
            data = data.Where(s => s.ShopId == shopId);
            var list = await data.ProjectTo<VipScoreSettingDto>(_mapper.Value.ConfigurationProvider).ToPagedListAsync(pageIndex, pageSize);
            return ResultModel.Success(list);
        }

    }

    public class CreateVipScoreSettingService : BaseService<VipScoreSetting, VipScoreSettingCreateDto, int>, ICreateVipScoreSettingService, IDependency
    {
        public CreateVipScoreSettingService(Lazy<IMapper> mapper, IUnitOfWork unitOfWork, ILogger<CreateVipScoreSettingService> logger,
            Lazy<IRepository<VipScoreSetting>> repository) : base(mapper, unitOfWork, logger, repository)
        {

        }
    }


    public class UpdateVipScoreSettingService : BaseService<VipScoreSetting, VipScoreSettingUpdateDto, int>, IUpdateVipScoreSettingService, IDependency
    {
        public UpdateVipScoreSettingService(Lazy<IMapper> mapper, IUnitOfWork unitOfWork, ILogger<UpdateVipScoreSettingService> logger, Lazy<IRepository<VipScoreSetting>> repository)
        : base(mapper, unitOfWork, logger, repository)
        {

        }
    }
}
