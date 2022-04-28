﻿using AutoMapper;
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
    public class PurchaseOderService : BaseService<PurchaseOder, PurchaseOderDto, int>, IPurchaseOderService, IDependency
    {
        public PurchaseOderService(Lazy<IMapper> mapper, IUnitOfWork unitOfWork, ILogger<PurchaseOderService> logger,
            Lazy<IRepository<PurchaseOder>> _repository) : base(mapper, unitOfWork, logger, _repository)
        {

        }

        public async Task<IResultModel> GetByShopIdOderNoAsync(Guid shopId, string oderNo)
        {
            var data = _repository.Value.TableNoTracking.Where(s => s.ShopId == shopId && s.OderNo == oderNo);
            var dto = await data.ProjectTo<PurchaseOderDto>(_mapper.Value.ConfigurationProvider).FirstOrDefaultAsync();
            return ResultModel.Success(dto);
        }


        public async Task<IResultModel> GetPageByShopIdAsync(int pageIndex, int pageSize, Guid shopId)
        {
            var data = _repository.Value.TableNoTracking;
            data = data.Where(s => s.ShopId == shopId);
            var list = await data.ProjectTo<PurchaseOderDto>(_mapper.Value.ConfigurationProvider).ToPagedListAsync(pageIndex, pageSize);
            return ResultModel.Success(list);
        }

        public async Task<IResultModel> GetPageByShopIdWhereQueryAsync(int pageIndex, int pageSize , Guid shopId, string oderNo)
        {
            var data = _repository.Value.TableNoTracking;
            data = data.Where(s => s.ShopId == shopId);

            oderNo = System.Web.HttpUtility.UrlDecode(oderNo);
            if (!string.IsNullOrEmpty(oderNo))
            {
                data = data.Where(s => s.OderNo != null && s.OderNo.Contains(oderNo));
            }
            var list = await data.ProjectTo<PurchaseOderDto>(_mapper.Value.ConfigurationProvider).ToPagedListAsync(pageIndex, pageSize);
            return ResultModel.Success(list);
        }
    }

    public class CreatePurchaseOderService : BaseService<PurchaseOder, PurchaseOderCreateDto, int>, ICreatePurchaseOderService, IDependency
    {
        public CreatePurchaseOderService(Lazy<IMapper> mapper, IUnitOfWork unitOfWork, ILogger<CreatePurchaseOderService> logger,
            Lazy<IRepository<PurchaseOder>> repository) : base(mapper, unitOfWork, logger, repository)
        {

        }
    }


    public class UpdatePurchaseOderService : BaseService<PurchaseOder, PurchaseOderUpdateDto, int>, IUpdatePurchaseOderService, IDependency
    {
        public UpdatePurchaseOderService(Lazy<IMapper> mapper, IUnitOfWork unitOfWork, ILogger<UpdatePurchaseOderService> logger, Lazy<IRepository<PurchaseOder>> repository)
        : base(mapper, unitOfWork, logger, repository)
        {

        }
    }
}
