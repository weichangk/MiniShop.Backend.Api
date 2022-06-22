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
    public class PaymentService : BaseService<Payment, PaymentDto, int>, IPaymentService, IDependency
    {
        public PaymentService(Lazy<IMapper> mapper, IUnitOfWork unitOfWork, ILogger<PaymentService> logger,
            Lazy<IRepository<Payment>> _repository) : base(mapper, unitOfWork, logger, _repository)
        {

        }

        public async Task<IResultModel> GetByShopIdCodeAsync(Guid shopId, String code)
        {
            var data = _repository.Value.TableNoTracking.Where(s => s.ShopId == shopId && s.Code == code);
            var dto = await data.ProjectTo<PaymentDto>(_mapper.Value.ConfigurationProvider).FirstOrDefaultAsync();
            return ResultModel.Success(dto);
        }

        public async Task<IResultModel> GetPageByShopIdAsync(int pageIndex, int pageSize, Guid shopId)
        {
            var data = _repository.Value.TableNoTracking;
            data = data.Where(s => s.ShopId == shopId);
            var list = await data.ProjectTo<PaymentDto>(_mapper.Value.ConfigurationProvider).ToPagedListAsync(pageIndex, pageSize);
            return ResultModel.Success(list);
        }

        public async Task<IResultModel> GetPageByShopIdWhereQueryAsync(int pageIndex, int pageSize , Guid shopId, string code, string name)
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
            var list = await data.ProjectTo<PaymentDto>(_mapper.Value.ConfigurationProvider).ToPagedListAsync(pageIndex, pageSize);
            return ResultModel.Success(list);
        }

        public async Task<IResultModel> GetListAllByShopIdAsync(Guid shopId, bool isDescending = false)
        {
            var data = _repository.Value.TableNoTracking;
            data = data.Where(s => s.ShopId == shopId);
            if (isDescending)
            {
                var Descendinglist = await data.OrderByDescending(k => k.Id).ProjectTo<PaymentDto>(_mapper.Value.ConfigurationProvider).ToListAsync();
                return ResultModel.Success(Descendinglist);
            }
            var list = await data.OrderBy(k => k.Id).ProjectTo<PaymentDto>(_mapper.Value.ConfigurationProvider).ToListAsync();
            return ResultModel.Success(list);
        }
    }

    public class CreatePaymentService : BaseService<Payment, PaymentCreateDto, int>, ICreatePaymentService, IDependency
    {
        public CreatePaymentService(Lazy<IMapper> mapper, IUnitOfWork unitOfWork, ILogger<CreatePaymentService> logger,
            Lazy<IRepository<Payment>> repository) : base(mapper, unitOfWork, logger, repository)
        {

        }
    }


    public class UpdatePaymentService : BaseService<Payment, PaymentUpdateDto, int>, IUpdatePaymentService, IDependency
    {
        public UpdatePaymentService(Lazy<IMapper> mapper, IUnitOfWork unitOfWork, ILogger<UpdatePaymentService> logger, Lazy<IRepository<Payment>> repository)
        : base(mapper, unitOfWork, logger, repository)
        {

        }
    }
}
