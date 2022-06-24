using MiniShop.Backend.Model.Dto;
using MiniShop.Backend.Model;
using Weick.Orm.Core.Result;
using System;
using System.Threading.Tasks;

namespace MiniShop.Backend.Api.Services
{
    public interface IBillInfoService : IBaseService<BillInfo, BillInfoDto, int>
    {
        Task<IResultModel> GetByShopIdAndBillNoAsync(Guid shopId, string billNo);
        Task<IResultModel> GetPageByShopIdAsync(int pageIndex, int pageSize, Guid shopId);
    }

    public interface ICreateBillInfoService : IBaseService<BillInfo, BillInfoCreateDto, int>
    {
    }

}
