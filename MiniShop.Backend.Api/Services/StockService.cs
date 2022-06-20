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
    public class StockService : BaseService<Stock, StockDto, int>, IStockService, IDependency
    {
        public StockService(Lazy<IMapper> mapper, IUnitOfWork unitOfWork, ILogger<StockService> logger,
            Lazy<IRepository<Stock>> _repository) : base(mapper, unitOfWork, logger, _repository)
        {

        }

        public async Task<IResultModel> GetByShopIdAndItemIdAsync(Guid shopId, int itemId)
        {
            var data = _repository.Value.TableNoTracking;
            data = data.Where(s => s.ShopId == shopId && s.Item.Id == itemId);
            var stock = await data.ProjectTo<StockDto>(_mapper.Value.ConfigurationProvider).FirstOrDefaultAsync();
            return ResultModel.Success(stock);
        }

        public async Task<IResultModel> GetPageByShopIdAsync(int pageIndex, int pageSize, Guid shopId)
        {
            var data = _repository.Value.TableNoTracking;
            data = data.Where(s => s.ShopId == shopId);
            var list = await data.ProjectTo<StockDto>(_mapper.Value.ConfigurationProvider).ToPagedListAsync(pageIndex, pageSize);
            return ResultModel.Success(list);
        }

        public async Task<IResultModel> GetPageByShopIdWhereQueryAsync(int pageIndex, int pageSize , Guid shopId, string code, string name)
        {
            var data = _repository.Value.TableNoTracking;
            data = data.Where(s => s.ShopId == shopId);

            code = System.Web.HttpUtility.UrlDecode(code);
            if (!string.IsNullOrEmpty(code))
            {
                data = data.Where(s => s.Item.Code.Contains(code));
            }

            name = System.Web.HttpUtility.UrlDecode(name);
            if (!string.IsNullOrEmpty(name))
            {
                data = data.Where(s => s.Item.Name != null && s.Item.Name.Contains(name));
            }
            var list = await data.ProjectTo<StockDto>(_mapper.Value.ConfigurationProvider).ToPagedListAsync(pageIndex, pageSize);
            return ResultModel.Success(list);
        }

    }

    public class CreateStockService : BaseService<Stock, StockCreateDto, int>, ICreateStockService, IDependency
    {
        public CreateStockService(Lazy<IMapper> mapper, IUnitOfWork unitOfWork, ILogger<CreateStockService> logger,
            Lazy<IRepository<Stock>> repository) : base(mapper, unitOfWork, logger, repository)
        {

        }
    }


    public class UpdateStockService : BaseService<Stock, StockUpdateDto, int>, IUpdateStockService, IDependency
    {
        public UpdateStockService(Lazy<IMapper> mapper, IUnitOfWork unitOfWork, ILogger<UpdateStockService> logger, Lazy<IRepository<Stock>> repository)
        : base(mapper, unitOfWork, logger, repository)
        {

        }
    }
}
