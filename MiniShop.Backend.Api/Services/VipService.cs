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
    public class VipService : BaseService<Vip, VipDto, int>, IVipService, IDependency
    {
        public VipService(Lazy<IMapper> mapper, IUnitOfWork unitOfWork, ILogger<VipService> logger,
            Lazy<IRepository<Vip>> _repository) : base(mapper, unitOfWork, logger, _repository)
        {

        }

        public async Task<IResultModel> GetByShopIdCodeAsync(Guid shopId, string code)
        {
            var data = _repository.Value.TableNoTracking.Where(s => s.ShopId == shopId && s.Code == code);
            var dto = await data.ProjectTo<VipDto>(_mapper.Value.ConfigurationProvider).FirstOrDefaultAsync();
            return ResultModel.Success(dto);
        }

        public async Task<IResultModel> GetPageByShopIdAsync(int pageIndex, int pageSize, Guid shopId)
        {
            var data = _repository.Value.TableNoTracking;
            data = data.Where(s => s.ShopId == shopId);
            var list = await data.ProjectTo<VipDto>(_mapper.Value.ConfigurationProvider).ToPagedListAsync(pageIndex, pageSize);
            return ResultModel.Success(list);
        }

        public async Task<IResultModel> GetPageByShopIdWhereQueryAsync(int pageIndex, int pageSize , Guid shopId, string code, string name, string phone)
        {
            var data = _repository.Value.TableNoTracking;
            data = data.Where(s => s.ShopId == shopId);

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
            phone = System.Web.HttpUtility.UrlDecode(phone);
            if (!string.IsNullOrEmpty(phone))
            {
                data = data.Where(s => s.Phone != null && s.Phone.Contains(phone));
            }
            var list = await data.ProjectTo<VipDto>(_mapper.Value.ConfigurationProvider).ToPagedListAsync(pageIndex, pageSize);
            return ResultModel.Success(list);
        }

    }

    public class CreateVipService : BaseService<Vip, VipCreateDto, int>, ICreateVipService, IDependency
    {
        public CreateVipService(Lazy<IMapper> mapper, IUnitOfWork unitOfWork, ILogger<CreateVipService> logger,
            Lazy<IRepository<Vip>> repository) : base(mapper, unitOfWork, logger, repository)
        {

        }
    }


    public class UpdateVipService : BaseService<Vip, VipUpdateDto, int>, IUpdateVipService, IDependency
    {
        public UpdateVipService(Lazy<IMapper> mapper, IUnitOfWork unitOfWork, ILogger<UpdateVipService> logger, Lazy<IRepository<Vip>> repository)
        : base(mapper, unitOfWork, logger, repository)
        {

        }
    }
}
