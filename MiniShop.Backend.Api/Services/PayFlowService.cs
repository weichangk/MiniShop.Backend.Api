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
    public class PayFlowService : BaseService<PayFlow, PayFlowDto, int>, IPayFlowService, IDependency
    {
        public PayFlowService(Lazy<IMapper> mapper, IUnitOfWork unitOfWork, ILogger<PayFlowService> logger,
            Lazy<IRepository<PayFlow>> _repository) : base(mapper, unitOfWork, logger, _repository)
        {

        }

        public async Task<IResultModel> GetPageByShopIdAsync(int pageIndex, int pageSize, Guid shopId)
        {
            var data = _repository.Value.TableNoTracking;
            data = data.Where(s => s.ShopId == shopId);
            var list = await data.ProjectTo<PayFlowDto>(_mapper.Value.ConfigurationProvider).ToPagedListAsync(pageIndex, pageSize);
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

            var list = await data.ProjectTo<PayFlowDto>(_mapper.Value.ConfigurationProvider).ToPagedListAsync(pageIndex, pageSize);
            return ResultModel.Success(list);
        }
    }

    public class CreatePayFlowService : BaseService<PayFlow, PayFlowCreateDto, int>, ICreatePayFlowService, IDependency
    {
        public CreatePayFlowService(Lazy<IMapper> mapper, IUnitOfWork unitOfWork, ILogger<CreatePayFlowService> logger,
            Lazy<IRepository<PayFlow>> repository) : base(mapper, unitOfWork, logger, repository)
        {

        }
    }

}
