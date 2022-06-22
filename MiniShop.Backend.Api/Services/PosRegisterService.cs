using AutoMapper;
using Microsoft.Extensions.Logging;
using MiniShop.Backend.Model.Dto;
using MiniShop.Backend.Model;
using System;
using Weick.Orm.Core;
using System.Threading.Tasks;
using Weick.Orm.Core.Result;
using AutoMapper.QueryableExtensions;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MiniShop.Backend.Api.Services
{
    public class PosRegisterService : BaseService<PosRegister, PosRegisterDto, int>, IPosRegisterService, IDependency
    {
        public PosRegisterService(Lazy<IMapper> mapper, IUnitOfWork unitOfWork, ILogger<PosRegisterService> logger,
            Lazy<IRepository<PosRegister>> _repository) : base(mapper, unitOfWork, logger, _repository)
        {

        }

        public async Task<IResultModel> GetByCodeAsync(String code)
        {
            var data = _repository.Value.TableNoTracking.Where(s => s.Code == code);
            var dto = await data.ProjectTo<PosRegisterDto>(_mapper.Value.ConfigurationProvider).FirstOrDefaultAsync();
            return ResultModel.Success(dto);
        }

        public async Task<IResultModel> GetPageAsync(int pageIndex, int pageSize)
        {
            var data = _repository.Value.TableNoTracking;
            var list = await data.ProjectTo<PosRegisterDto>(_mapper.Value.ConfigurationProvider).ToPagedListAsync(pageIndex, pageSize);
            return ResultModel.Success(list);
        }

        public async Task<IResultModel> GetPageWhereQueryAsync(int pageIndex, int pageSize, string code, string name)
        {
            var data = _repository.Value.TableNoTracking;

            code = System.Web.HttpUtility.UrlDecode(code);
            if (!string.IsNullOrEmpty(code))
            {
                data = data.Where(s => s.Code.Contains(code));
            }
            name = System.Web.HttpUtility.UrlDecode(name);
            if (!string.IsNullOrEmpty(name))
            {
                data = data.Where(s => s.Name != null && s.Name.Contains(name));
            }
            var list = await data.ProjectTo<PosRegisterDto>(_mapper.Value.ConfigurationProvider).ToPagedListAsync(pageIndex, pageSize);
            return ResultModel.Success(list);
        }
    }

    public class CreatePosRegisterService : BaseService<PosRegister, PosRegisterCreateDto, int>, ICreatePosRegisterService, IDependency
    {
        public CreatePosRegisterService(Lazy<IMapper> mapper, IUnitOfWork unitOfWork, ILogger<CreatePosRegisterService> logger,
            Lazy<IRepository<PosRegister>> repository) : base(mapper, unitOfWork, logger, repository)
        {

        }
    }


    public class UpdatePosRegisterService : BaseService<PosRegister, PosRegisterUpdateDto, int>, IUpdatePosRegisterService, IDependency
    {
        public UpdatePosRegisterService(Lazy<IMapper> mapper, IUnitOfWork unitOfWork, ILogger<UpdatePosRegisterService> logger, Lazy<IRepository<PosRegister>> repository)
        : base(mapper, unitOfWork, logger, repository)
        {

        }
    }
}
