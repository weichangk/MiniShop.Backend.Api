using MiniShop.Backend.Model.Dto;
using MiniShop.Backend.Model;
using Weick.Orm.Core.Result;
using System;
using System.Threading.Tasks;

namespace MiniShop.Backend.Api.Services
{
    public interface IPosRegisterService : IBaseService<PosRegister, PosRegisterDto, int>
    {
        Task<IResultModel> GetByCodeAsync(string code);

        Task<IResultModel> GetPageAsync(int pageIndex, int pageSize);

        Task<IResultModel> GetPageWhereQueryAsync(int pageIndex, int pageSize, string code, string name);
    }

    public interface ICreatePosRegisterService : IBaseService<PosRegister, PosRegisterCreateDto, int>
    {

    }

    public interface IUpdatePosRegisterService : IBaseService<PosRegister, PosRegisterUpdateDto, int>
    {

    }
}
