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
    public class SaleFlowService : BaseService<SaleFlow, SaleFlowDto, int>, ISaleFlowService, IDependency
    {
        public SaleFlowService(Lazy<IMapper> mapper, IUnitOfWork unitOfWork, ILogger<SaleFlowService> logger,
            Lazy<IRepository<SaleFlow>> _repository) : base(mapper, unitOfWork, logger, _repository)
        {

        }

        public async Task<IResultModel> GetPageByShopIdAsync(int pageIndex, int pageSize, Guid shopId)
        {
            var data = _repository.Value.TableNoTracking;
            data = data.Where(s => s.ShopId == shopId);
            var list = await data.ProjectTo<SaleFlowDto>(_mapper.Value.ConfigurationProvider).ToPagedListAsync(pageIndex, pageSize);
            return ResultModel.Success(list);
        }

        public async Task<IResultModel> GetPageByShopIdWhereQueryAsync(int pageIndex, int pageSize, Guid shopId, DateTime? startTime, DateTime? endTime)
        {
            var data = _repository.Value.TableNoTracking;
            data = data.Where(s => s.ShopId == shopId);

            if(startTime != null)
            {
                data = data.Where(s => s.CreatedTime >= startTime);
            }
            if(endTime != null)
            {
                data = data.Where(s => s.CreatedTime <= endTime);
            }

            var list = await data.ProjectTo<SaleFlowDto>(_mapper.Value.ConfigurationProvider).ToPagedListAsync(pageIndex, pageSize);
            return ResultModel.Success(list);
        }
    }

    public class CreateSaleFlowService : BaseService<SaleFlow, SaleFlowCreateDto, int>, ICreateSaleFlowService, IDependency
    {
        public CreateSaleFlowService(Lazy<IMapper> mapper, IUnitOfWork unitOfWork, ILogger<CreateSaleFlowService> logger,
            Lazy<IRepository<SaleFlow>> repository) : base(mapper, unitOfWork, logger, repository)
        {

        }
    }

}
