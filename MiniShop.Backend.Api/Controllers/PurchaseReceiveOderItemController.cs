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
    [Description("采购收货订单商品")]
    public class PurchaseReceiveOderItemController : ControllerAbstract
    {
        private readonly Lazy<IPurchaseReceiveOderItemService> _purchaseReceiveOderItemService;
        private readonly Lazy<ICreatePurchaseReceiveOderItemService> _createPurchaseReceiveOderItemService;
        private readonly Lazy<IUpdatePurchaseReceiveOderItemService> _updatePurchaseReceiveOderItemService;
        public PurchaseReceiveOderItemController(ILogger<PurchaseReceiveOderItemController> logger, Lazy<IMapper> mapper,
            Lazy<IPurchaseReceiveOderItemService> purchaseReceiveOderItemService,
            Lazy<ICreatePurchaseReceiveOderItemService> createPurchaseReceiveOderItemService,
            Lazy<IUpdatePurchaseReceiveOderItemService> updatePurchaseReceiveOderItemService) : base(logger, mapper)
        {
            _purchaseReceiveOderItemService = purchaseReceiveOderItemService;
            _createPurchaseReceiveOderItemService = createPurchaseReceiveOderItemService;
            _updatePurchaseReceiveOderItemService = updatePurchaseReceiveOderItemService;
        }

        [Description("根据 ID 获取采购收货订单商品")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "id", param = "ID")]
        [HttpGet("GetByIdAsync")]
        public async Task<IResultModel> GetByIdAsync([Required] int id)
        {
            _logger.LogDebug($"根据ID：{id} 获取采购收货订单商品");
            return await _purchaseReceiveOderItemService.Value.GetByIdAsync(id);
        }

        [Description("根据 shopId、purchaseReceiveOderId 获取全部采购收货订单商品列表")]
        [ResponseCache(Duration = 0)]
        [HttpGet("GetListAllByShopIdPurchaseReceiveOderIdAsync")]
        public async Task<IResultModel> GetListAllByShopIdPurchaseReceiveOderIdAsync(Guid shopId, int purchaseReceiveOderId, bool isDescending = false)
        {
            _logger.LogDebug($"根据 shopId：{shopId} purchaseOderId：{purchaseReceiveOderId} 获取全部采购收货订单商品列表");
            return await _purchaseReceiveOderItemService.Value.GetListAllByShopIdPurchaseReceiveOderIdAsync(shopId, purchaseReceiveOderId, isDescending);
        }

        [Description("根据商店 ID、采购收货订单 ID 获取采购收货订单商品列表")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "pageIndex", param = "索引页")]
        [Parameters(name = "pageSize", param = "单页条数")]
        [Parameters(name = "shopId", param = "商店 ID")]
        [Parameters(name = "purchaseReceiveOderId", param = "采购收货订单 ID")]
        [HttpGet("GetPageByShopIdPurchaseReceiveOderIdAsync")]
        public async Task<IResultModel> GetPageByShopIdPurchaseReceiveOderIdAsync([Required] int pageIndex, int pageSize,  Guid shopId, int purchaseReceiveOderId)
        {
            _logger.LogDebug($"根据商店ID：{shopId} 采购收货订单ID：{purchaseReceiveOderId} 分页条件：索引页{pageIndex} 单页条数{pageSize} 获取采购收货订单商品列表");
            return await _purchaseReceiveOderItemService.Value.GetPageByShopIdPurchaseReceiveOderIdAsync(pageIndex, pageSize, shopId, purchaseReceiveOderId);
        }

        [Description("根据ID 删除采购收货订单商品")]
        [Parameters(name = "id", param = "ID")]
        [HttpDelete("DeleteAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> DeleteAsync([Required] int id)
        {
            _logger.LogDebug("删除采购收货订单商品");
            return await _purchaseReceiveOderItemService.Value.RemoveAsync(id);
        }

        [Description("根据ID 集合批量删除采购收货订单商品")]
        [Parameters(name = "ids", param = "ID 集合")]
        [HttpDelete("BatchDeleteAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> BatchDeleteAsync([FromBody] List<int> ids)
        {
            _logger.LogDebug("批量删除采购收货订单");
            return await _purchaseReceiveOderItemService.Value.RemoveAsync(ids);
        }

        [Description("添加采购收货订单商品")]
        [HttpPost("InsertAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> InsertAsync([FromBody] PurchaseReceiveOderItemCreateDto model)
        {
            _logger.LogDebug("添加采购收货订单商品");
            return await _createPurchaseReceiveOderItemService.Value.InsertAsync(model);
        }

        [Description("Put 修改采购收货订单商品")]
        [HttpPut("UpdateAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> UpdateAsync([FromBody] PurchaseReceiveOderItemUpdateDto model)
        {
            _logger.LogDebug("修改采购收货订单商品");
            return await _updatePurchaseReceiveOderItemService.Value.UpdateAsync(model);
        }

        [Description("Patch 修改采购收货订单商品")]
        [HttpPatch("PatchAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> PatchAsync([FromRoute] int id, [FromBody] JsonPatchDocument<PurchaseReceiveOderItemUpdateDto> patchDocument)
        {
            _logger.LogDebug("使用JsonPatch修改采购商品");
            return await _updatePurchaseReceiveOderItemService.Value.PatchAsync(id, patchDocument);
        }

        [Description("根据采购收货订单ID获取采购金额合计")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "purchaseReceiveOderId", param = "采购收货订单ID")]
        [HttpGet("GetSumAmountByPurchaseReceiveOderIdAsync")]
        public async Task<IResultModel> GetSumAmountByPurchaseReceiveOderIdAsync([Required] int purchaseReceiveOderId)
        {
            _logger.LogDebug($"根据采购收货订单ID：{purchaseReceiveOderId} 获取采购收货订单金额");
            return await _purchaseReceiveOderItemService.Value.GetSumAmountByPurchaseReceiveOderIdAsync(purchaseReceiveOderId);
        }

    }
}
