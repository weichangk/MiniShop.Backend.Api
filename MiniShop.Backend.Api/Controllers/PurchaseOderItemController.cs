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
    [Description("采购订单商品")]
    public class PurchaseOderItemController : ControllerAbstract
    {
        private readonly Lazy<IPurchaseOderItemService> _purchaseOderItemService;
        private readonly Lazy<ICreatePurchaseOderItemService> _createPurchaseOderItemService;
        private readonly Lazy<IUpdatePurchaseOderItemService> _updatePurchaseOderItemService;
        public PurchaseOderItemController(ILogger<PurchaseOderItemController> logger, Lazy<IMapper> mapper,
            Lazy<IPurchaseOderItemService> purchaseOderItemService,
            Lazy<ICreatePurchaseOderItemService> createPurchaseOderItemService,
            Lazy<IUpdatePurchaseOderItemService> updatePurchaseOderItemService) : base(logger, mapper)
        {
            _purchaseOderItemService = purchaseOderItemService;
            _createPurchaseOderItemService = createPurchaseOderItemService;
            _updatePurchaseOderItemService = updatePurchaseOderItemService;
        }

        [Description("根据 ID 获取采购订单商品")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "id", param = "采购订单商品 ID")]
        [HttpGet("GetByIdAsync")]
        public async Task<IResultModel> GetByIdAsync([Required] int id)
        {
            _logger.LogDebug($"根据采购订单商品ID：{id} 获取采购订单商品");
            return await _purchaseOderItemService.Value.GetByIdAsync(id);
        }

        [Description("根据 shopId、purchaseOderId 获取全部采购订单商品列表")]
        [ResponseCache(Duration = 0)]
        [HttpGet("GetListAllByShopIdPurchaseOderIdAsync")]
        public async Task<IResultModel> GetListAllByShopIdPurchaseOderIdAsync(Guid shopId, int purchaseOderId, bool isDescending = false)
        {
            _logger.LogDebug($"根据 shopId：{shopId} purchaseOderId：{purchaseOderId} 获取全部采购订单商品列表");
            return await _purchaseOderItemService.Value.GetListAllByShopIdPurchaseOderIdAsync(shopId, purchaseOderId, isDescending);
        }

        [Description("根据 shopId、采购订单商品ID 获取采购订单商品分页列表")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "pageIndex", param = "索引页")]
        [Parameters(name = "pageSize", param = "单页条数")]
        [Parameters(name = "shopId", param = "shopId")]
        [HttpGet("GetPageByShopIdAsync")]
        public async Task<IResultModel> GetPageByShopIdAsync([Required] int pageIndex, int pageSize, Guid shopId)
        {
            _logger.LogDebug($"根据 shopId：{shopId} 分页条件：索引页{pageIndex} 单页条数{pageSize} 获取采购订单商品分页列表");
            return await _purchaseOderItemService.Value.GetPageByShopIdAsync(pageIndex, pageSize, shopId);
        }

        [Description("根据商店 ID、采购订单 ID 获取采购订单商品列表")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "pageIndex", param = "索引页")]
        [Parameters(name = "pageSize", param = "单页条数")]
        [Parameters(name = "shopId", param = "商店 ID")]
        [Parameters(name = "purchaseOderId", param = "采购订单 ID")]
        [HttpGet("GetPageByShopIdPurchaseOderIdAsync")]
        public async Task<IResultModel> GetPageByShopIdPurchaseOderIdAsync([Required] int pageIndex, int pageSize,  Guid shopId, int purchaseOderId)
        {
            _logger.LogDebug($"根据商店ID：{shopId} 采购订单ID：{purchaseOderId} 分页条件：索引页{pageIndex} 单页条数{pageSize} 获取采购订单商品列表");
            return await _purchaseOderItemService.Value.GetPageByShopIdPurchaseOderIdAsync(pageIndex, pageSize, shopId, purchaseOderId);
        }

        [Description("根据采购订单商品 ID 删除采购订单商品")]
        [Parameters(name = "id", param = "采购订单商品 ID")]
        [HttpDelete("DeleteAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> DeleteAsync([Required] int id)
        {
            _logger.LogDebug("删除采购订单商品");
            return await _purchaseOderItemService.Value.RemoveAsync(id);
        }

        [Description("根据采购订单商品 ID 集合批量删除采购订单商品")]
        [Parameters(name = "ids", param = "采购订单商品 ID 集合")]
        [HttpDelete("BatchDeleteAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> BatchDeleteAsync([FromBody] List<int> ids)
        {
            _logger.LogDebug("批量删除采购订单");
            return await _purchaseOderItemService.Value.RemoveAsync(ids);
        }

        [Description("添加采购订单商品")]
        [HttpPost("InsertAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> InsertAsync([FromBody] PurchaseOderItemCreateDto model)
        {
            _logger.LogDebug("添加采购订单商品");
            return await _createPurchaseOderItemService.Value.InsertAsync(model);
        }

        [Description("Put 修改采购订单")]
        [HttpPut("UpdateAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> UpdateAsync([FromBody] PurchaseOderItemUpdateDto model)
        {
            _logger.LogDebug("修改采购订单商品");
            return await _updatePurchaseOderItemService.Value.UpdateAsync(model);
        }

        [Description("Patch 修改采购订单")]
        [HttpPatch("PatchAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> PatchAsync([FromRoute] int id, [FromBody] JsonPatchDocument<PurchaseOderItemUpdateDto> patchDocument)
        {
            _logger.LogDebug("使用JsonPatch修改采购商品");
            return await _updatePurchaseOderItemService.Value.PatchAsync(id, patchDocument);
        }

        [Description("根据采购订单ID获取采购金额合计")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "purchaseOderId", param = "采购订单ID")]
        [HttpGet("GetSumAmountByPurchaseOderIdAsync")]
        public async Task<IResultModel> GetSumAmountByPurchaseOderIdAsync([Required] int purchaseOderId)
        {
            _logger.LogDebug($"根据采购订单ID：{purchaseOderId} 获取采购订单金额");
            return await _purchaseOderItemService.Value.GetSumAmountByPurchaseOderIdAsync(purchaseOderId);
        }

    }
}
