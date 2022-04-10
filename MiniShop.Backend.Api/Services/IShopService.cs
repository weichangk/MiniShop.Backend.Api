using MiniShop.Backend.Model.Dto;
using MiniShop.Backend.Model;
using Weick.Orm.Core.Result;
using System;
using System.Threading.Tasks;

namespace MiniShop.Backend.Api.Services
{
    public interface IShopService : IBaseService<Shop, ShopDto, int>
    {
        Task<IResultModel> GetByShopIdAsync(Guid shopId); 
    }

    public interface IShopCreateService : IBaseService<Shop, ShopCreateDto, int>
    { 

    }

    public interface IShopUpdateService : IBaseService<Shop, ShopUpdateDto, int>
    {

    }
}
