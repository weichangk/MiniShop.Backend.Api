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
    [Description("采购收货订单")]
    public class PurchaseReceiveOderController : ControllerAbstract
    {
        private readonly Lazy<IPurchaseReceiveOderService> _purchaseReceiveOderService;
        private readonly Lazy<ICreatePurchaseReceiveOderService> _createPurchaseReceiveOderService;
        private readonly Lazy<IUpdatePurchaseReceiveOderService> _updatePurchaseReceiveOderService;

        public PurchaseReceiveOderController(ILogger<PurchaseReceiveOderController> logger, Lazy<IMapper> mapper,
            Lazy<IPurchaseReceiveOderService> purchaseReceiveOderService,
            Lazy<ICreatePurchaseReceiveOderService> createPurchaseReceiveOderService,
            Lazy<IUpdatePurchaseReceiveOderService> updatePurchaseReceiveOderService) : base(logger, mapper)
        {
            _purchaseReceiveOderService = purchaseReceiveOderService;
            _createPurchaseReceiveOderService = createPurchaseReceiveOderService;
            _updatePurchaseReceiveOderService = updatePurchaseReceiveOderService;
        }

        [Description("根据采购收货订单 ID 获取采购收货订单")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "id", param = "采购收货订单 ID")]
        [HttpGet("GetByIdAsync")]
        public async Task<IResultModel> GetByIdAsync([Required] int id)
        {
            _logger.LogDebug($"根据采购收货订单 ID：{id} 获取采购收货订单");
            return await _purchaseReceiveOderService.Value.GetByIdAsync(id);
        }

        [Description("根据 shopId、采购收货订单号获取采购收货订单")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "shopId", param = "shopId")]
        [Parameters(name = "oderNo", param = "订单号")]
        [HttpGet("GetByShopIdOderNoAsync")]
        public async Task<IResultModel> GetByShopIdOderNoAsync([Required] Guid shopId, string oderNo)
        {
            _logger.LogDebug($"根据 shopId：{shopId} 采购收货订单号：{oderNo} 获取采购收货订单");
            return await _purchaseReceiveOderService.Value.GetByShopIdOderNoAsync(shopId, oderNo);
        }

        [Description("根据 shopId 获取采购收货订单分页列表")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "pageIndex", param = "索引页")]
        [Parameters(name = "pageSize", param = "单页条数")]
        [Parameters(name = "shopId", param = "shopId")]
        [HttpGet("GetPageByShopIdAsync")]
        public async Task<IResultModel> GetPageByShopIdAsync([Required] int pageIndex, int pageSize, Guid shopId)
        {
            _logger.LogDebug($"根据 shopId：{shopId} 分页条件：索引页{pageIndex} 单页条数{pageSize} 获取采购收货订单分页列表");
            return await _purchaseReceiveOderService.Value.GetPageByShopIdAsync(pageIndex, pageSize, shopId);
        }

        [Description("根据 shopId 附加查询条件获取采购收货订单分页列表")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "pageIndex", param = "索引页")]
        [Parameters(name = "pageSize", param = "单页条数")]
        [Parameters(name = "shopId", param = "shopId")]
        [Parameters(name = "oderNo", param = "采购收货订单号")]
        [HttpGet("GetPageByShopIdWhereQueryAsync")]
        public async Task<IResultModel> GetPageByShopIdWhereQueryAsync([Required] int pageIndex, int pageSize, Guid shopId, string oderNo)
        {
            _logger.LogDebug($"根据 shopId：{shopId} 分页条件：索引页 {pageIndex} 单页条数 {pageSize} 查询条件：采购收货订单号 {oderNo} 获取采购收货订单分页列表");
            return await _purchaseReceiveOderService.Value.GetPageByShopIdWhereQueryAsync(pageIndex, pageSize, shopId, oderNo);
        }

        [Description("根据 shopId 获取已审核未退货采购订单分页列表")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "pageIndex", param = "索引页")]
        [Parameters(name = "pageSize", param = "单页条数")]
        [Parameters(name = "shopId", param = "shopId")]
        [HttpGet("GetAuditedUnReturnedPageByShopIdAsync")]
        public async Task<IResultModel> GetAuditedUnReturnedPageByShopIdAsync([Required] int pageIndex, int pageSize, Guid shopId)
        {
            _logger.LogDebug($"根据 shopId：{shopId} 分页条件：索引页{pageIndex} 单页条数{pageSize} 获取已审核未退货采购订单分页列表");
            return await _purchaseReceiveOderService.Value.GetAuditedUnReturnedPageByShopIdAsync(pageIndex, pageSize, shopId);
        }

        [Description("根据 shopId 附加查询条件获取已审核未退货采购订单分页列表")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "pageIndex", param = "索引页")]
        [Parameters(name = "pageSize", param = "单页条数")]
        [Parameters(name = "shopId", param = "shopId")]
        [Parameters(name = "oderNo", param = "采购订单号")]
        [HttpGet("GetAuditedUnReturnedPageByShopIdWhereQueryAsync")]
        public async Task<IResultModel> GetAuditedUnReturnedPageByShopIdWhereQueryAsync([Required] int pageIndex, int pageSize, Guid shopId, string oderNo)
        {
            _logger.LogDebug($"根据 shopId：{shopId} 分页条件：索引页 {pageIndex} 单页条数 {pageSize} 查询条件：采购订单号 {oderNo} 获取已审核未退货采购订单分页列表");
            return await _purchaseReceiveOderService.Value.GetAuditedUnReturnedPageByShopIdWhereQueryAsync(pageIndex, pageSize, shopId, oderNo);
        }

        [Description("根据 ID 删除采购收货订单")]
        [Parameters(name = "id", param = "采购收货订单ID")]
        [HttpDelete("DeleteAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> DeleteAsync([Required] int id)
        {
            _logger.LogDebug("删除采购收货订单");
            return await _purchaseReceiveOderService.Value.RemoveAsync(id);
        }

        [Description("根据 ID 集合批量删除采购收货订单")]
        [Parameters(name = "ids", param = "采购收货订单 ID 集合")]
        [HttpDelete("BatchDeleteAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> BatchDeleteAsync([FromBody] List<int> ids)
        {
            _logger.LogDebug("批量删除采购收货订单");
            return await _purchaseReceiveOderService.Value.RemoveAsync(ids);
        }

        [Description("添加采购收货订单")]
        [HttpPost("InsertAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> InsertAsync([FromBody] PurchaseReceiveOderCreateDto model)
        {
            _logger.LogDebug("添加采购收货订单");
            return await _createPurchaseReceiveOderService.Value.InsertAsync(model);
        }

        [Description("Put 修改采购收货订单")]
        [HttpPut("UpdateAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> UpdateAsync([FromBody] PurchaseReceiveOderUpdateDto model)
        {
            _logger.LogDebug("修改采购收货订单");
            return await _updatePurchaseReceiveOderService.Value.UpdateAsync(model);
        }

        [Description("Patch 修改采购收货订单")]
        [HttpPatch("PatchAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> PatchAsync([FromRoute] int id, [FromBody] JsonPatchDocument<PurchaseReceiveOderUpdateDto> patchDocument)
        {
            _logger.LogDebug("使用JsonPatch修改采购收货订单");
            return await _updatePurchaseReceiveOderService.Value.PatchAsync(id, patchDocument);
        }

        [Description("修改采购收货订单金额")]
        [HttpPut("UpdateReceiveOderAmountAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> UpdateReceiveOderAmountAsync([FromForm]int id, [FromForm]decimal oderAmount)
        {
            _logger.LogDebug("修改采购收货订单金额");
            return await _updatePurchaseReceiveOderService.Value.UpdateReceiveOderAmountAsync(id, oderAmount);
        }
    }
}
