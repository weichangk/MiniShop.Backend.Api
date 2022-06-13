using MiniShop.Backend.Model.Dto;
using MiniShop.Backend.Model;
using Weick.Orm.Core.Result;
using System;
using System.Threading.Tasks;

namespace MiniShop.Backend.Api.Services
{
    public interface IItemService : IBaseService<Item, ItemDto, int>
    {
        Task<IResultModel> GetByShopIdCodeAsync(Guid shopId, string code);

        Task<IResultModel> GetPageByShopIdAsync(int pageIndex, int pageSize, Guid shopId);

        Task<IResultModel> GetPageByShopIdWhereQueryAsync(int pageIndex, int pageSize, Guid shopId, string code, string name);

        Task<IResultModel> GetListAllByShopIdAsync(Guid shopId, bool isDescending = false);
    }

    public interface ICreateItemService : IBaseService<Item, ItemCreateDto, int>
    {

    }

    public interface IUpdateItemService : IBaseService<Item, ItemUpdateDto, int>
    {

    }
}
