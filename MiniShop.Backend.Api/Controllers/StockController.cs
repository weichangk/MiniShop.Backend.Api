﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MiniShop.Backend.Model.Dto;
using MiniShop.Backend.Api.Services;
using Weick.Orm.Core.Result;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace MiniShop.Backend.Api.Controllers
{
    [Description("库存信息")]
    public class StockController : ControllerAbstract
    {
        private readonly Lazy<IStockService> _stockService;
        private readonly Lazy<ICreateStockService> _createStockService;
        private readonly Lazy<IUpdateStockService> _updateStockService;
        public StockController(ILogger<StockController> logger, Lazy<IMapper> mapper,
            Lazy<IStockService> stockService,
            Lazy<ICreateStockService> createStockService,
            Lazy<IUpdateStockService> updateStockService) : base(logger, mapper)
        {
            _stockService = stockService;
            _createStockService = createStockService;
            _updateStockService = updateStockService;
        }

        [Description("根据 shopId 获取库存分页列表")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "pageIndex", param = "索引页")]
        [Parameters(name = "pageSize", param = "单页条数")]
        [Parameters(name = "shopId", param = "shopId")]
        [HttpGet("GetPageByShopIdAsync")]
        public async Task<IResultModel> GetPageByShopIdAsync([Required] int pageIndex, int pageSize, Guid shopId)
        {
            _logger.LogDebug($"根据 shopId：{shopId} 分页条件：索引页{pageIndex} 单页条数{pageSize} 获取库存分页列表");
            return await _stockService.Value.GetPageByShopIdAsync(pageIndex, pageSize, shopId);
        }

        [Description("根据 shopId 附加查询条件获取库存分页列表")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "pageIndex", param = "索引页")]
        [Parameters(name = "pageSize", param = "单页条数")]
        [Parameters(name = "shopId", param = "shopId")]
        [Parameters(name = "name", param = "商品名称")]
        [HttpGet("GetPageByShopIdWhereQueryAsync")]
        public async Task<IResultModel> GetPageByShopIdWhereQueryAsync([Required] int pageIndex, int pageSize, Guid shopId, string name)
        {
            _logger.LogDebug($"根据 shopId：{shopId} 分页条件：索引页 {pageIndex} 单页条数 {pageSize} 查询条件：商品名称 {name} 获取库存分页列表");
            return await _stockService.Value.GetPageByShopIdWhereQueryAsync(pageIndex, pageSize, shopId, name);
        }

        [Description("根据 ID 删除库存")]
        [Parameters(name = "id", param = "库存 ID")]
        [HttpDelete("DeleteAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> DeleteAsync([Required] int id)
        {
            _logger.LogDebug("删除库存");
            return await _stockService.Value.RemoveAsync(id);
        }

        [Description("根据 ID 集合批量删除库存")]
        [Parameters(name = "ids", param = "库存 ID 集合")]
        [HttpDelete("BatchDeleteAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> BatchDeleteAsync([FromBody] List<int> ids)
        {
            _logger.LogDebug("批量删除库存");
            return await _stockService.Value.RemoveAsync(ids);
        }

        [Description("添加库存")]
        [HttpPost("InsertAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> InsertAsync([FromBody] StockCreateDto model)
        {
            _logger.LogDebug("添加库存");
            return await _createStockService.Value.InsertAsync(model);
        }

        [Description("Put 修改库存")]
        [HttpPut("UpdateAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> UpdateAsync([FromBody] StockUpdateDto model)
        {
            _logger.LogDebug("修改库存");
            return await _updateStockService.Value.UpdateAsync(model);
        }

        [Description("Patch 修改库存")]
        [HttpPatch("PatchAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> PatchAsync([FromRoute] int id, [FromBody] JsonPatchDocument<StockUpdateDto> patchDocument)
        {
            _logger.LogDebug("使用JsonPatch修改库存");
            return await _updateStockService.Value.PatchAsync(id, patchDocument);
        }
    }
}
