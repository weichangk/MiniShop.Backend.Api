using MiniShop.Backend.Model.Dto;
using MiniShop.Backend.Model;
using Weick.Orm.Core.Result;
using System;
using System.Threading.Tasks;

namespace MiniShop.Backend.Api.Services
{
    public interface IVipService : IBaseService<Vip, VipDto, int>
    {
        Task<IResultModel> GetByShopIdCodeAsync(Guid shopId, string code);
        Task<IResultModel> GetPageByShopIdAsync(int pageIndex, int pageSize, Guid shopId);
        Task<IResultModel> GetPageByShopIdWhereQueryAsync(int pageIndex, int pageSize, Guid shopId, string code, string name, string phone);
    }

    public interface ICreateVipService : IBaseService<Vip, VipCreateDto, int>
    {
    }

    public interface IUpdateVipService : IBaseService<Vip, VipUpdateDto, int>
    {
    }
}
