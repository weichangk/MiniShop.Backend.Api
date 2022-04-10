﻿using MiniShop.Backend.Model.Dto;
using MiniShop.Backend.Model;
using Weick.Orm.Core.Result;
using System;
using System.Threading.Tasks;

namespace MiniShop.Backend.Api.Services
{
    public interface IPurchaseOderService : IBaseService<PurchaseOder, PurchaseOderDto, int>
    {
        Task<IResultModel> GetByOderNoOnShopAsync(Guid shopId, string oderNo);

        Task<IResultModel> GetPageByShopIdAsync(int pageIndex, int pageSize, Guid shopId);

        Task<IResultModel> GetPageByShopIdWhereQueryAsync(int pageIndex, int pageSize, Guid shopId, int storeId, string oderNo);
    }

    public interface ICreatePurchaseOderService : IBaseService<PurchaseOder, PurchaseOderCreateDto, int>
    {

    }

    public interface IUpdatePurchaseOderService : IBaseService<PurchaseOder, PurchaseOderUpdateDto, int>
    {

    }
}
