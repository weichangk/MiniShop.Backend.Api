using MiniShop.Backend.Model.Dto;
using MiniShop.Backend.Model;
using Weick.Orm.Core.Result;
using System;
using System.Threading.Tasks;

namespace MiniShop.Backend.Api.Services
{
    public interface IStockService : IBaseService<Stock, StockDto, int>
    {
        Task<IResultModel> GetByShopIdAndItemIdAsync(Guid shopId, int itemId);
        Task<IResultModel> GetPageByShopIdAsync(int pageIndex, int pageSize, Guid shopId);

        Task<IResultModel> GetPageByShopIdWhereQueryAsync(int pageIndex, int pageSize, Guid shopId, string code, string name);
    }

    public interface ICreateStockService : IBaseService<Stock, StockCreateDto, int>
    {
    }

    public interface IUpdateStockService : IBaseService<Stock, StockUpdateDto, int>
    {
    }
}
