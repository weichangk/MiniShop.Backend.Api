using AutoMapper;
using Microsoft.Extensions.Logging;
using MiniShop.Backend.Model.Dto;
using MiniShop.Backend.Model;
using System;
using Weick.Orm.Core;
using System.Threading.Tasks;
using Weick.Orm.Core.Result;
using System.Linq;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace MiniShop.Backend.Api.Services
{
    public class ShopService : BaseService<Shop, ShopDto, int>, IShopService, IDependency
    {
        public ShopService(Lazy<IMapper> mapper, IUnitOfWork unitOfWork, ILogger<ShopService> logger,
            Lazy<IRepository<Shop>> _repository) : base(mapper, unitOfWork, logger, _repository)
        {

        }

        public async Task<IResultModel> GetByShopIdAsync(Guid shopId)
        {
            var data = _repository.Value.TableNoTracking.Where(s => s.ShopId == shopId);
            var shopDto = await data.ProjectTo<ShopDto>(_mapper.Value.ConfigurationProvider).FirstOrDefaultAsync();
            return ResultModel.Success(shopDto);
        }
    }

    public class ShopCreateService : BaseService<Shop, ShopCreateDto, int>, IShopCreateService, IDependency
    {
        public ShopCreateService(Lazy<IMapper> mapper, IUnitOfWork unitOfWork, ILogger<ShopCreateService> logger,
            Lazy<IRepository<Shop>> _repository) : base(mapper, unitOfWork, logger, _repository)
        {

        }
    }

    public class ShopUpdateService : BaseService<Shop, ShopUpdateDto, int>, IShopUpdateService, IDependency
    {
        public ShopUpdateService(Lazy<IMapper> mapper, IUnitOfWork unitOfWork, ILogger<ShopUpdateService> logger,
            Lazy<IRepository<Shop>> _repository) : base(mapper, unitOfWork, logger, _repository)
        {

        }
    }
}
