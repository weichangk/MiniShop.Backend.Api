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
    [Description("采购退货订单")]
    public class PurchaseReturnOderController : ControllerAbstract
    {
        private readonly Lazy<IPurchaseReturnOderService> _purchaseReturnOderService;
        private readonly Lazy<ICreatePurchaseReturnOderService> _createPurchaseReturnOderService;
        private readonly Lazy<IUpdatePurchaseReturnOderService> _updatePurchaseReturnOderService;

        public PurchaseReturnOderController(ILogger<PurchaseReturnOderController> logger, Lazy<IMapper> mapper,
            Lazy<IPurchaseReturnOderService> purchaseReturnOderService,
            Lazy<ICreatePurchaseReturnOderService> createPurchaseReturnOderService,
            Lazy<IUpdatePurchaseReturnOderService> updatePurchaseReturnOderService) : base(logger, mapper)
        {
            _purchaseReturnOderService = purchaseReturnOderService;
            _createPurchaseReturnOderService = createPurchaseReturnOderService;
            _updatePurchaseReturnOderService = updatePurchaseReturnOderService;
        }

        [Description("根据采购退货订单 ID 获取采购退货订单")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "id", param = "采购退货订单 ID")]
        [HttpGet("GetByIdAsync")]
        public async Task<IResultModel> GetByIdAsync([Required] int id)
        {
            _logger.LogDebug($"根据采购退货订单 ID：{id} 获取采购退货订单");
            return await _purchaseReturnOderService.Value.GetByIdAsync(id);
        }

        [Description("根据 shopId、采购退货订单号获取采购退货订单")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "shopId", param = "shopId")]
        [Parameters(name = "oderNo", param = "订单号")]
        [HttpGet("GetByShopIdOderNoAsync")]
        public async Task<IResultModel> GetByShopIdOderNoAsync([Required] Guid shopId, string oderNo)
        {
            _logger.LogDebug($"根据 shopId：{shopId} 采购退货订单号：{oderNo} 获取采购退货订单");
            return await _purchaseReturnOderService.Value.GetByShopIdOderNoAsync(shopId, oderNo);
        }

        [Description("根据 shopId 获取采购退货订单分页列表")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "pageIndex", param = "索引页")]
        [Parameters(name = "pageSize", param = "单页条数")]
        [Parameters(name = "shopId", param = "shopId")]
        [HttpGet("GetPageByShopIdAsync")]
        public async Task<IResultModel> GetPageByShopIdAsync([Required] int pageIndex, int pageSize, Guid shopId)
        {
            _logger.LogDebug($"根据 shopId：{shopId} 分页条件：索引页{pageIndex} 单页条数{pageSize} 获取采购退货订单分页列表");
            return await _purchaseReturnOderService.Value.GetPageByShopIdAsync(pageIndex, pageSize, shopId);
        }

        [Description("根据 shopId 附加查询条件获取采购退货订单分页列表")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "pageIndex", param = "索引页")]
        [Parameters(name = "pageSize", param = "单页条数")]
        [Parameters(name = "shopId", param = "shopId")]
        [Parameters(name = "oderNo", param = "采购退货订单号")]
        [HttpGet("GetPageByShopIdWhereQueryAsync")]
        public async Task<IResultModel> GetPageByShopIdWhereQueryAsync([Required] int pageIndex, int pageSize, Guid shopId, string oderNo)
        {
            _logger.LogDebug($"根据 shopId：{shopId} 分页条件：索引页 {pageIndex} 单页条数 {pageSize} 查询条件：采购退货订单号 {oderNo} 获取采购退货订单分页列表");
            return await _purchaseReturnOderService.Value.GetPageByShopIdWhereQueryAsync(pageIndex, pageSize, shopId, oderNo);
        }

        [Description("根据 ID 删除采购退货订单")]
        [Parameters(name = "id", param = "采购退货订单ID")]
        [HttpDelete("DeleteAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> DeleteAsync([Required] int id)
        {
            _logger.LogDebug("删除采购退货订单");
            return await _purchaseReturnOderService.Value.RemoveAsync(id);
        }

        [Description("根据 ID 集合批量删除采购退货订单")]
        [Parameters(name = "ids", param = "采购退货订单 ID 集合")]
        [HttpDelete("BatchDeleteAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> BatchDeleteAsync([FromBody] List<int> ids)
        {
            _logger.LogDebug("批量删除采购退货订单");
            return await _purchaseReturnOderService.Value.RemoveAsync(ids);
        }

        [Description("添加采购退货订单")]
        [HttpPost("InsertAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> InsertAsync([FromBody] PurchaseReturnOderCreateDto model)
        {
            _logger.LogDebug("添加采购退货订单");
            return await _createPurchaseReturnOderService.Value.InsertAsync(model);
        }

        [Description("Put 修改采购退货订单")]
        [HttpPut("UpdateAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> UpdateAsync([FromBody] PurchaseReturnOderUpdateDto model)
        {
            _logger.LogDebug("修改采购退货订单");
            return await _updatePurchaseReturnOderService.Value.UpdateAsync(model);
        }

        [Description("Patch 修改采购退货订单")]
        [HttpPatch("PatchAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> PatchAsync([FromRoute] int id, [FromBody] JsonPatchDocument<PurchaseReturnOderUpdateDto> patchDocument)
        {
            _logger.LogDebug("使用JsonPatch修改采购退货订单");
            return await _updatePurchaseReturnOderService.Value.PatchAsync(id, patchDocument);
        }

        [Description("修改采购退货订单金额")]
        [Parameters(name = "id", param = "ID")]
        [Parameters(name = "oderAmount", param = "订单金额")]
        [HttpPut("UpdateReturnOderAmountAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> UpdateReturnOderAmountAsync([FromForm]int id, [FromForm]decimal oderAmount)
        {
            _logger.LogDebug("修改采购退货订单金额");
            return await _updatePurchaseReturnOderService.Value.UpdateReturnOderAmountAsync(id, oderAmount);
        }
    }
}
