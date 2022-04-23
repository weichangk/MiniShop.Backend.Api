using MiniShop.Backend.Model.Dto;
using MiniShop.Backend.Model;
using Weick.Orm.Core.Result;
using System;
using System.Threading.Tasks;

namespace MiniShop.Backend.Api.Services
{
    public interface ISupplierService : IBaseService<Supplier, SupplierDto, int>
    {
        Task<IResultModel> GetByShopIdCodeAsync(Guid shopId, int code);

        Task<IResultModel> GetMaxCodeByShopIdAsync(Guid shopId);

        Task<IResultModel> GetByShopIdAsync(Guid shopId);
        
        Task<IResultModel> GetPageByShopIdAsync(int pageIndex, int pageSize, Guid shopId);

        Task<IResultModel> GetPageByShopIdWhereQueryAsync(int pageIndex, int pageSize, Guid shopId, string code, string name);
    }

    public interface ICreateSupplierService : IBaseService<Supplier, SupplierCreateDto, int>
    {

    }

    public interface IUpdateSupplierService : IBaseService<Supplier, SupplierUpdateDto, int>
    {

    }
}
