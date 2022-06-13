using MiniShop.Backend.Model.Dto;
using MiniShop.Backend.Model;
using Weick.Orm.Core.Result;
using System;
using System.Threading.Tasks;

namespace MiniShop.Backend.Api.Services
{
    public interface ICategorieService : IBaseService<Categorie, CategorieDto, int>
    {
        Task<IResultModel> GetByShopIdCodeAsync(Guid shopId, int code);

        Task<IResultModel> GetMaxCodeByShopIdLevelAsync(Guid shopId, int level);

        Task<IResultModel> GetPageByShopIdAsync(int pageIndex, int pageSize, Guid shopId);

        Task<IResultModel> GetPageByShopIdWhereQueryAsync(int pageIndex, int pageSize, Guid shopId, string code, string name);

        Task<IResultModel> GetListAllByShopIdAsync(Guid shopId, bool isDescending = false);
    }

    public interface ICreateCategorieService : IBaseService<Categorie, CategorieCreateDto, int>
    {

    }

    public interface IUpdateCategorieService : IBaseService<Categorie, CategorieUpdateDto, int>
    {

    }
}
