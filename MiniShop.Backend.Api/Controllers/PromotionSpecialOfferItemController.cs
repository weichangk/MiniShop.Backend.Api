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

namespace MiniShop.Backend.Api.Controllers
{
    [Description("促销订单特价商品")]
    public class PromotionSpecialOfferItemController : ControllerAbstract
    {
        private readonly Lazy<IPromotionSpecialOfferItemService> _promotionSpecialOfferItemService;
        private readonly Lazy<ICreatePromotionSpecialOfferItemService> _createPromotionSpecialOfferItemService;
        private readonly Lazy<IUpdatePromotionSpecialOfferItemService> _updatePromotionSpecialOfferItemService;
        public PromotionSpecialOfferItemController(ILogger<PromotionSpecialOfferItemController> logger, Lazy<IMapper> mapper,
            Lazy<IPromotionSpecialOfferItemService> promotionSpecialOfferItemService,
            Lazy<ICreatePromotionSpecialOfferItemService> createPromotionSpecialOfferItemService,
            Lazy<IUpdatePromotionSpecialOfferItemService> updatePromotionSpecialOfferItemService) : base(logger, mapper)
        {
            _promotionSpecialOfferItemService = promotionSpecialOfferItemService;
            _createPromotionSpecialOfferItemService = createPromotionSpecialOfferItemService;
            _updatePromotionSpecialOfferItemService = updatePromotionSpecialOfferItemService;
        }

        [Description("根据 ID 获取促销订单特价商品")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "id", param = "促销订单特价商品 ID")]
        [HttpGet("GetByIdAsync")]
        public async Task<IResultModel> GetByIdAsync([Required] int id)
        {
            _logger.LogDebug($"根据促销订单特价商品ID：{id} 获取促销订单特价商品");
            return await _promotionSpecialOfferItemService.Value.GetByIdAsync(id);
        }

        [Description("根据 shopId、promotionOderId 获取全部促销订单特价商品列表")]
        [ResponseCache(Duration = 0)]
        [HttpGet("GetListAllByShopIdPromotionOderIdAsync")]
        public async Task<IResultModel> GetListAllByShopIdPromotionOderIdAsync(Guid shopId, int promotionOderId, bool isDescending = false)
        {
            _logger.LogDebug($"根据 shopId：{shopId} promotionOderId：{promotionOderId} 获取全部促销订单特价商品列表");
            return await _promotionSpecialOfferItemService.Value.GetListAllByShopIdPromotionOderIdAsync(shopId, promotionOderId, isDescending);
        }

        [Description("根据 shopId 获取促销订单特价商品分页列表")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "pageIndex", param = "索引页")]
        [Parameters(name = "pageSize", param = "单页条数")]
        [Parameters(name = "shopId", param = "shopId")]
        [HttpGet("GetPageByShopIdAsync")]
        public async Task<IResultModel> GetPageByShopIdAsync([Required] int pageIndex, int pageSize, Guid shopId)
        {
            _logger.LogDebug($"根据 shopId：{shopId} 分页条件：索引页{pageIndex} 单页条数{pageSize} 获取促销订单特价商品分页列表");
            return await _promotionSpecialOfferItemService.Value.GetPageByShopIdAsync(pageIndex, pageSize, shopId);
        }

        [Description("根据商店 ID、促销订单 ID 获取促销订单特价商品列表")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "pageIndex", param = "索引页")]
        [Parameters(name = "pageSize", param = "单页条数")]
        [Parameters(name = "shopId", param = "商店 ID")]
        [Parameters(name = "promotionOderId", param = "促销订单 ID")]
        [HttpGet("GetPageByShopIdPromotionOderIdAsync")]
        public async Task<IResultModel> GetPageByShopIdPromotionOderIdAsync([Required] int pageIndex, int pageSize,  Guid shopId, int promotionOderId)
        {
            _logger.LogDebug($"根据商店ID：{shopId} 促销订单ID：{promotionOderId} 分页条件：索引页{pageIndex} 单页条数{pageSize} 获取促销订单特价商品列表");
            return await _promotionSpecialOfferItemService.Value.GetPageByShopIdPromotionOderIdAsync(pageIndex, pageSize, shopId, promotionOderId);
        }

        [Description("根据促销订单特价商品 ID 删除促销订单特价商品")]
        [Parameters(name = "id", param = "促销订单特价商品 ID")]
        [HttpDelete("DeleteAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> DeleteAsync([Required] int id)
        {
            _logger.LogDebug("删除促销订单特价商品");
            return await _promotionSpecialOfferItemService.Value.RemoveAsync(id);
        }

        [Description("根据促销订单特价商品 ID 集合批量删除促销订单特价商品")]
        [Parameters(name = "ids", param = "促销订单特价商品 ID 集合")]
        [HttpDelete("BatchDeleteAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> BatchDeleteAsync([FromBody] List<int> ids)
        {
            _logger.LogDebug("批量删除促销订单特价商品");
            return await _promotionSpecialOfferItemService.Value.RemoveAsync(ids);
        }

        [Description("添加促销订单特价商品")]
        [HttpPost("InsertAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> InsertAsync([FromBody] PromotionSpecialOfferItemCreateDto model)
        {
            _logger.LogDebug("添加促销订单特价商品");
            return await _createPromotionSpecialOfferItemService.Value.InsertAsync(model);
        }

        [Description("Put 修改促销订单特价商品")]
        [HttpPut("UpdateAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> UpdateAsync([FromBody] PromotionSpecialOfferItemUpdateDto model)
        {
            _logger.LogDebug("修改促销订单特价商品");
            return await _updatePromotionSpecialOfferItemService.Value.UpdateAsync(model);
        }

        [Description("Patch 修改促销订单特价商品")]
        [HttpPatch("PatchAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> PatchAsync([FromRoute] int id, [FromBody] JsonPatchDocument<PromotionSpecialOfferItemUpdateDto> patchDocument)
        {
            _logger.LogDebug("使用JsonPatch修改采购商品");
            return await _updatePromotionSpecialOfferItemService.Value.PatchAsync(id, patchDocument);
        }

    }
}
