using AutoMapper;
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
using MiniShop.Backend.Model.Enums;

namespace MiniShop.Backend.Api.Controllers
{
    [Description("促销订单")]
    public class PromotionOderController : ControllerAbstract
    {
        private readonly Lazy<IPromotionOderService> _promotionOderService;
        private readonly Lazy<ICreatePromotionOderService> _createPromotionOderService;
        private readonly Lazy<IUpdatePromotionOderService> _updatePromotionOderService;

        public PromotionOderController(ILogger<PromotionOderController> logger, Lazy<IMapper> mapper,
            Lazy<IPromotionOderService> promotionOderService,
            Lazy<ICreatePromotionOderService> createPromotionOderService,
            Lazy<IUpdatePromotionOderService> updatePromotionOderService) : base(logger, mapper)
        {
            _promotionOderService = promotionOderService;
            _createPromotionOderService = createPromotionOderService;
            _updatePromotionOderService = updatePromotionOderService;
        }

        [Description("根据促销订单 ID 获取促销订单")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "id", param = "促销订单 ID")]
        [HttpGet("GetByIdAsync")]
        public async Task<IResultModel> GetByIdAsync([Required] int id)
        {
            _logger.LogDebug($"根据促销订单 ID：{id} 获取促销订单");
            return await _promotionOderService.Value.GetByIdAsync(id);
        }

        [Description("根据 shopId、促销订单号获取促销订单")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "shopId", param = "shopId")]
        [Parameters(name = "oderNo", param = "订单号")]
        [HttpGet("GetByShopIdOderNoAsync")]
        public async Task<IResultModel> GetByShopIdOderNoAsync([Required] Guid shopId, string oderNo)
        {
            _logger.LogDebug($"根据 shopId：{shopId} 促销订单号：{oderNo} 获取促销订单");
            return await _promotionOderService.Value.GetByShopIdOderNoAsync(shopId, oderNo);
        }

        [Description("根据 shopId 获取促销订单分页列表")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "pageIndex", param = "索引页")]
        [Parameters(name = "pageSize", param = "单页条数")]
        [Parameters(name = "shopId", param = "shopId")]
        [HttpGet("GetPageByShopIdAsync")]
        public async Task<IResultModel> GetPageByShopIdAsync([Required] int pageIndex, int pageSize, Guid shopId)
        {
            _logger.LogDebug($"根据 shopId：{shopId} 分页条件：索引页{pageIndex} 单页条数{pageSize} 获取促销订单分页列表");
            return await _promotionOderService.Value.GetPageByShopIdAsync(pageIndex, pageSize, shopId);
        }

        [Description("根据 shopId 附加查询条件获取促销订单分页列表")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "pageIndex", param = "索引页")]
        [Parameters(name = "pageSize", param = "单页条数")]
        [Parameters(name = "shopId", param = "shopId")]
        [Parameters(name = "oderNo", param = "促销订单号")]
        [HttpGet("GetPageByShopIdWhereQueryAsync")]
        public async Task<IResultModel> GetPageByShopIdWhereQueryAsync([Required] int pageIndex, int pageSize, Guid shopId, string oderNo)
        {
            _logger.LogDebug($"根据 shopId：{shopId} 分页条件：索引页 {pageIndex} 单页条数 {pageSize} 查询条件：促销订单号 {oderNo} 获取促销订单分页列表");
            return await _promotionOderService.Value.GetPageByShopIdWhereQueryAsync(pageIndex, pageSize, shopId, oderNo);
        }

        [Description("根据 ID 删除促销订单")]
        [Parameters(name = "id", param = "促销订单ID")]
        [HttpDelete("DeleteAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> DeleteAsync([Required] int id)
        {
            _logger.LogDebug("删除促销订单");
            return await _promotionOderService.Value.RemoveAsync(id);
        }

        [Description("根据 ID 集合批量删除促销订单")]
        [Parameters(name = "ids", param = "促销订单 ID 集合")]
        [HttpDelete("BatchDeleteAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> BatchDeleteAsync([FromBody] List<int> ids)
        {
            _logger.LogDebug("批量删除促销订单");
            return await _promotionOderService.Value.RemoveAsync(ids);
        }

        [Description("添加促销订单")]
        [HttpPost("InsertAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> InsertAsync([FromBody] PromotionOderCreateDto model)
        {
            _logger.LogDebug("添加促销订单");
            return await _createPromotionOderService.Value.InsertAsync(model);
        }

        [Description("Put 修改促销订单")]
        [HttpPut("UpdateAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> UpdateAsync([FromBody] PromotionOderUpdateDto model)
        {
            _logger.LogDebug("修改促销订单");
            return await _updatePromotionOderService.Value.UpdateAsync(model);
        }
    }
}
