using MiniShop.Backend.Model.Dto;
using MiniShop.Backend.Model;
using Weick.Orm.Core.Result;
using System;
using System.Threading.Tasks;

namespace MiniShop.Backend.Api.Services
{
    public interface IUnitService : IBaseService<Unit, UnitDto, int>
    {
        Task<IResultModel> GetByShopIdCodeAsync(Guid shopId, int code);

        Task<IResultModel> GetMaxCodeByShopIdAsync(Guid shopId);

        Task<IResultModel> GetPageByShopIdAsync(int pageIndex, int pageSize, Guid shopId);

        Task<IResultModel> GetPageByShopIdWhereQueryAsync(int pageIndex, int pageSize, Guid shopId, string code, string name);

        Task<IResultModel> GetListAllByShopIdAsync(Guid shopId, bool isDescending = false);
    }

    public interface ICreateUnitService : IBaseService<Unit, UnitCreateDto, int>
    {

    }

    public interface IUpdateUnitService : IBaseService<Unit, UnitUpdateDto, int>
    {

    }
}
