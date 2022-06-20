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
    [Description("采购退货订单商品")]
    public class PurchaseReturnOderItemController : ControllerAbstract
    {
        private readonly Lazy<IPurchaseReturnOderItemService> _purchaseReturnOderItemService;
        private readonly Lazy<ICreatePurchaseReturnOderItemService> _createPurchaseReturnOderItemService;
        private readonly Lazy<IUpdatePurchaseReturnOderItemService> _updatePurchaseReturnOderItemService;
        public PurchaseReturnOderItemController(ILogger<PurchaseReturnOderItemController> logger, Lazy<IMapper> mapper,
            Lazy<IPurchaseReturnOderItemService> purchaseReturnOderItemService,
            Lazy<ICreatePurchaseReturnOderItemService> createPurchaseReturnOderItemService,
            Lazy<IUpdatePurchaseReturnOderItemService> updatePurchaseReturnOderItemService) : base(logger, mapper)
        {
            _purchaseReturnOderItemService = purchaseReturnOderItemService;
            _createPurchaseReturnOderItemService = createPurchaseReturnOderItemService;
            _updatePurchaseReturnOderItemService = updatePurchaseReturnOderItemService;
        }

        [Description("根据 ID 获取采购退货订单商品")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "id", param = "ID")]
        [HttpGet("GetByIdAsync")]
        public async Task<IResultModel> GetByIdAsync([Required] int id)
        {
            _logger.LogDebug($"根据ID：{id} 获取采购退货订单商品");
            return await _purchaseReturnOderItemService.Value.GetByIdAsync(id);
        }

        [Description("根据 shopId、purchaseReturnOderId 获取全部采购退货订单商品列表")]
        [ResponseCache(Duration = 0)]
        [HttpGet("GetListAllByShopIdPurchaseReturnOderIdAsync")]
        public async Task<IResultModel> GetListAllByShopIdPurchaseReturnOderIdAsync(Guid shopId, int purchaseReturnOderId, bool isDescending = false)
        {
            _logger.LogDebug($"根据 shopId：{shopId} purchaseReturnOderId{purchaseReturnOderId} 获取全部采购退货订单商品列表");
            return await _purchaseReturnOderItemService.Value.GetListAllByShopIdPurchaseReturnOderIdAsync(shopId, purchaseReturnOderId, isDescending);
        }

        [Description("根据商店 ID、采购退货订单 ID 获取采购退货订单商品列表")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "pageIndex", param = "索引页")]
        [Parameters(name = "pageSize", param = "单页条数")]
        [Parameters(name = "shopId", param = "商店 ID")]
        [Parameters(name = "purchaseReturnOderId", param = "采购退货订单 ID")]
        [HttpGet("GetPageByShopIdPurchaseReturnOderIdAsync")]
        public async Task<IResultModel> GetPageByShopIdPurchaseReturnOderIdAsync([Required] int pageIndex, int pageSize,  Guid shopId, int purchaseReturnOderId)
        {
            _logger.LogDebug($"根据商店ID：{shopId} 采购退货订单ID：{purchaseReturnOderId} 分页条件：索引页{pageIndex} 单页条数{pageSize} 获取采购退货订单商品列表");
            return await _purchaseReturnOderItemService.Value.GetPageByShopIdPurchaseReturnOderIdAsync(pageIndex, pageSize, shopId, purchaseReturnOderId);
        }

        [Description("根据ID 删除采购退货订单商品")]
        [Parameters(name = "id", param = "ID")]
        [HttpDelete("DeleteAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> DeleteAsync([Required] int id)
        {
            _logger.LogDebug("删除采购退货订单商品");
            return await _purchaseReturnOderItemService.Value.RemoveAsync(id);
        }

        [Description("根据ID 集合批量删除采购退货订单商品")]
        [Parameters(name = "ids", param = "ID 集合")]
        [HttpDelete("BatchDeleteAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> BatchDeleteAsync([FromBody] List<int> ids)
        {
            _logger.LogDebug("批量删除采购退货订单");
            return await _purchaseReturnOderItemService.Value.RemoveAsync(ids);
        }

        [Description("添加采购退货订单商品")]
        [HttpPost("InsertAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> InsertAsync([FromBody] PurchaseReturnOderItemCreateDto model)
        {
            _logger.LogDebug("添加采购退货订单商品");
            return await _createPurchaseReturnOderItemService.Value.InsertAsync(model);
        }

        [Description("Put 修改采购退货订单商品")]
        [HttpPut("UpdateAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> UpdateAsync([FromBody] PurchaseReturnOderItemUpdateDto model)
        {
            _logger.LogDebug("修改采购退货订单商品");
            return await _updatePurchaseReturnOderItemService.Value.UpdateAsync(model);
        }

        [Description("Patch 修改采购退货订单商品")]
        [HttpPatch("PatchAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> PatchAsync([FromRoute] int id, [FromBody] JsonPatchDocument<PurchaseReturnOderItemUpdateDto> patchDocument)
        {
            _logger.LogDebug("使用JsonPatch修改采购商品");
            return await _updatePurchaseReturnOderItemService.Value.PatchAsync(id, patchDocument);
        }

        [Description("根据采购退货订单ID获取采购金额合计")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "purchaseReturnOderId", param = "采购退货订单ID")]
        [HttpGet("GetSumAmountByPurchaseReturnOderIdAsync")]
        public async Task<IResultModel> GetSumAmountByPurchaseReturnOderIdAsync([Required] int purchaseReturnOderId)
        {
            _logger.LogDebug($"根据采购退货订单ID：{purchaseReturnOderId} 获取采购退货订单金额");
            return await _purchaseReturnOderItemService.Value.GetSumAmountByPurchaseReturnOderIdAsync(purchaseReturnOderId);
        }

    }
}
