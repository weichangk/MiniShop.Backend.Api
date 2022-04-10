using MiniShop.Backend.Model.Dto;
using MiniShop.Backend.Model;
using System;
using System.Threading.Tasks;
using Weick.Orm.Core.Result;

namespace MiniShop.Backend.Api.Services
{
    public interface IStoreService : IBaseService<Store, StoreDto, int>
    {
        Task<IResultModel> GetByStoreIdAsync(Guid storeId);

        Task<IResultModel> GetByShopIdAsync(Guid shopId);

        Task<IResultModel> GetPageByShopIdAsync(int pageIndex, int pageSize, Guid shopId);

        Task<IResultModel> GetPageByShopIdWhereQueryAsync(int pageIndex, int pageSize, Guid shopId, string name);
    }

    public interface ICreateStoreService : IBaseService<Store, StoreCreateDto, int>
    {

    }

    public interface IUpdateStoreService : IBaseService<Store, StoreUpdateDto, int>
    {

    }
}
