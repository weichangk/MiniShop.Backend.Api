using MiniShop.Backend.Model.Dto;
using MiniShop.Backend.Model;
using Weick.Orm.Core.Result;
using System;
using System.Threading.Tasks;
using MiniShop.Backend.Model.Enums;

namespace MiniShop.Backend.Api.Services
{
    public interface IVipScoreSettingService : IBaseService<VipScoreSetting, VipScoreSettingDto, int>
    {
        Task<IResultModel> GetByShopIdVipTypeIdVipScoreWayAsync(Guid shopId, int vipTypeId, EnumVipScoreWay vipScoreWay);
        Task<IResultModel> GetPageByShopIdAsync(int pageIndex, int pageSize, Guid shopId);
    }

    public interface ICreateVipScoreSettingService : IBaseService<VipScoreSetting, VipScoreSettingCreateDto, int>
    {
    }

    public interface IUpdateVipScoreSettingService : IBaseService<VipScoreSetting, VipScoreSettingUpdateDto, int>
    {
    }
}
