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
    public class BillInfoService : BaseService<BillInfo, BillInfoDto, int>, IBillInfoService, IDependency
    {
        public BillInfoService(Lazy<IMapper> mapper, IUnitOfWork unitOfWork, ILogger<BillInfoService> logger,
            Lazy<IRepository<BillInfo>> _repository) : base(mapper, unitOfWork, logger, _repository)
        {

        }

        public async Task<IResultModel> GetByShopIdAndBillNoAsync(Guid shopId, string billNo)
        {
            var data = _repository.Value.TableNoTracking;
            data = data.Where(s => s.ShopId == shopId && s.BillNo == billNo);
            var model = await data.ProjectTo<BillInfoDto>(_mapper.Value.ConfigurationProvider).FirstOrDefaultAsync();
            return ResultModel.Success(model);
        }

        public async Task<IResultModel> GetPageByShopIdAsync(int pageIndex, int pageSize, Guid shopId)
        {
            var data = _repository.Value.TableNoTracking;
            data = data.Where(s => s.ShopId == shopId);
            var list = await data.ProjectTo<BillInfoDto>(_mapper.Value.ConfigurationProvider).ToPagedListAsync(pageIndex, pageSize);
            return ResultModel.Success(list);
        }

    }

    public class CreateBillInfoService : BaseService<BillInfo, BillInfoCreateDto, int>, ICreateBillInfoService, IDependency
    {
        public CreateBillInfoService(Lazy<IMapper> mapper, IUnitOfWork unitOfWork, ILogger<CreateBillInfoService> logger,
            Lazy<IRepository<BillInfo>> repository) : base(mapper, unitOfWork, logger, repository)
        {

        }
    }

}
